﻿using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;

namespace Desafio.Presentation.API.Extensions;

public static class ConfigurationOpenApi
{
    public static void ConfigureOpenApi(this IServiceCollection services)
    {
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        services.AddEndpointsApiExplorer();

        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc(name: "v1", info: CreateInfoForApiVersion("v1", false));
            options.DescribeAllParametersInCamelCase();

            options.AddSecurityDefinition(name: "oauth2", securityScheme: new OpenApiSecurityScheme()
            {
                Description = "Bearer Token",
                In = ParameterLocation.Header,
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey
            });

            options.OperationFilter<SecurityRequirementsOperationFilter>();

            var xmlApiPath = Path.Combine(AppContext.BaseDirectory, path2: $"{typeof(Program).Assembly.GetName().Name}.xml");
            options.IncludeXmlComments(xmlApiPath);
        });
    }

    /// <summary>
    /// Informações da API
    /// </summary>
    private static OpenApiInfo CreateInfoForApiVersion(string version, bool isDeprecated)
    {
        var info = new OpenApiInfo
        {
            Title = "Desafio .Net",
            Version = version,
            Description =
                "API Desafio de .Net da Stefanini Group.",
            Contact = new OpenApiContact { Name = "Dimas Gomes Torres Duarte", Email = "dgduarte@stefanini.com" }
            //License = new OpenApiLicense() { Name = "MIT", Url = new Uri("https://opensource.org/licenses/MIT") }
        };

        if (isDeprecated)
            info.Description += " API OBSOLETA.";

        return info;
    }
}
