using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
//Здесь описана логика работы с базой данных.
//Прямо сейчас здесь происходит подключение к базе данных, из которой последовательно из всех таблиц считываются данные и сохраняются в соответствующие списки. После выполнения метода LoadData() из любого места в программе можно получить данные из БД из соответсвующих списков.

//Tady je popsána logika práce s databází.
//Je zde provedeno připojení k databázi, ze které jsou postupně načtena data ze všech tabulek a uložena do příslušných seznamů. Po provedení metody LoadData() z libovolného místa programu lze data z databáze načíst z příslušných seznamů.

namespace Pharmacy
{
    public class DatabaseLogic
    {
        //объявление всех нужных нам списков
        //deklaruje všechny seznamy, které potřebujeme

        private List<MedicationModel> _medicationsData = new List<MedicationModel>();
        private List<WarehouseModel> _warehousesData = new List<WarehouseModel>();
        private List<ManufacturerModel> _manufacturersData = new List<ManufacturerModel>();
        private List<SaleModel> _salesData = new List<SaleModel>();
        private List<PurchaseModel> _purchasesData = new List<PurchaseModel>();

        public List<MedicationModel> MedicationsData => _medicationsData;
        public List<WarehouseModel> WarehousesData => _warehousesData;
        public List<ManufacturerModel> ManufacturersData => _manufacturersData;
        public List<SaleModel> SalesData => _salesData;
        public List<PurchaseModel> PurchasesData => _purchasesData;

        public void LoadData() //главный метод для считывания данных из базы, который мы вызываем у экземпляра класса DatabaseLogic
                               // hlavní metoda pro čtení dat z databáze, kterou voláme na instanci DatabaseLogic
        {
            using (SqlConnection pharmacyConnection = DatabaseConnectionService.DbConnection)
            {
                try { pharmacyConnection.Open(); }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    Environment.Exit(1);
                }

                //вызов дочерних методов для последовательного считывания и присваивания данных в соответствующие списки
                //volání podřízených metod pro postupné čtení a přiřazování dat do příslušných seznamů
                ReadMedicationsTableData(pharmacyConnection);
                ReadWarehousesTableData(pharmacyConnection);
                ReadManufacturersTableData(pharmacyConnection);
                ReadSalesTableData(pharmacyConnection);
                ReadPurchasesTableData(pharmacyConnection);
            }
        }

        private void ReadMedicationsTableData(SqlConnection databaseConnection) //дочерний метод для считывания данных из определённой таблицы (всего у нас 5 таких методов)
                                                                                //source metoda pro čtení dat z určité tabulky (máme celkem 5 takových metod)
        {
            SqlDataReader dataReader = null;
            MedicationModel medicationRow = null;

            try
            {
                SqlCommand sqlCommand = new SqlCommand("SELECT Id_Medication, Title, Availability, Count, Prescription, Expiration_date, Price, Description FROM Medications", databaseConnection); //передача запроса в базу данных для получения данных
                                                    // Odeslání dotazu do databáze za účelem získání dat
                dataReader = sqlCommand.ExecuteReader(); //объявление считывателя
                                                        //prohlášení čtenáře
                while (dataReader.Read()) //пока данные в таблице присутствуют, мы считываем их, присваиваем в поля класса и добавляем готовый объект класса в список
                                          //pokud jsou data v tabulce, načteme je, přiřadíme je do polí třídy a hotový objekt třídy přidáme do seznamu. 
                {
                    medicationRow = new MedicationModel
                        (
                        dataReader.GetInt32(0), dataReader.GetString(1), dataReader.GetBoolean(2), dataReader.GetInt32(3), dataReader.GetString(7), dataReader.GetBoolean(4), dataReader.GetDateTime(5), dataReader.GetDecimal(6)
                        );
                    _medicationsData.Add(medicationRow);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (dataReader != null && !(dataReader.IsClosed))
                    dataReader.Close();
            }
        }
        private void ReadWarehousesTableData(SqlConnection databaseConnection)
        {
            SqlDataReader dataReader = null;
            WarehouseModel warehouseRow = null;

            try
            {
                SqlCommand sqlCommand = new SqlCommand("SELECT Id_Warehouse, Name FROM Warehouses", databaseConnection);
                dataReader = sqlCommand.ExecuteReader();
                while (dataReader.Read())
                {
                    warehouseRow = new WarehouseModel
                        (
                        dataReader.GetInt32(0), dataReader.GetString(1)
                        );
                    _warehousesData.Add(warehouseRow);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (dataReader != null && !(dataReader.IsClosed))
                    dataReader.Close();
            }
        }
        private void ReadManufacturersTableData(SqlConnection databaseConnection)
        {
            SqlDataReader dataReader = null;
            ManufacturerModel manufacturerRow = null;

            try
            {
                SqlCommand sqlCommand = new SqlCommand("SELECT Id_Manufacturer, Name, Country, License FROM Manufacturers", databaseConnection);
                dataReader = sqlCommand.ExecuteReader();
                while (dataReader.Read())
                {
                    manufacturerRow = new ManufacturerModel
                        (
                        dataReader.GetInt32(0), dataReader.GetString(1), dataReader.GetString(2), dataReader.GetString(3)
                        );
                    _manufacturersData.Add(manufacturerRow);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (dataReader != null && !(dataReader.IsClosed))
                    dataReader.Close();
            }
        }
        private void ReadSalesTableData(SqlConnection databaseConnection)
        {
            SqlDataReader dataReader = null;
            SaleModel saleRow = null;

            try
            {
                SqlCommand sqlCommand = new SqlCommand("SELECT Id_Sale, Price, Date FROM Sales", databaseConnection);
                dataReader = sqlCommand.ExecuteReader();
                while (dataReader.Read())
                {
                    saleRow = new SaleModel
                        (
                        dataReader.GetInt32(0), dataReader.GetDecimal(1), dataReader.GetDateTime(2)
                        );
                    _salesData.Add(saleRow);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (dataReader != null && !(dataReader.IsClosed))
                    dataReader.Close();
            }
        }
        private void ReadPurchasesTableData(SqlConnection databaseConnection)
        {
            SqlDataReader dataReader = null;
            PurchaseModel purchaseRow = null;

            try
            {
                SqlCommand sqlCommand = new SqlCommand("SELECT Id_Purchase, Date, Count, Cost FROM Purchases", databaseConnection);
                dataReader = sqlCommand.ExecuteReader();
                while (dataReader.Read())
                {
                    purchaseRow = new PurchaseModel
                        (
                        dataReader.GetInt32(0), dataReader.GetDateTime(1), dataReader.GetInt32(2), dataReader.GetDecimal(3)
                        );
                    _purchasesData.Add(purchaseRow);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (dataReader != null && !(dataReader.IsClosed))
                    dataReader.Close();
            }
        }
    }
}



/*======================================================THERE ARE JUST TEST OPTIONS HERE======================================================*/

//private List<T> ReadTableData<T>(SqlConnection databaseConnection, string tableName, Func<SqlDataReader, T> createObject)
//{
//    var data = new List<T>();
//    SqlDataReader dataReader = null;
//    try
//    {
//        SqlCommand sqlCommand = new SqlCommand($"SELECT * FROM {tableName}", databaseConnection);
//        dataReader = sqlCommand.ExecuteReader();
//        while (dataReader.Read())
//        {
//            data.Add(createObject(dataReader));
//        }
//    }
//    catch (Exception ex)
//    {
//        MessageBox.Show(ex.Message);
//    }
//    finally
//    {
//        if (dataReader != null && !(dataReader.IsClosed))
//            dataReader.Close();
//    }
//    return data;
//}


//_medicationsData = ReadTableData(databaseConnection, "Medications", dataReader =>
//    new MedicationModel(
//        dataReader.GetInt32(0),
//        dataReader.GetString(1),
//        dataReader.GetBoolean(2),
//        dataReader.GetInt32(3),
//        dataReader.GetString(7),
//        dataReader.GetBoolean(4),
//        dataReader.GetDateTime(5),
//        dataReader.GetDecimal(6)
//    )
//);

//_warehousesData = ReadTableData(databaseConnection, "Warehouses", dataReader =>
//    new WarehouseModel(
//        dataReader.GetInt32(0),
//        dataReader.GetString(1)
//    )
//);

//_manufacturersData = ReadTableData(databaseConnection, "Manufacturers", dataReader =>
//    new ManufacturerModel(
//        dataReader.GetInt32(0),
//        dataReader.GetString(1),
//        dataReader.GetString(2),
//        dataReader.GetString(3)
//    )
//);

//_salesData = ReadTableData(databaseConnection, "Sales", dataReader =>
//    new SaleModel(
//        dataReader.GetInt32(0),
//        dataReader.GetDecimal(1),
//        dataReader.GetDateTime(2)
//    )
//);

//_purchasesData = ReadTableData(databaseConnection, "Purchases", dataReader =>
//    new PurchaseModel(
//        dataReader.GetInt32(0),
//        dataReader.GetInt32(1),
//        dataReader.GetDateTime(2),
//        dataReader.GetDecimal(3)
//    )
//);
