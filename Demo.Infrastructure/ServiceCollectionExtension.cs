using Demo.Application.Interfaces;
using Demo.Infrastructure.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace Demo.Infrastructure
{
    public static class ServiceCollectionExtension
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddTransient<IContactRepository, ContactRepository>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();
        }
    }
}
