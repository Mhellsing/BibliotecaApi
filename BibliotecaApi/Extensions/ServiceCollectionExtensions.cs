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
    }
}
