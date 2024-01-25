using Catalog.API.Data;
using Catalog.API.Repositories;

namespace Catalog.API.Extensions;

public static class DatabaseExtensions
{
    public static void RegisterDatabaseDependencies(this IServiceCollection services)
    {
        services.AddScoped<ICatalogContext, CatalogContext>();
        services.AddScoped<IProductRepository, ProductRepository>();
    }
}
