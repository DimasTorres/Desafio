using Desafio.Infrastructure.Persistence.Data;

namespace Desafio.Presentation.API.Extensions;

public static class ConfigurationDB
{
    public static void DbConnectionConfigure(this WebApplicationBuilder builder)
    {
        //Add Connection DB
        var config = new ConfigurationBuilder()
                    .AddJsonFile("appsettings.json")
                    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true)
                    .Build();

        var defaultConnectionString = config.GetConnectionString("Default");

        builder.Services.AddScoped<IDbConnector>(db => new SqlConnection(defaultConnectionString!));
    }
}
