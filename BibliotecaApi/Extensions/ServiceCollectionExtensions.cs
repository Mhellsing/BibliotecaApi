using BibliotecaApi.Repository;
using BibliotecaApi.Repository.Connection;
using BibliotecaApi.Repository.Interfaces;
using BibliotecaApi.Services;
using BibliotecaApi.Services.Interfaces;


namespace BibliotecaApi.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<ILivroService, LivroService>();

            return services;
        }

        public static IServiceCollection AddRepository(this IServiceCollection services) 
        { 
            services.AddScoped<ILivroRepository, LivroRepository>();
            services.AddScoped<PostgreConnection>();
            
            return services;
        }
    }
}
