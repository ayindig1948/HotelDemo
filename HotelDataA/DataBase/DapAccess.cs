using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace HotelDataA.DataBase
{
    public class DapAccess : IDapAccess
    {
        private readonly IConfiguration _config;

        public DapAccess(IConfiguration config)
        {
            _config = config;
        }

        public List<T> LoadData<T, P>(string sql, P param, string csName, bool IsProcedure = false)
        {

            string cs = _config.GetConnectionString(csName);
            CommandType type = CommandType.Text;
            if (IsProcedure == true)
            {
                type = CommandType.StoredProcedure;

            }
            using (var connection = new SqlConnection(cs))
            {
                var rooms = connection.Query<T>(sql, param, commandType: type).ToList<T>();
                return rooms;
            }
        }


        public void Save<T>(string sql, T param, string csName, bool IsProcedure= false)
        {
            string cs = _config.GetConnectionString(csName);
            CommandType type = CommandType.Text;
            if (IsProcedure == true)
            {
                type = CommandType.StoredProcedure;

            }
            using (var connection = new SqlConnection(cs))
            {
                connection.Execute(sql, param, commandType: type);

            }


        }

    }
}
