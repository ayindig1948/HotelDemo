using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;

namespace SqliteData;

public class DataAccessLite : IDataAccessLite
{
    private readonly IConfiguration _config;

    public DataAccessLite(IConfiguration config)
    {
        _config=config;
    }
    public List<T> LoadData<T, P>(string sql, P par, string cStringName)
    {
        string cString=_config.GetConnectionString(cStringName);
        using (var connection = new SqliteConnection(cString))
        {
            var rows = connection.Query<T>(sql, par);
            return rows.ToList();
        }
    }
    public void Save<T>(string sql, T data, string cStringName)
    {
        string cString = _config.GetConnectionString(cStringName);
        using (var connection = new SqliteConnection(cString))
        {
            connection.Execute(sql, data);

        }


    }
}