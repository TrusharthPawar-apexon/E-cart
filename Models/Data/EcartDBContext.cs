using Microsoft.Data.SqlClient;
using System.Data;

namespace Ecart.Models.Data
{
    public class EcartDBContext
    {
        private readonly IConfiguration _configuration;
        private readonly string ConnectionString;

        public EcartDBContext(IConfiguration configuration)
        {
            ConnectionString = configuration.GetConnectionString("connection");
        }

        public IDbConnection GetDbConnection() => new SqlConnection(ConnectionString);
    }
}
