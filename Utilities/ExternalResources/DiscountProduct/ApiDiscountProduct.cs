using Microsoft.Extensions.Configuration;
using System.Text.Json;

namespace Tekton.Products.Utilities.ExternalResources.DiscountProduct
{
    public class ApiDiscountProduct
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public ApiDiscountProduct(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;

        }

        /// <summary>
        /// Get Integer Value By Id Async
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public async Task<int> GetIntegerValueByIdAsync(int productId, IConfiguration configuration)
        {
            int discount = 0;
            string apiUrl = configuration.GetSection("UrlApiDiscountProduct").Value;
            try
            {
                string urlWithId = $"{apiUrl}/{productId}";
                HttpResponseMessage response = await _httpClient.GetAsync(urlWithId);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<ApiDiscountProductResponse>(responseBody);
                discount = result?.discount ?? 0;
            }
            catch (Exception ex) { }
            return discount;
        }
    }
}
