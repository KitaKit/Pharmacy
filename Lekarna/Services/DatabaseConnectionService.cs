using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;
//Здесь будет описано подключение к базе данных
namespace Pharmacy.Services
{
    internal class DatabaseConnectionService
    {
        private readonly string _databasePath = Path.Combine(Directory.GetParent(Directory.GetParent(Environment.CurrentDirectory).Parent.FullName).Parent.FullName, "Lekarna", "Pharmacy.mdf");
        private readonly SqlConnection _dbConnection = null;
        public SqlConnection DbConnection
        {
            get { return _dbConnection;}
            private set { }
        }
        public DatabaseConnectionService() 
        {
            _dbConnection = new SqlConnection($"Data Source = (LocalDB)\\MSSQLLocalDB; AttachDbFilename={_databasePath};Integrated Security = True");
        }
    }
}
