using Dapper;
using System.Data.SqlClient;

namespace Desafio.Presentation.API.Extensions;

public class DatabaseInitializer
{
    private readonly IConfiguration _configuration;

    public DatabaseInitializer(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task InitializeDatabaseAsync()
    {
        var connectionString = _configuration.GetConnectionString("DefaultConnection");

        using (var connection = new SqlConnection(connectionString))
        {
            await connection.OpenAsync();

            // Ler o script SQL
            var scriptPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Scripts", "CreateDatabase.sql");
            var script = await File.ReadAllTextAsync(scriptPath);

            // Executar o script de criação do banco de dados
            await connection.ExecuteAsync(script);
        }
    }
}
