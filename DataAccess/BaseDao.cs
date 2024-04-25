using Dapper;
using DataTransfer;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace DataAccess
{
    public interface IBaseDao
    {
        public Task<List<T>> QueryAsync<T, U>(string sql, U parameters);

        public Task<T> QueryFirstOrDefaultAsync<T, U>(string sql, U parameters);

        public Task ExecuteAsync(string sql, object param = null);
    }


    public class BaseDao : IBaseDao
    {
        private readonly IConfiguration _configuration;

        public string ConnectionStringName = "Default";

        public BaseDao(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<List<T>> QueryAsync<T, U>(string sql, U parameters)
        {
            string? connectionString = _configuration.GetConnectionString(ConnectionStringName);

            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                var data = await connection.QueryAsync<T>(sql, parameters);
                return data.ToList();
            }
        }

        public async Task<T> QueryFirstOrDefaultAsync<T, U>(string sql, U parameters)
        {
            string? connectionString = _configuration.GetConnectionString(ConnectionStringName);

            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                var data = await connection.QueryAsync<T>(sql, parameters);
                return data.FirstOrDefault();
            }
        }

        public async Task ExecuteAsync(string sql, object param = null)
        {
            string? connectionString = _configuration.GetConnectionString(ConnectionStringName);

            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                await connection.ExecuteAsync(sql, param);
            }
        }
    }
}
