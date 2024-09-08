using Desafio.Infrastructure.Persistence.Data;

namespace Desafio.Presentation.API.Extensions;

public static class ConfigurationDB
{
    public static void DbConnectionConfigure(this WebApplicationBuilder builder)
    {
        //Add Connection DB
        var defaultConnectionString = builder.Configuration.GetConnectionString("DefaultConnection");
        builder.Services.AddScoped<IDbConnector>(db => new SqlConnection(defaultConnectionString!));

        // Adicionar serviços ao container
        builder.Services.AddSingleton<DatabaseInitializer>();
    }
}
