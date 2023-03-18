using System;
using System.Data.SqlClient;
using System.IO;
//Здесь описано подключение к базе данных с динамческой установкой пути в строке подключения для того, чтобы при запуске проекта на разных ПК, всё работало корректно без надобности изменять код программы, нужно только то, чтобы файл "Pharmacy.mdf" был в корне папки проекта, т.е. в папке "Lekarna"

//Připojení k databázi je zde popsáno s dynamickým nastavením cesty v připojovacím řádku, aby při spuštění projektu na různých počítačích vše fungovalo správně bez nutnosti měnit kód programu, stačí mít soubor "lékárna.mdf" v kořeni složky projektu, tj. ve složce "Lekárna"

namespace Pharmacy.Services
{
    static public class DatabaseConnectionService //класс статический, чтобы не было возможности делать одновременно несколько подключений
                                                  // třída je statická, aby nebylo možné navázat více spojení najednou
    {
        static private readonly string _databasePath = null; //экземпляр класса для присваивания пути к файлу с базой данных
                                                            //proměnná třídy pro přiřazení cesty k databázovému souboru

        static private readonly SqlConnection _dbConnection = null;//переменная класса для присваивания строки подключения к базе данных
                                                                  //proměnná třídy pro přiřazení řetězce pro připojení k databázi
        static public SqlConnection DbConnection
        {
            get { return _dbConnection; }
            private set {}
        }

        static DatabaseConnectionService()//конструктор класса с инициализацией переменных, после вызова которого можно подключаться к базе данных
                                          //konstruktor třídy s inicializací proměnných, po jehož zavolání se můžete připojit k databázi
        {
            _databasePath = Path.Combine(Directory.GetParent(Directory.GetParent(Environment.CurrentDirectory).Parent.FullName).Parent.FullName, "Lekarna", "Pharmacy.mdf");
            _dbConnection = new SqlConnection($"Data Source = (LocalDB)\\MSSQLLocalDB; AttachDbFilename={_databasePath};Integrated Security = True");
        }
    }
}
