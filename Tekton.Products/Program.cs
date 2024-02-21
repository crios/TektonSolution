using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Serilog;
using Serilog.Events;
using System.Reflection;
using Tekton.Products.APILayer.Models;
using Tekton.Products.BusinessLayer.Implementations;
using Tekton.Products.BusinessLayer.Interfaces;
using Tekton.Products.RepositoryLayer.Context;
using Tekton.Products.RepositoryLayer.Implementations;
using Tekton.Products.RepositoryLayer.Interfaces;
using Tekton.Products.Utilities.ExternalResources.DiscountProduct;

var builder = WebApplication.CreateBuilder(args);


#region DATABASE
IConfigurationRoot configuration = new ConfigurationBuilder().SetBasePath(AppDomain.CurrentDomain.BaseDirectory).AddJsonFile("appsettings.json").Build();
string connectionString = configuration.GetConnectionString("ConexionBD");
#endregion

#region SERVICES
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddTransient<IProductRepository, ProductRepository>();
builder.Services.AddTransient<IProductService, ProductService>();
builder.Services.AddHttpClient<ApiDiscountProduct>();
builder.Services.AddMemoryCache();
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));
builder.Logging.AddSerilog(new LoggerConfiguration()
        .MinimumLevel.Information()
        .Enrich.FromLogContext()
        .WriteTo.File(configuration.GetValue<string>("PathFileLog"))
        .WriteTo.Logger(l => l.Filter.ByIncludingOnly(e => e.Level == LogEventLevel.Warning))
        .CreateLogger());
#endregion


#region SWAGGER
builder.Services.AddSwaggerGen(options =>
    {
        string xfile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        string xpath = Path.Combine(AppContext.BaseDirectory, xfile);
        if (File.Exists(xpath))
            options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xpath));
    });
#endregion
var app = builder.Build();
#region CACHE
try
{
    IMemoryCache cache = app.Services.GetRequiredService<IMemoryCache>();
    var cacheEntryOptions = new MemoryCacheEntryOptions
    {
        AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(configuration.GetValue<int>("CacheForStatusName:TimeSpanFromMinutes"))
    };

    IEnumerable<StatusCacheModel> status = configuration.GetSection("CacheForStatusName:ValuesCacheForStatusName").Get<IEnumerable<StatusCacheModel>>();
    cache.Set(configuration.GetValue<string>("CacheForStatusName:CacheNameForStatusName"), status, cacheEntryOptions);
}
catch (Exception) { }
#endregion

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
