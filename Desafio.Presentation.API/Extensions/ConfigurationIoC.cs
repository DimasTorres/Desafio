using Desafio.Core.Service.Interfaces;
using Desafio.Core.Service.Services;
using Desafio.Infrastructure.Persistence.Interfaces;
using Desafio.Infrastructure.Persistence.Interfaces.Base;
using Desafio.Infrastructure.Persistence.Repositories;
using Desafio.Infrastructure.Persistence.Repositories.Base;

namespace Desafio.Presentation.API.Extensions;

public static class ConfigurationIoC
{
    public static void ConfigureIoC(this IServiceCollection services)
    {
        
        //Unit Of Work
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        /*
        //Security
        services.AddScoped<ISecurityService, SecurityService>();
        services.AddScoped<ITokenManager, TokenManager>();

        //Application
        services.AddScoped<IOrderApplication, OrderApplication>();
        services.AddScoped<IProductApplication, ProductApplication>();
        services.AddScoped<IUserApplication, UserApplication>();
          */

        //Add Services
        services.AddScoped<IOrderService, OrderService>();
        services.AddScoped<IOrderItemService, OrderItemService>();
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<IUserService, UserService>();
      
        //Add Repositories
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<IOrderItemRepository, OrderItemRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IUserRepository, UserRepository>();        
    }
}
