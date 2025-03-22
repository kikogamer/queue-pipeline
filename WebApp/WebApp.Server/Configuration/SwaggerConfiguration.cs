using Asp.Versioning.ApiExplorer;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Text.Json;

namespace WebApp.Server.Configuration
{
    public static class SwaggerConfiguration
    {
        public static IServiceCollection AddSwaggerConfiguration(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.OperationFilter<SwaggerDefaultValues>();
            });

            return services;
        }

        public static IApplicationBuilder UseSwaggerConfiguration(this IApplicationBuilder app,
                                                                  IApiVersionDescriptionProvider provider)
        {
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                var apiVersionDescriptions = provider.ApiVersionDescriptions
                    .Select(description => description.GroupName);

                foreach (var description in apiVersionDescriptions)
                {
                    options.SwaggerEndpoint($"/swagger/{description}/swagger.json",
                                            description.ToUpperInvariant());
                }
            });

            return app;
        }

    }

    public class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
    {
        readonly IApiVersionDescriptionProvider provider;

        public ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider) => this.provider = provider;

        public void Configure(SwaggerGenOptions options)
        {
            foreach (var description in provider.ApiVersionDescriptions)
            {
                options.SwaggerGeneratorOptions.SwaggerDocs.Add(description.GroupName,
                                                                CreateInfoForApiVersion(description));
            }
        }

        static OpenApiInfo CreateInfoForApiVersion(ApiVersionDescription description)
        {
            var info = new OpenApiInfo()
            {
                Title = "API - Kiko Store",
                Version = description.ApiVersion.ToString(),
                Description = "API do Web App Kiko Store.",
                Contact = new OpenApiContact() { Name = "Ronaldo", Email = "kikogamer@gmail.com" }
            };

            if (description.IsDeprecated)
            {
                info.Description = "Esta versão está obsoleta!";
            }

            return info;
        }
    }

    public class SwaggerDefaultValues : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var apiDescription = context.ApiDescription;
            operation.Deprecated = apiDescription.IsDeprecated();

            if (operation.Parameters == null) return;

            foreach (var parameter in operation.Parameters)
            {
                var description = apiDescription.ParameterDescriptions.First(p => p.Name == parameter.Name);

                if (parameter.Description == null)
                {
                    parameter.Description = description.ModelMetadata?.Description;
                }

                if (parameter.Schema.Default == null &&
                    description.DefaultValue != null &&
                    description.ModelMetadata != null &&
                    description.ModelMetadata.ModelType != typeof(Guid))
                {
                    var json = JsonSerializer.Serialize(description.DefaultValue, description.ModelMetadata.ModelType);
                    parameter.Schema.Default = OpenApiAnyFactory.CreateFromJson(json);
                }

                parameter.Required |= description.IsRequired;
            }
        }
    }
}
