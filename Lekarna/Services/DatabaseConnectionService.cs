using System;
using System.Data.SqlClient;
using System.IO;
//Здесь описано подключение к базе данных с динамческой установкой пути в строке подключения для того, чтобы при запуске проекта на разных ПК, всё работало корректно без надобности изменять код программы, нужно только то, чтобы файл "Pharmacy.mdf" был в корне папки проекта, т.е. в нашем случае в папке "...\Lekarna\bin\Debug". Так же в свойствах файла с базой данных установлено "копировать всегда", чтобы он всегда был в одной папке с exe файлом

//Připojení k databázi je zde popsáno s dynamickým nastavením cesty v připojovacím řádku, aby při spuštění projektu na různých počítačích vše fungovalo správně bez nutnosti měnit kód programu, stačí mít soubor "lékárna.mdf" v kořeni složky projektu, tj. v daném případě ve složce "...\Lekarna\bin\Debug". Také vlastnosti databázového souboru jsou nastaveny na "kopírovat vždy", aby byl vždy ve stejné složce jako soubor exe

namespace Pharmacy
{
    static public class DatabaseConnectionService//класс статический, чтобы не было возможности делать одновременно несколько подключений
                                                 // třída je statická, aby nebylo možné navázat více spojení najednou
    {
        static private readonly string _databasePath = Path.Combine(Environment.CurrentDirectory, "Pharmacy.mdf");
        //экземпляр класса для присваивания пути к файлу с базой данных
       //proměnná třídy pro přiřazení cesty k databázovému souboru

        static private readonly SqlConnection _dbConnection = new SqlConnection($"Data Source = (LocalDB)\\MSSQLLocalDB; AttachDbFilename={_databasePath};Integrated Security = True"); //переменная класса для присваивания строки подключения к базе данных
                            //proměnná třídy pro přiřazení řetězce pro připojení k databázi
        static public SqlConnection DbConnection => _dbConnection; //je to stejné jako:
        //{
        //    get { return _dbConnection; }
        //}
    }
}
