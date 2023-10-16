using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeatroAPI.DataAccess.Interface;

namespace TeatroAPI.DataAccess
{
    public class ConnectionManager : Interface.IConnectionManager
    {
        private const string BD_KEY = "Teatro";
        private readonly IConfiguration _configuration;

        public ConnectionManager(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IDbConnection GetConnection()
        {
            return new SqlConnection(ConfigurationExtensions.GetConnectionString(_configuration, BD_KEY));
        }
    }
}
