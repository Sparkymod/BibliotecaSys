using System.Reflection;
using System.Text;
using BibliotecaSys.Application.Common;
using BibliotecaSys.Infrastructure.Data;
using BibliotecaSys.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace BibliotecaSys.API.Extensions;

public static class ServiceCollectionExtensions
{
    /// <summary>
    ///     Extension method to add the UnitOfWork implementation to the IServiceCollection.
    /// </summary>
    /// <param name="services">The IServiceCollection instance for which to add the UnitOfWork implementation.</param>
    public static void AddUnitOfWork(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
    }

    /// <summary>
    ///     Extension method to add the generic Repository implementation to the IServiceCollection.
    /// </summary>
    /// <param name="services">The IServiceCollection instance for which to add the generic Repository implementation.</param>
    public static void AddGenericRepository(this IServiceCollection services)
    {
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
    }

    /// <summary>
    ///     Extension method to add a custom DbContext to the IServiceCollection with lazy loading proxies and MySQL configuration.
    /// </summary>
    /// <typeparam name="TContext">The type of the DbContext to add.</typeparam>
    /// <param name="services">The IServiceCollection instance for which to add the custom DbContext.</param>
    /// <param name="config">The IConfiguration instance used to configure the DbContext options.</param>
    public static void AddCustomDbContext<TContext>(this IServiceCollection services, IConfiguration config)
        where TContext : DbContext
    {
        services.AddDbContext<TContext>(options =>
        {
            options.UseLazyLoadingProxies();
            options.UseSqlServer(config.GetConnectionString("SqlServer"));
        });
    }

    /// <summary>
    ///     Dynamically adds all non-abstract classes from the specified or current namespace ending with "Service" to the service collection.
    ///     These classes must implement an interface that follows the convention "I[ClassName]".
    /// </summary>
    /// <param name="services">The IServiceCollection instance to extend.</param>
    /// <param name="namespace">The namespace to scan for services. If not provided or empty, the current application's friendly name will be used.</param>
    public static void AddServicesFromAssembly(this IServiceCollection services, string @namespace = "")
    {
        @namespace = string.IsNullOrEmpty(@namespace) ? AppDomain.CurrentDomain.FriendlyName : @namespace;

        var servicesTypes = Assembly.GetExecutingAssembly().GetTypes().Where(type =>
            type is { IsClass: true, IsAbstract: false } 
            && type.Namespace == @namespace + ".Services"
            && type.Name.EndsWith("Service"));

        foreach (var serviceType in servicesTypes)
        {
            var interfaceType = serviceType.GetInterfaces().FirstOrDefault(i => i.Name == $"I{serviceType.Name}");
            if (interfaceType == null)
            {
                continue;
            }

            services.AddScoped(interfaceType, serviceType);
        }
    }

    /// <summary>
    ///     Extension method to add SwaggerGen with custom options to the IServiceCollection.
    /// </summary>
    /// <param name="services">The IServiceCollection instance for which to add the SwaggerGen with custom options.</param>
    public static void AddSwaggerGenWithOptions(this IServiceCollection services)
    {
        services.AddSwaggerGen(swag =>
        {
            swag.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "API Biblioteca",
                Version = "v1",
                Description = "UNPHU.",
            });

            swag.OperationFilter<CustomOperationFilter>();
        });
    }
}