using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
//Здесь описана логика работы с базой данных.
//Прямо сейчас здесь происходит подключение к базе данных, из которой последовательно из всех таблиц считываются данные и сохраняются в соответствующие списки. После выполнения метода LoadData() из любого места в программе можно получить данные из БД из соответсвующих списков.

//Tady je popsána logika práce s databází.
//Je zde provedeno připojení k databázi, ze které jsou postupně načtena data ze všech tabulek a uložena do příslušných seznamů. Po provedení metody LoadData() z libovolného místa programu lze data z databáze načíst z příslušných seznamů.

namespace Pharmacy
{
    public class DatabaseIOLogic
    {
        //объявление всех нужных нам списков
        //deklaruje všechny seznamy, které potřebujeme

        public void ReadData(DataLists dataLists) //главный метод для считывания данных из базы, который мы вызываем у экземпляра класса DatabaseLogic
                               // hlavní metoda pro čtení dat z databáze, kterou voláme na instanci DatabaseLogic
        {
            DatabaseConnectionService.Connect();
            using (SqlConnection pharmacyConnection = DatabaseConnectionService.DbConnection)
            {
                try { pharmacyConnection.Open(); }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }

                //вызов дочерних методов для последовательного считывания и присваивания данных в соответствующие списки
                //volání podřízených metod pro postupné čtení a přiřazování dat do příslušných seznamů
                ReadMedicationsTableData(pharmacyConnection, dataLists);
                ReadWarehousesTableData(pharmacyConnection, dataLists);
                ReadManufacturersTableData(pharmacyConnection, dataLists);
                ReadSalesTableData(pharmacyConnection, dataLists);
                ReadPurchasesTableData(pharmacyConnection, dataLists);
            }
        }

        public void WriteData()
        {

        }

        public void EditData()
        {

        }

        private void ReadMedicationsTableData(SqlConnection databaseConnection, DataLists dataLists) //дочерний метод для считывания данных из определённой таблицы (всего у нас 5 таких методов)
                                                                                //source metoda pro čtení dat z určité tabulky (máme celkem 5 takových metod)
        {
            SqlDataReader dataReader = null;
            MedicationModel medicationRow = null;

            try
            {
                SqlCommand sqlCommand = new SqlCommand("SELECT Medications.Id_Medication, Medications.Title, Medications.Availability, Medications.Count, Medications.Prescription, Medications.Expiration_date, Medications.Price, Medications.Description, Warehouses.Name AS Warehouse, Medication_forms.Form AS MedicationForm, Manufacturers.Name AS Manufacturer, Categories.Name AS Category FROM Medications LEFT JOIN Warehouses ON Medications.Warehouse_Id = Warehouses.Id_Warehouse LEFT JOIN Medication_forms ON Medications.MedicationForm_Id = Medication_forms.Id_Form LEFT JOIN Manufacturers ON Medications.Manufacturer_Id = Manufacturers.Id_Manufacturer LEFT JOIN Categories ON Medications.Category_Id = Categories.Id_Category", databaseConnection); //передача запроса в базу данных для получения данных
                                                    // Odeslání dotazu do databáze za účelem získání dat
                dataReader = sqlCommand.ExecuteReader(); //объявление считывателя
                                                        //prohlášení čtenáře
                while (dataReader.Read()) //пока данные в таблице присутствуют, мы считываем их, присваиваем в поля класса и добавляем готовый объект класса в список
                                          //pokud jsou data v tabulce, načteme je, přiřadíme je do polí třídy a hotový objekt třídy přidáme do seznamu. 
                {
                    medicationRow = new MedicationModel
                        (
                        dataReader.GetInt32(0), dataReader.GetString(1), dataReader.GetBoolean(2), dataReader.GetInt32(3), dataReader.GetString(7), dataReader.GetBoolean(4), dataReader.GetDateTime(5), dataReader.GetDecimal(6), dataReader.GetString(8), dataReader.GetString(9), dataReader.GetString(10), dataReader.GetString(11)
                        );
                    dataLists.AddToDataList(medicationRow, dataLists.MedicationsData);
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
        private void ReadWarehousesTableData(SqlConnection databaseConnection, DataLists dataLists)
        {
            SqlDataReader dataReader = null;
            WarehouseModel warehouseRow = null;

            try
            {
                SqlCommand sqlCommand = new SqlCommand("SELECT Warehouses.*, STRING_AGG(Medications.Title, ', ') AS Medications FROM Warehouses LEFT JOIN Medications ON Warehouses.Id_Warehouse = Medications.Warehouse_Id WHERE Warehouses.Id_Warehouse = Medications.Warehouse_Id GROUP BY Warehouses.Id_Warehouse, Warehouses.Name", databaseConnection);
                dataReader = sqlCommand.ExecuteReader();
                while (dataReader.Read())
                {
                    warehouseRow = new WarehouseModel
                        (
                        dataReader.GetInt32(0), dataReader.GetString(1), dataReader.GetString(2)
                        );
                    dataLists.AddToDataList(warehouseRow, dataLists.WarehousesData);
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
        private void ReadManufacturersTableData(SqlConnection databaseConnection, DataLists dataLists)
        {
            SqlDataReader dataReader = null;
            ManufacturerModel manufacturerRow = null;

            try
            {
                SqlCommand sqlCommand = new SqlCommand("SELECT Manufacturers.*, p.last_purchase_date, s.Providers, Medications.Medications FROM Manufacturers LEFT JOIN (SELECT Manufacturer_Id, MAX(Date) AS last_purchase_date FROM Purchases GROUP BY Manufacturer_Id) p ON Manufacturers.Id_Manufacturer = p.Manufacturer_Id LEFT JOIN (SELECT DISTINCT Manufacturer_Id, STRING_AGG(Providers.Name, ', ') AS Providers FROM Purchases JOIN Providers ON Purchases.Provider_Id = Providers.Id_Provider GROUP BY Manufacturer_Id) s ON Manufacturers.Id_Manufacturer = s.Manufacturer_Id LEFT JOIN (SELECT Manufacturer_Id, STRING_AGG(Medications.Title, ', ') AS Medications FROM Medications GROUP BY Manufacturer_Id) Medications ON Manufacturers.Id_Manufacturer = Medications.Manufacturer_Id", databaseConnection);
                dataReader = sqlCommand.ExecuteReader();
                while (dataReader.Read())
                {
                    manufacturerRow = new ManufacturerModel
                        (
                        dataReader.GetInt32(0), dataReader.GetString(1), dataReader.GetString(2), dataReader.GetString(3), dataReader.GetDateTime(4), dataReader.GetString(5), dataReader.GetString(6)
                        );
                    dataLists.AddToDataList(manufacturerRow, dataLists.ManufacturersData);
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
        private void ReadSalesTableData(SqlConnection databaseConnection, DataLists dataLists)
        {
            SqlDataReader dataReader = null;
            SaleModel saleRow = null;

            try
            {
                SqlCommand sqlCommand = new SqlCommand("SELECT Sales.*, COUNT(Sold_medications.Sale_Id) AS number_of_positions, STRING_AGG(Medications.Title, ', ') AS Medications FROM Sales LEFT JOIN Sold_medications ON Sales.Id_Sale = Sold_medications.Sale_Id LEFT JOIN Medications ON Sold_medications.Medication_Id = Medications.Id_Medication GROUP BY Sales.Id_Sale, Sales.Price, Sales.Date", databaseConnection);
                
                dataReader = sqlCommand.ExecuteReader();
                while (dataReader.Read())
                {
                    saleRow = new SaleModel
                        (
                        dataReader.GetInt32(0), dataReader.GetDecimal(1), dataReader.GetDateTime(2), dataReader.GetInt32(3), dataReader.GetString(4)
                        );
                    dataLists.AddToDataList(saleRow, dataLists.SalesData);
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
        private void ReadPurchasesTableData(SqlConnection databaseConnection, DataLists dataLists)
        {
            SqlDataReader dataReader = null;
            PurchaseModel purchaseRow = null;

            try
            {
                SqlCommand sqlCommand = new SqlCommand("SELECT Purchases.Id_Purchase, Purchases.Date, Purchases.Count, Purchases.Cost, Medications.Title AS Medication, Manufacturers.Name AS Manufacturer, Providers.Name AS Provider FROM Purchases LEFT JOIN Medications ON Purchases.Medication_Id = Medications.Id_Medication LEFT JOIN Manufacturers ON Purchases.Manufacturer_Id = Manufacturers.Id_Manufacturer LEFT JOIN Providers ON Purchases.Provider_Id = Providers.Id_Provider", databaseConnection);
                dataReader = sqlCommand.ExecuteReader();
                while (dataReader.Read())
                {
                    purchaseRow = new PurchaseModel
                        (
                        dataReader.GetInt32(0), dataReader.GetDateTime(1), dataReader.GetInt32(2), dataReader.GetDecimal(3), dataReader.GetString(4), dataReader.GetString(5), dataReader.GetString(6)
                        );
                    dataLists.AddToDataList(purchaseRow, dataLists.PurchasesData);
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

/*private List<T> ReadTableData<T>(SqlConnection databaseConnection, string tableName, Func<SqlDataReader, T> createObject)
{
    var data = new List<T>();
    SqlDataReader dataReader = null;
    try
    {
        SqlCommand sqlCommand = new SqlCommand($"SELECT * FROM {tableName}", databaseConnection);
        dataReader = sqlCommand.ExecuteReader();
        while (dataReader.Read())
        {
            data.Add(createObject(dataReader));
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
    return data;
}


_medicationsData = ReadTableData(databaseConnection, "Medications", dataReader =>
    new MedicationModel(
        dataReader.GetInt32(0),
        dataReader.GetString(1),
        dataReader.GetBoolean(2),
        dataReader.GetInt32(3),
        dataReader.GetString(7),
        dataReader.GetBoolean(4),
        dataReader.GetDateTime(5),
        dataReader.GetDecimal(6)
    )
);

_warehousesData = ReadTableData(databaseConnection, "Warehouses", dataReader =>
    new WarehouseModel(
        dataReader.GetInt32(0),
        dataReader.GetString(1)
    )
);

_manufacturersData = ReadTableData(databaseConnection, "Manufacturers", dataReader =>
    new ManufacturerModel(
        dataReader.GetInt32(0),
        dataReader.GetString(1),
        dataReader.GetString(2),
        dataReader.GetString(3)
    )
);

_salesData = ReadTableData(databaseConnection, "Sales", dataReader =>
    new SaleModel(
        dataReader.GetInt32(0),
        dataReader.GetDecimal(1),
        dataReader.GetDateTime(2)
    )
);

_purchasesData = ReadTableData(databaseConnection, "Purchases", dataReader =>
    new PurchaseModel(
        dataReader.GetInt32(0),
        dataReader.GetInt32(1),
        dataReader.GetDateTime(2),
        dataReader.GetDecimal(3)
    )
);*/
