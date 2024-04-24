using System;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.FileExtensions;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.Data.SqlClient;

class Program
{
    private static IConfiguration _configuration;

    static async Task Main(string[] args)
    {
        // Load appsettings.json configuration
        _configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .Build();

        // Retrieve connection string from configuration
        string connectionString = _configuration.GetConnectionString("DefaultConnection");

        try
        {
            using IDbConnection db = new SqlConnection(connectionString);
            await db.OpenAsync();

            // Execute a test query
            var result = await db.QueryFirstOrDefaultAsync<int>("SELECT 1");

            Console.WriteLine("Database connection test successful.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Database connection test failed: {ex.Message}");
        }
    }
}
