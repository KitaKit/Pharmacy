using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Xml.Linq;

namespace Pharmacy
{
    public class DatabaseIOLogic
    {
        public void ReadData()
        {
            DatabaseConnectionService.Connect();
            using (SqlConnection pharmacyConnection = DatabaseConnectionService.DbConnection)
            {
                try { pharmacyConnection.Open(); }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                ReadMedicationsTableData(pharmacyConnection);
                ReadWarehousesTableData(pharmacyConnection);
                ReadManufacturersTableData(pharmacyConnection);
                ReadSalesTableData(pharmacyConnection);
                ReadPurchasesTableData(pharmacyConnection);
                ReadCategoriesTableData(pharmacyConnection);
                ReadMedicationFormsTableData(pharmacyConnection);
                ReadProvidersTableData(pharmacyConnection);
                pharmacyConnection.Close();
            }
        }

        public void WriteData<T>(T model, SelectedTable selectedTable)
        {
            DatabaseConnectionService.Connect();
            using (SqlConnection pharmacyConnection = DatabaseConnectionService.DbConnection)
            {
                try { pharmacyConnection.Open(); }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                switch (selectedTable)
                {
                    case SelectedTable.Medications:
                        WriteDataToMedications(model as MedicationModel, pharmacyConnection);
                        break;
                    case SelectedTable.Warehouses:
                        WriteDataToWarehouses(model as WarehouseModel, pharmacyConnection);
                        break;
                    case SelectedTable.Manufacturers:
                        WriteDataToManufacturers(model as ManufacturerModel, pharmacyConnection);
                        break;
                    case SelectedTable.Sales:
                        WriteDataToSales(model as SaleModel, pharmacyConnection);
                        break;
                    case SelectedTable.Purchases:
                        WriteDataToPurchases(model as PurchaseModel, pharmacyConnection);
                        break;
                    default:
                        break;
                }
                pharmacyConnection.Close();
            }
        }

        public void EditData()
        {

        }

        private void WriteDataToMedications(MedicationModel model, SqlConnection databaseConnection)
        {
            using (SqlCommand sqlCommand = new SqlCommand("INSERT INTO Medications(Title, Count, Availability, Description, Prescription,\r\nExpiration_date, Price, Warehouse_Id, MedicationForm_Id, Manufacturer_Id, Category_Id) VALUES (@title, @count, @availability, @description, @prescription, @expiration_date, @price, @warehouse_Id, @medicationForm_Id, @manufacturer_Id, @category_Id)", databaseConnection))
            {
                try
                {
                    sqlCommand.Parameters.Clear();
                    sqlCommand.Parameters.AddWithValue("@title", model.Title);
                    sqlCommand.Parameters.AddWithValue("@count", model.Count);
                    sqlCommand.Parameters.AddWithValue("@price", model.Price);
                    sqlCommand.Parameters.AddWithValue("@availability", model.Availability);
                    sqlCommand.Parameters.AddWithValue("@description", model.Description);
                    sqlCommand.Parameters.AddWithValue("@prescription", model.Prescription);
                    sqlCommand.Parameters.AddWithValue("@expiration_date", $"{model.ExpirationDate.Month}/{model.ExpirationDate.Day}/{model.ExpirationDate.Year}");
                    sqlCommand.Parameters.AddWithValue("@warehouse_Id", DataLists.WarehousesData.Find(x => x.Name == model.Warehouse).Id);
                    sqlCommand.Parameters.AddWithValue("@medicationForm_Id", DataLists.MedicationFormsData.Find(x => x.Form == model.Form).Id);
                    sqlCommand.Parameters.AddWithValue("@manufacturer_Id", DataLists.ManufacturersData.Find(x => x.Name == model.Manufacturer).Id);
                    sqlCommand.Parameters.AddWithValue("@category_Id", DataLists.CategoriesData.Find(x => x.Name == model.Category).Id);
                    sqlCommand.ExecuteNonQuery();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        private void WriteDataToPurchases(PurchaseModel model, SqlConnection databaseConnection)
        {
            using (SqlCommand sqlCommand = new SqlCommand("INSERT INTO Purchases(Date, Cost, Provider_Id) VALUES (@date, @cost, @provider_Id)", databaseConnection))
            {
                try
                {
                    sqlCommand.Parameters.Clear();
                    sqlCommand.Parameters.AddWithValue("@date", $"{model.DeliveryDate.Month}/{model.DeliveryDate.Day}/{model.DeliveryDate.Year}");
                    sqlCommand.Parameters.AddWithValue("@cost", model.Cost);
                    sqlCommand.Parameters.AddWithValue("@provider_Id", DataLists.ProvidersData.Find(x => x.Name == model.Provider).Id);
                    sqlCommand.ExecuteNonQuery();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void WriteDataToSales(SaleModel model, SqlConnection databaseConnection)
        {
            using (SqlCommand sqlCommand = new SqlCommand("INSERT INTO Sales(Price, Date) VALUES (@price, @date", databaseConnection))
            {
                try
                {
                    sqlCommand.Parameters.Clear();
                    sqlCommand.Parameters.AddWithValue("@price", model.Price);
                    sqlCommand.Parameters.AddWithValue("@date", $"{model.Date.Month}/{model.Date.Day}/{model.Date.Year}");
                    sqlCommand.ExecuteNonQuery();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void WriteDataToManufacturers(ManufacturerModel model, SqlConnection databaseConnection)
        {
            using (SqlCommand sqlCommand = new SqlCommand("INSERT INTO Manufacturers(Name, Country, License) VALUES (@name, @country, @license)", databaseConnection))
            {
                try
                {
                    sqlCommand.Parameters.Clear();
                    sqlCommand.Parameters.AddWithValue("@name", model.Name);
                    sqlCommand.Parameters.AddWithValue("@country", model.Country);
                    sqlCommand.Parameters.AddWithValue("@license", model.License);
                    sqlCommand.ExecuteNonQuery();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void WriteDataToWarehouses(WarehouseModel model, SqlConnection databaseConnection)
        {
            using (SqlCommand sqlCommand = new SqlCommand("INSERT INTO Warehouses(Name) VALUES (@name)", databaseConnection))
            {
                try
                {
                    sqlCommand.Parameters.Clear();
                    sqlCommand.Parameters.AddWithValue("@name", model.Name);
                    sqlCommand.ExecuteNonQuery();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }


        private void ReadMedicationsTableData(SqlConnection databaseConnection)
        {
            MedicationModel medicationRow = null;

            using (SqlCommand sqlCommand = new SqlCommand("SELECT Medications.Id_Medication, Medications.Title, Medications.Availability, Medications.Count, Medications.Prescription, Medications.Expiration_date, Medications.Price, Medications.Description, Warehouses.Name AS Warehouse, Medication_forms.Form AS MedicationForm, Manufacturers.Name AS Manufacturer, Categories.Name AS Category FROM Medications LEFT JOIN Warehouses ON Medications.Warehouse_Id = Warehouses.Id_Warehouse LEFT JOIN Medication_forms ON Medications.MedicationForm_Id = Medication_forms.Id_Form LEFT JOIN Manufacturers ON Medications.Manufacturer_Id = Manufacturers.Id_Manufacturer LEFT JOIN Categories ON Medications.Category_Id = Categories.Id_Category", databaseConnection))
            {
                try
                {
                    using (SqlDataReader dataReader = sqlCommand.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            medicationRow = new MedicationModel
                                (
                                dataReader.GetInt32(0), dataReader.GetString(1), dataReader.GetBoolean(2), dataReader.GetInt32(3), dataReader.GetString(7), dataReader.GetBoolean(4), dataReader.GetDateTime(5), dataReader.GetDecimal(6), dataReader.GetString(8), dataReader.GetString(9), dataReader.GetString(10), dataReader.GetString(11)
                                );
                            DataLists.AddToDataList(medicationRow, DataLists.MedicationsData);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
  
            }
               
        }
        private void ReadWarehousesTableData(SqlConnection databaseConnection)
        {
            WarehouseModel warehouseRow = null;

            using (SqlCommand sqlCommand = new SqlCommand("SELECT Warehouses.* FROM Warehouses", databaseConnection))
            {
                try
                {
                    using (SqlDataReader dataReader = sqlCommand.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            warehouseRow = new WarehouseModel
                                (
                                dataReader.GetInt32(0), dataReader.GetString(1)
                                );
                            DataLists.AddToDataList(warehouseRow, DataLists.WarehousesData);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        private void ReadManufacturersTableData(SqlConnection databaseConnection)
        {
            ManufacturerModel manufacturerRow = null;

            using (SqlCommand sqlCommand = new SqlCommand("SELECT Manufacturers.* FROM Manufacturers", databaseConnection))
            {
                try
                {
                    using (SqlDataReader dataReader = sqlCommand.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            manufacturerRow = new ManufacturerModel
                                (
                                dataReader.GetInt32(0), dataReader.GetString(1), dataReader.GetString(2), dataReader.GetString(3)
                                );
                            DataLists.AddToDataList(manufacturerRow, DataLists.ManufacturersData);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        private void ReadSalesTableData(SqlConnection databaseConnection)
        { 
            SaleModel saleRow = null;
            using (SqlCommand sqlCommand = new SqlCommand("SELECT Sales.*, STRING_AGG(Medications.Title, ', ') AS Medications FROM Sales LEFT JOIN Sold_medications ON Sales.Id_Sale = Sold_medications.Sale_Id LEFT JOIN Medications ON Sold_medications.Medication_Id = Medications.Id_Medication GROUP BY Sales.Id_Sale, Sales.Price, Sales.Date", databaseConnection))
            {
                try
                {
                    using (SqlDataReader dataReader = sqlCommand.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            saleRow = new SaleModel
                                (
                                dataReader.GetInt32(0), dataReader.GetDecimal(1), dataReader.GetDateTime(2), dataReader.GetString(3)
                                );
                            DataLists.AddToDataList(saleRow, DataLists.SalesData);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        private void ReadPurchasesTableData(SqlConnection databaseConnection)
        {
            PurchaseModel purchaseRow = null;

            using (SqlCommand sqlCommand = new SqlCommand("SELECT Purchases.Id_Purchase, Purchases.Date, Purchases.Cost, Providers.Name AS Provider, STRING_AGG(Medications.Title, ', ') AS Medications FROM Purchases LEFT JOIN Providers ON Purchases.Provider_Id = Providers.Id_Provider LEFT JOIN Purchased_medications ON Purchases.Id_Purchase = Purchased_medications.Purchase_Id LEFT JOIN Medications ON Purchased_medications.Medication_Id = Medications.Id_Medication GROUP BY Purchases.Id_Purchase, Purchases.Date, Purchases.Cost, Providers.Name", databaseConnection))
            {
                try
                {
                    using (SqlDataReader dataReader = sqlCommand.ExecuteReader())
                    { 
                    while (dataReader.Read())
                    {
                        purchaseRow = new PurchaseModel
                            (
                            dataReader.GetInt32(0), dataReader.GetDateTime(1), dataReader.GetDecimal(2), dataReader.GetString(3), dataReader.GetString(4)
                            );
                        DataLists.AddToDataList(purchaseRow, DataLists.PurchasesData);
                    }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        private void ReadCategoriesTableData(SqlConnection databaseConnection)
        {
            CategoryModel categoryRow = null;

            using (SqlCommand sqlCommand = new SqlCommand("SELECT Categories.* FROM Categories", databaseConnection)) {
                try
                {
                    using (SqlDataReader dataReader = sqlCommand.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            categoryRow = new CategoryModel
                                (
                                dataReader.GetInt32(0), dataReader.GetString(1)
                                );
                            DataLists.AddToDataList(categoryRow, DataLists.CategoriesData);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        private void ReadMedicationFormsTableData(SqlConnection databaseConnection)
        {
            MedicationFormModel medicationFormRow = null;

            using (SqlCommand sqlCommand = new SqlCommand("SELECT Medication_forms.* FROM Medication_forms", databaseConnection))
            {
                try
                {
                    using (SqlDataReader dataReader = sqlCommand.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            medicationFormRow = new MedicationFormModel
                                (
                                dataReader.GetInt32(0), dataReader.GetString(1)
                                );
                            DataLists.AddToDataList(medicationFormRow, DataLists.MedicationFormsData);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        private void ReadProvidersTableData(SqlConnection databaseConnection)
        {
            ProviderModel providerRow = null;

            using (SqlCommand sqlCommand = new SqlCommand("SELECT Providers.* FROM Providers", databaseConnection))
            {
                try
                {
                    using (SqlDataReader dataReader = sqlCommand.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            providerRow = new ProviderModel
                                (
                                dataReader.GetInt32(0), dataReader.GetString(1)
                                );
                            DataLists.AddToDataList(providerRow, DataLists.ProvidersData);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
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
