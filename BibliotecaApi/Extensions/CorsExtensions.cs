using Microsoft.Extensions.DependencyInjection;

namespace BibliotecaApi.Extensions
{
    public static class CorsExtensions
    {
        private const string DefaultCorsPolicy = "DefaultCorsPolicy";

        public static IServiceCollection AddCorsPolicies(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(DefaultCorsPolicy, builder =>
                {
                    builder
                        .AllowAnyOrigin() // Para desenvolvimento. Em produção, prefira .WithOrigins("https://seusite.com")
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });

            return services;
        }

        public static string GetDefaultCorsPolicy() => DefaultCorsPolicy;
    }
}