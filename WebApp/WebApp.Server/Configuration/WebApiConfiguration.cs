using Asp.Versioning;

namespace WebApp.Server.Configuration
{
    public static class WebApiConfiguration
    {
        public static IServiceCollection AddWebApiConfiguration(this IServiceCollection services,
            IConfiguration configuration)
        {
            services
                .AddApiVersioning(options =>
                {
                    options.AssumeDefaultVersionWhenUnspecified = true;
                    options.DefaultApiVersion = new ApiVersion(1, 0);
                    options.ReportApiVersions = true;
                })
                .AddApiExplorer(options =>
                {
                    options.GroupNameFormat = "'v'VVV";
                    options.SubstituteApiVersionInUrl = true;
                });

            return services;
        }
    }
}
