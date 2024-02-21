using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Tekton.Products.APILayer.Models;
using Tekton.Products.BusinessLayer.Interfaces;
using Tekton.Products.Entity.Implementations;
using Tekton.Products.Entity.Interfaces;
using Tekton.Products.Utilities.ExternalResources.DiscountProduct;
namespace Tekton.Products.Api.Controllers
{

    /// <summary>
    /// Constructor
    /// </summary>
    [Route("api/products")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly ApiDiscountProduct _externalApiDiscountProduct;
        private readonly IMemoryCache _memoryCache;
        private readonly IConfiguration _configuration;



        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="productService"></param>
        /// <param name="externalApiDiscountProduct"></param>
        /// <param name="memoryCache"></param>
        public ProductsController(IProductService productService, ApiDiscountProduct externalApiDiscountProduct, IMemoryCache memoryCache, IConfiguration configuration)
        {
            _productService = productService;
            _externalApiDiscountProduct = externalApiDiscountProduct;
            _memoryCache = memoryCache;
            _configuration = configuration;
        }


        /// <summary>
        /// Get All Products
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<IProduct>>> GetProducts()
        {
            var products = await _productService.GetAllProducts();
            return Ok(products);
        }

        /// <summary>
        /// Get a specific product by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<ExtendedProduct>> GetProduct(int id)
        {
            var product = await _productService.GetProductById(id);
            if (product == null)
                return NotFound();


            ExtendedProduct extendedProduct = new ExtendedProduct
            {
                productId = product.productId,
                name = product.name,
                status = product.status,
                description = product.description,
                stock = product.stock,
                price = product.price
            };

            CalculateStatusName(extendedProduct);
            await CalculateFinalPrice(extendedProduct);
            return Ok(extendedProduct);
        }

        /// <summary>
        /// Calculate Final Price
        /// </summary>
        /// <param name="extendedProduct"></param>
        private async Task CalculateFinalPrice(ExtendedProduct extendedProduct)
        {
            var discount = await CalculateDiscount(extendedProduct, _externalApiDiscountProduct);
            extendedProduct.discount = discount;
            extendedProduct.finalPrice = extendedProduct.price * (100 - discount) / 100;
        }

        /// <summary>
        /// Calculate Discount
        /// </summary>
        /// <param name="extendedProduct"></param>
        /// <param name="externalApiDiscountProduct"></param>
        /// <returns></returns>
        private async Task<decimal> CalculateDiscount(ExtendedProduct extendedProduct, ApiDiscountProduct externalApiDiscountProduct)
        {
            return await externalApiDiscountProduct.GetIntegerValueByIdAsync(extendedProduct.productId, _configuration);
        }

        /// <summary>
        /// Calculate Status Name
        /// </summary>
        /// <param name="extendedProduct"></param>
        /// <returns></returns>
        private void CalculateStatusName(ExtendedProduct extendedProduct)
        {
            if (_memoryCache.TryGetValue("StatusNameCache", out IEnumerable<StatusCacheModel> statusName))
                extendedProduct.statusName = statusName.FirstOrDefault<StatusCacheModel>(s => s.status == extendedProduct.status).statusName;
        }

        /// <summary>
        /// Save a new product 
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<IProduct>> AddProduct(Product product)
        {
            await _productService.AddProduct(product);
            return CreatedAtAction(nameof(GetProduct), new { id = 0 }, product);
        }
        /// <summary>
        /// Update an existing product
        /// </summary>
        /// <param name="id"></param>
        /// <param name="product"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, Product product)
        {
            await _productService.UpdateProduct(id, product);
            return NoContent();
        }
    }
}
