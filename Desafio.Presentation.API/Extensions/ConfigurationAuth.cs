﻿using Desafio.Core.Application.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Desafio.Presentation.API.Extensions;

public static class ConfigurationAuth
{
    public static void ConfigureAuthentication(this WebApplicationBuilder builder)
    {
        //Add Connection DB
        var config = new ConfigurationBuilder()
                    .AddJsonFile("appsettings.json")
                    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true)
                    .Build();

        var authSettingsSection = config.GetSection("AuthSettings");

        builder.Services.Configure<AuthSettings>(authSettingsSection);

        var authSettings = authSettingsSection.Get<AuthSettings>();
        SymmetricSecurityKey key = new(Encoding.UTF8.GetBytes(authSettings!.Secret));

        builder.Services.AddAuthentication(x =>
        {
            x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
            .AddPolicyScheme(authenticationScheme: "desafionet", displayName: "Authorization Bearer or AccessToken", options =>
            {
                options.ForwardDefaultSelector = context =>
                {
                    if (context.Request.Headers[key: "Access-Token"].Any())
                    {
                        return "Access-Token";
                    }

                    return JwtBearerDefaults.AuthenticationScheme;
                };
            })
            .AddJwtBearer(x =>
            {
                x.TokenValidationParameters = new()
                {
                    ValidateIssuer = true,
                    ValidIssuer = "desafionet",

                    ValidateAudience = false,

                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = key,

                    ValidateLifetime = true,
                    RequireExpirationTime = true
                };
            });
    }
}
