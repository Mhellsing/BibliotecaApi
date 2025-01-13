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
            #region Services
            services.AddScoped<ILivroService, LivroService>();
            #endregion

            #region Repository
            services.AddScoped<ILivroRepository, LivroRepository>();
            services.AddScoped<PostgreConnection>();
            #endregion

            return services;
        }
    }
}
