using System;
using System.Data.SqlClient;
using System.IO;

namespace Pharmacy
{
    static public class DatabaseConnectionService
    {
        static private bool _isConnected = false;
        static public bool IsConnected => _isConnected;
        static private readonly string _databasePath = Path.Combine(Environment.CurrentDirectory, "Database\\Pharmacy.mdf");
        static private SqlConnection _dbConnection = null;
        static public SqlConnection DbConnection => _dbConnection;

        static public void Connect()
        {
               _dbConnection = new SqlConnection($"Data Source = (LocalDB)\\MSSQLLocalDB; AttachDbFilename={_databasePath};Integrated Security = True");
               _isConnected = true;
        }
    }
}
