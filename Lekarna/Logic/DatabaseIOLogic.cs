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
            using (SqlCommand sqlCommand = new SqlCommand("INSERT INTO Medications(Title, Count, Availability, Description, Prescription, Expiration_date, Price, Warehouse_Id, MedicationForm_Id, Manufacturer_Id, Category_Id) VALUES (@title, @count, @availability, @description, @prescription, @expiration_date, @price, @warehouse_Id, @medicationForm_Id, @manufacturer_Id, @category_Id)", databaseConnection))
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
                try
                {
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
                sqlCommand.Parameters.Clear();
                sqlCommand.Parameters.AddWithValue("@date", $"{model.DeliveryDate.Month}/{model.DeliveryDate.Day}/{model.DeliveryDate.Year}");
                sqlCommand.Parameters.AddWithValue("@cost", model.Cost);
                sqlCommand.Parameters.AddWithValue("@provider_Id", DataLists.ProvidersData.Find(x => x.Name == model.Provider).Id);
                try
                {
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
            using (SqlCommand sqlCommand = new SqlCommand("INSERT INTO Sales(Price, Date) VALUES (@price, @date)", databaseConnection))
            {
                sqlCommand.Parameters.Clear();
                sqlCommand.Parameters.AddWithValue("@price", model.Price);
                sqlCommand.Parameters.AddWithValue("@date", $"{model.Date.Month}/{model.Date.Day}/{model.Date.Year}");
                try
                {
                    sqlCommand.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            using (SqlCommand sqlCommand = new SqlCommand("INSERT INTO Sold_medications (Sale_Id, Medication_Id, Count) VALUES (@sale_Id, @medication_Id, @count)", databaseConnection))
            {
                foreach(var row in DataLists.SoldMedicationsData)
                {
                    sqlCommand.Parameters.Clear();
                    sqlCommand.Parameters.AddWithValue("@sale_Id", row.SaleId);
                    sqlCommand.Parameters.AddWithValue("@medication_Id", row.MedicationId);
                    sqlCommand.Parameters.AddWithValue("@count", row.Count);
                    try
                    {
                        sqlCommand.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                DataLists.SoldMedicationsData.Clear();
            }
        }

        private void WriteDataToManufacturers(ManufacturerModel model, SqlConnection databaseConnection)
        {
            using (SqlCommand sqlCommand = new SqlCommand("INSERT INTO Manufacturers(Name, Country, License) VALUES (@name, @country, @license)", databaseConnection))
            {
                sqlCommand.Parameters.Clear();
                sqlCommand.Parameters.AddWithValue("@name", model.Name);
                sqlCommand.Parameters.AddWithValue("@country", model.Country);
                sqlCommand.Parameters.AddWithValue("@license", model.License);
                try
                {
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
                sqlCommand.Parameters.Clear();
                sqlCommand.Parameters.AddWithValue("@name", model.Name);
                try
                {
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
                            if (DataLists.MedicationsData.Any(x => x.Id == dataReader.GetInt32(0)))
                                continue;
                            medicationRow = new MedicationModel
                                (
                                dataReader.GetInt32(0), dataReader.GetString(1), dataReader.GetBoolean(2), dataReader.GetInt32(3), dataReader.GetString(7), dataReader.GetBoolean(4), dataReader.GetDateTime(5), dataReader.GetDecimal(6), dataReader.GetString(8), dataReader.GetString(9), dataReader.GetString(10), dataReader.GetString(11)
                                );
                            DataLists.Add(medicationRow);
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

            using (SqlCommand sqlCommand = new SqlCommand("SELECT Warehouses.*, STRING_AGG(Medications.Title, ', ') AS Medications FROM Warehouses LEFT JOIN Medications ON Warehouses.Id_Warehouse = Medications.Warehouse_Id GROUP BY Warehouses.Id_Warehouse, Warehouses.Name", databaseConnection))
            {
                try
                {
                    using (SqlDataReader dataReader = sqlCommand.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            if (DataLists.WarehousesData.Any(x => x.Id == dataReader.GetInt32(0)))
                                continue;
                            warehouseRow = new WarehouseModel
                                (
                                dataReader.GetInt32(0), dataReader.GetString(1)
                                );
                            warehouseRow.Medications = dataReader.GetString(2);
                            DataLists.Add(warehouseRow);
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

            using (SqlCommand sqlCommand = new SqlCommand("SELECT Manufacturers.*, STRING_AGG(Medications.Title, ', ') AS Medications FROM Manufacturers LEFT JOIN Medications ON Manufacturers.Id_Manufacturer = Medications.Manufacturer_Id GROUP BY Manufacturers.Id_Manufacturer, Manufacturers.Name, Manufacturers.Country, Manufacturers.License", databaseConnection))
            {
                try
                {
                    using (SqlDataReader dataReader = sqlCommand.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            if (DataLists.ManufacturersData.Any(x => x.Id == dataReader.GetInt32(0)))
                                continue;
                            manufacturerRow = new ManufacturerModel
                                (
                                dataReader.GetInt32(0), dataReader.GetString(1), dataReader.GetString(2), dataReader.GetString(3)
                                );
                            manufacturerRow.Medications = dataReader.GetString(4);
                            DataLists.Add(manufacturerRow);
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
                            if (DataLists.SalesData.Any(x => x.Id == dataReader.GetInt32(0)))
                                continue;
                            saleRow = new SaleModel
                                (
                                dataReader.GetInt32(0), dataReader.GetDecimal(1), dataReader.GetDateTime(2), dataReader.GetString(3)
                                );
                            DataLists.Add(saleRow);
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
                        if (DataLists.PurchasesData.Any(x => x.Id == dataReader.GetInt32(0)))
                            continue;
                        purchaseRow = new PurchaseModel
                            (
                            dataReader.GetInt32(0), dataReader.GetDateTime(1), dataReader.GetDecimal(2), dataReader.GetString(3), dataReader.GetString(4)
                            );
                           
                        DataLists.Add(purchaseRow);
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
                            if (DataLists.CategoriesData.Any(x => x.Id == dataReader.GetInt32(0)))
                                continue;
                            categoryRow = new CategoryModel
                                (
                                dataReader.GetInt32(0), dataReader.GetString(1)
                                );
                            DataLists.Add(categoryRow);
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
                            if(DataLists.MedicationFormsData.Any(x => x.Id == dataReader.GetInt32(0)))
                                continue;
                            medicationFormRow = new MedicationFormModel
                                (
                                dataReader.GetInt32(0), dataReader.GetString(1)
                                );
                            DataLists.Add(medicationFormRow);
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
                            if(DataLists.ProvidersData.Any(x=>x.Id == dataReader.GetInt32(0)))
                                continue;
                            providerRow = new ProviderModel
                                (
                                dataReader.GetInt32(0), dataReader.GetString(1)
                                );
                            DataLists.Add(providerRow);
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
