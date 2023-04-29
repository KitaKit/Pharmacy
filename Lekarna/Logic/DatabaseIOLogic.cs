using System;
using System.Data.SqlClient;
using System.Linq;
using System.Windows;

namespace Pharmacy
{
    public class DatabaseIOLogic
    {
        public void ReadData(DataLists dataLists)
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
                ReadMedicationsTableData(pharmacyConnection, dataLists);
                ReadWarehousesTableData(pharmacyConnection, dataLists);
                ReadManufacturersTableData(pharmacyConnection, dataLists);
                ReadSalesTableData(pharmacyConnection, dataLists);
                ReadPurchasesTableData(pharmacyConnection, dataLists);
                ReadCategoriesTableData(pharmacyConnection, dataLists);
                ReadMedicationFormsTableData(pharmacyConnection, dataLists);
                ReadProvidersTableData(pharmacyConnection, dataLists);
            }
        }

        public void WriteData<T>(T model, SelectedTable selectedTable, DataLists dataLists)
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
                        WriteDataToMedications(model as MedicationModel, pharmacyConnection, dataLists);
                        break;
                    case SelectedTable.Warehouses:
                        WriteDataToWarehouses(model as WarehouseModel, pharmacyConnection);
                        break;
                    case SelectedTable.Manufacturers:
                        WriteDataToManufacturers(model as ManufacturerModel, pharmacyConnection);
                        break;
                    case SelectedTable.Sales:
                        WriteDataToSales(model as SaleModel, pharmacyConnection, dataLists);
                        break;
                    case SelectedTable.Purchases:
                        WriteDataToPurchases(model as PurchaseModel, pharmacyConnection, dataLists);
                        break;
                    default:
                        break;
                }
            }
        }

        public bool DeleteData<T>(T model)
        {
            DatabaseConnectionService.Connect();
            using (SqlConnection pharmacyConnection = DatabaseConnectionService.DbConnection)
            {
                pharmacyConnection.Open();
                using (SqlTransaction transaction = pharmacyConnection.BeginTransaction())
                {
                    try
                    {
                        SqlCommand sqlCommand = new SqlCommand();
                        sqlCommand.Connection = pharmacyConnection;
                        sqlCommand.Transaction = transaction;

                        if (model is MedicationModel)
                        {
                            sqlCommand.CommandText = $"DELETE FROM Medications WHERE Id = {(model as MedicationModel).Id}";
                            sqlCommand.ExecuteNonQuery();
                        }
                        else if (model is WarehouseModel)
                        {
                            sqlCommand.CommandText = $"DELETE FROM Warehouses WHERE Id = {(model as WarehouseModel).Id}";
                            sqlCommand.ExecuteNonQuery();
                        }
                        else if (model is ManufacturerModel)
                        {
                            sqlCommand.CommandText = $"DELETE FROM Manufacturers WHERE Id = {(model as ManufacturerModel).Id}";
                            sqlCommand.ExecuteNonQuery();
                        }
                        else if (model is SaleModel)
                        {
                            sqlCommand.CommandText = $"DELETE FROM Sold_medications WHERE Sale_Id = {(model as SaleModel).Id}";
                            sqlCommand.ExecuteNonQuery();
                            sqlCommand.CommandText = $"DELETE FROM Sales WHERE Id = {(model as SaleModel).Id}";
                            sqlCommand.ExecuteNonQuery();
                        }
                        else if (model is PurchaseModel)
                        {
                            sqlCommand.CommandText = $"DELETE FROM Purchased_medications WHERE Purchase_Id = {(model as PurchaseModel).Id}";
                            sqlCommand.ExecuteNonQuery();
                            sqlCommand.CommandText = $"DELETE FROM Purchases WHERE Id = {(model as PurchaseModel).Id}";
                            sqlCommand.ExecuteNonQuery();
                        }

                        transaction.Commit();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        return false;
                    }
                }
            }
        }

        public bool EditData<T>(T model, DataLists dataLists)
        {
            DatabaseConnectionService.Connect();
            using (SqlConnection pharmacyConnection = DatabaseConnectionService.DbConnection)
            {
                try
                {
                    pharmacyConnection.Open();

                    if (model is MedicationModel)
                    {
                        EditDataInMedications(model as MedicationModel, pharmacyConnection, dataLists);
                    }
                    else if (model is WarehouseModel)
                    {
                        EditDataInWarehouses(model as WarehouseModel, pharmacyConnection);
                    }
                    else if (model is ManufacturerModel)
                    {
                        EditDataInManufacturers(model as ManufacturerModel, pharmacyConnection);
                    }
                    else if (model is SaleModel)
                    {
                        EditDataInSales(model as SaleModel, pharmacyConnection);
                    }
                    else if (model is PurchaseModel)
                    {
                        EditDataInPurchases(model as PurchaseModel, pharmacyConnection);
                    }
                    return true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
            }
        }

        private void EditDataInPurchases(PurchaseModel model, SqlConnection databaseConnection)
        {
            using (SqlCommand sqlCommand =  new SqlCommand("UPDATE Purchases SET Cost = @cost, Date = @date WHERE Id = @id", databaseConnection))
            {
                sqlCommand.Parameters.Clear();
                sqlCommand.Parameters.AddWithValue("@id", model.Id);
                sqlCommand.Parameters.AddWithValue("@cost", model.Cost);
                sqlCommand.Parameters.AddWithValue("@date", model.DeliveryDate);
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

        private void EditDataInSales(SaleModel model, SqlConnection databaseConnection)
        {
            using (SqlCommand sqlCommand = new SqlCommand("UPDATE Sales SET Price = @price, Date = @date WHERE Id = @id", databaseConnection))
            {
                sqlCommand.Parameters.Clear();
                sqlCommand.Parameters.AddWithValue("@id", model.Id);
                sqlCommand.Parameters.AddWithValue("@price", model.Price);
                sqlCommand.Parameters.AddWithValue("@date", model.Date);
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

        private void EditDataInManufacturers(ManufacturerModel model, SqlConnection databaseConnection)
        {
            using (SqlCommand sqlCommand = new SqlCommand("UPDATE Manufacturers SET Name = @name, Country = @country, License = @license WHERE Id = @id", databaseConnection))
            {
                sqlCommand.Parameters.Clear();
                sqlCommand.Parameters.AddWithValue("@id", model.Id);
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

        private void EditDataInWarehouses(WarehouseModel model, SqlConnection databaseConnection)
        {
            using (SqlCommand sqlCommand = new SqlCommand("UPDATE Warehouses SET Name = @name WHERE Id = @id", databaseConnection))
            {
                sqlCommand.Parameters.Clear();
                sqlCommand.Parameters.AddWithValue("@id", model.Id);
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

        private void EditDataInMedications(MedicationModel model, SqlConnection databaseConnection, DataLists dataLists)
        {
            using (SqlCommand sqlCommand = new SqlCommand("UPDATE Medications SET Title = @title, Count = @count, Availability = @availability, Description = @description, Prescription = @prescription, Expiration_date = @expiration_date, Price = @price, Warehouse_Id = @warehouse_Id, MedicationForm_Id = @medicationForm_Id, Manufacturer_Id = @manufacturer_Id, Category_Id = @category_Id WHERE Id = @id", databaseConnection))
            {
                sqlCommand.Parameters.Clear();
                sqlCommand.Parameters.AddWithValue("@id", model.Id);
                sqlCommand.Parameters.AddWithValue("@title", model.Title);
                sqlCommand.Parameters.AddWithValue("@count", model.Count);
                sqlCommand.Parameters.AddWithValue("@price", model.Price);
                sqlCommand.Parameters.AddWithValue("@availability", model.Availability);
                sqlCommand.Parameters.AddWithValue("@description", model.Description);
                sqlCommand.Parameters.AddWithValue("@prescription", model.Prescription);
                sqlCommand.Parameters.AddWithValue("@expiration_date", $"{model.ExpirationDate.Month}/{model.ExpirationDate.Day}/{model.ExpirationDate.Year}");
                sqlCommand.Parameters.AddWithValue("@warehouse_Id", dataLists.WarehousesData.Find(x => x.Name == model.Warehouse).Id);
                sqlCommand.Parameters.AddWithValue("@medicationForm_Id", dataLists.MedicationFormsData.Find(x => x.Form == model.Form).Id);
                sqlCommand.Parameters.AddWithValue("@manufacturer_Id", dataLists.ManufacturersData.Find(x => x.Name == model.Manufacturer).Id);
                sqlCommand.Parameters.AddWithValue("@category_Id", dataLists.CategoriesData.Find(x => x.Name == model.Category).Id);

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

        private void WriteDataToMedications(MedicationModel model, SqlConnection databaseConnection, DataLists dataLists)
        {
            using (SqlCommand sqlCommand = new SqlCommand("INSERT INTO Medications(Id, Title, Count, Availability, Description, Prescription, Expiration_date, Price, Warehouse_Id, MedicationForm_Id, Manufacturer_Id, Category_Id) VALUES (@id, @title, @count, @availability, @description, @prescription, @expiration_date, @price, @warehouse_Id, @medicationForm_Id, @manufacturer_Id, @category_Id)", databaseConnection))
            {
                sqlCommand.Parameters.Clear();
                sqlCommand.Parameters.AddWithValue("@id", model.Id);
                sqlCommand.Parameters.AddWithValue("@title", model.Title);
                sqlCommand.Parameters.AddWithValue("@count", model.Count);
                sqlCommand.Parameters.AddWithValue("@price", model.Price);
                sqlCommand.Parameters.AddWithValue("@availability", model.Availability);
                sqlCommand.Parameters.AddWithValue("@description", model.Description);
                sqlCommand.Parameters.AddWithValue("@prescription", model.Prescription);
                sqlCommand.Parameters.AddWithValue("@expiration_date", $"{model.ExpirationDate.Month}/{model.ExpirationDate.Day}/{model.ExpirationDate.Year}");
                sqlCommand.Parameters.AddWithValue("@warehouse_Id", dataLists.WarehousesData.Find(x => x.Name == model.Warehouse).Id);
                sqlCommand.Parameters.AddWithValue("@medicationForm_Id", dataLists.MedicationFormsData.Find(x => x.Form == model.Form).Id);
                sqlCommand.Parameters.AddWithValue("@manufacturer_Id", dataLists.ManufacturersData.Find(x => x.Name == model.Manufacturer).Id);
                sqlCommand.Parameters.AddWithValue("@category_Id", dataLists.CategoriesData.Find(x => x.Name == model.Category).Id);
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
        private void WriteDataToPurchases(PurchaseModel model, SqlConnection databaseConnection, DataLists dataLists)
        {
            using (SqlCommand sqlCommand = new SqlCommand("INSERT INTO Purchases(Id, Date, Cost, Provider_Id) VALUES (@id, @date, @cost, @provider_Id)", databaseConnection))
            {
                sqlCommand.Parameters.Clear();
                sqlCommand.Parameters.AddWithValue("@id", model.Id);
                sqlCommand.Parameters.AddWithValue("@date", $"{model.DeliveryDate.Month}/{model.DeliveryDate.Day}/{model.DeliveryDate.Year}");
                sqlCommand.Parameters.AddWithValue("@cost", model.Cost);
                sqlCommand.Parameters.AddWithValue("@provider_Id", dataLists.ProvidersData.Find(x => x.Name == model.Provider).Id);
                try
                {
                    sqlCommand.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            using (SqlCommand sqlCommand = new SqlCommand("INSERT INTO Purchased_medications (Medication_Id, Purchase_Id, Count) VALUES (@medication_Id, @purchase_Id, @count)", databaseConnection))
            {
                foreach (var row in dataLists.PurchasedMedicationsData)
                {
                    sqlCommand.Parameters.Clear();
                    sqlCommand.Parameters.AddWithValue("@id", model.Id);
                    sqlCommand.Parameters.AddWithValue("@medication_Id", row.MedicationId);
                    sqlCommand.Parameters.AddWithValue("@purchase_Id", row.PurchasedId);
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
                dataLists.PurchasedMedicationsData.Clear();
            }
        }

        private void WriteDataToSales(SaleModel model, SqlConnection databaseConnection, DataLists dataLists)
        {
            using (SqlCommand sqlCommand = new SqlCommand("INSERT INTO Sales(Id, Price, Date) VALUES (@id, @price, @date)", databaseConnection))
            {
                sqlCommand.Parameters.Clear();
                sqlCommand.Parameters.AddWithValue("@id", model.Id);
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
                foreach(var row in dataLists.SoldMedicationsData)
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
                dataLists.SoldMedicationsData.Clear();
            }
        }

        private void WriteDataToManufacturers(ManufacturerModel model, SqlConnection databaseConnection)
        {
            using (SqlCommand sqlCommand = new SqlCommand("INSERT INTO Manufacturers(Id, Name, Country, License) VALUES (@id, @name, @country, @license)", databaseConnection))
            {
                sqlCommand.Parameters.Clear();
                sqlCommand.Parameters.AddWithValue("@id", model.Id);
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
            using (SqlCommand sqlCommand = new SqlCommand("INSERT INTO Warehouses(Id, Name) VALUES (@id, @name)", databaseConnection))
            {
                sqlCommand.Parameters.Clear();
                sqlCommand.Parameters.AddWithValue("@id", model.Id);
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


        private void ReadMedicationsTableData(SqlConnection databaseConnection, DataLists dataLists)
        {
            MedicationModel medicationRow = null;

            using (SqlCommand sqlCommand = new SqlCommand("SELECT Medications.Id, Medications.Title, Medications.Availability, Medications.Count, Medications.Prescription, Medications.Expiration_date, Medications.Price, Medications.Description, Warehouses.Name AS Warehouse, Medication_forms.Form AS MedicationForm, Manufacturers.Name AS Manufacturer, Categories.Name AS Category FROM Medications LEFT JOIN Warehouses ON Medications.Warehouse_Id = Warehouses.Id LEFT JOIN Medication_forms ON Medications.MedicationForm_Id = Medication_forms.Id LEFT JOIN Manufacturers ON Medications.Manufacturer_Id = Manufacturers.Id LEFT JOIN Categories ON Medications.Category_Id = Categories.Id", databaseConnection))
            {
                try
                {
                    using (SqlDataReader dataReader = sqlCommand.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            if (dataLists.MedicationsData.Any(x => x.Id == dataReader.GetInt32(0)))
                                continue;
                            medicationRow = new MedicationModel
                                (
                                dataReader.GetInt32(0), dataReader.GetString(1), dataReader.GetBoolean(2), dataReader.GetInt32(3), dataReader.GetString(7), dataReader.GetBoolean(4), dataReader.GetDateTime(5), dataReader.GetDecimal(6), dataReader.GetString(8), dataReader.GetString(9), dataReader.GetString(10), dataReader.GetString(11)
                                );
                            dataLists.Add(medicationRow);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
  
            }
               
        }
        private void ReadWarehousesTableData(SqlConnection databaseConnection, DataLists dataLists)
        {
            WarehouseModel warehouseRow = null;

            using (SqlCommand sqlCommand = new SqlCommand("SELECT Warehouses.*, STRING_AGG(Medications.Title, ', ') AS Medications FROM Warehouses LEFT JOIN Medications ON Warehouses.Id = Medications.Warehouse_Id GROUP BY Warehouses.Id, Warehouses.Name", databaseConnection))
            {
                try
                {
                    using (SqlDataReader dataReader = sqlCommand.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            if (dataLists.WarehousesData.Any(x => x.Id == dataReader.GetInt32(0)))
                                continue;
                            warehouseRow = new WarehouseModel
                                (
                                dataReader.GetInt32(0), dataReader.GetString(1)
                                );
                            warehouseRow.Medications = dataReader.GetString(2);
                            dataLists.Add(warehouseRow);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        private void ReadManufacturersTableData(SqlConnection databaseConnection, DataLists dataLists)
        {
            ManufacturerModel manufacturerRow = null;

            using (SqlCommand sqlCommand = new SqlCommand("SELECT Manufacturers.*, STRING_AGG(Medications.Title, ', ') AS Medications FROM Manufacturers LEFT JOIN Medications ON Manufacturers.Id = Medications.Manufacturer_Id GROUP BY Manufacturers.Id, Manufacturers.Name, Manufacturers.Country, Manufacturers.License", databaseConnection))
            {
                try
                {
                    using (SqlDataReader dataReader = sqlCommand.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            if (dataLists.ManufacturersData.Any(x => x.Id == dataReader.GetInt32(0)))
                                continue;
                            manufacturerRow = new ManufacturerModel
                                (
                                dataReader.GetInt32(0), dataReader.GetString(1), dataReader.GetString(2), dataReader.GetString(3)
                                );
                            manufacturerRow.Medications = dataReader.GetString(4);
                            dataLists.Add(manufacturerRow);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        private void ReadSalesTableData(SqlConnection databaseConnection, DataLists dataLists)
        { 
            SaleModel saleRow = null;
            using (SqlCommand sqlCommand = new SqlCommand("SELECT Sales.*, STRING_AGG(Medications.Title, ', ') AS Medications FROM Sales LEFT JOIN Sold_medications ON Sales.Id = Sold_medications.Sale_Id LEFT JOIN Medications ON Sold_medications.Medication_Id = Medications.Id GROUP BY Sales.Id, Sales.Price, Sales.Date", databaseConnection))
            {
                try
                {
                    using (SqlDataReader dataReader = sqlCommand.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            if (dataLists.SalesData.Any(x => x.Id == dataReader.GetInt32(0)))
                                continue;
                            saleRow = new SaleModel
                                (
                                dataReader.GetInt32(0), dataReader.GetDecimal(1), dataReader.GetDateTime(2), dataReader.GetString(3)
                                );
                            dataLists.Add(saleRow);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        private void ReadPurchasesTableData(SqlConnection databaseConnection, DataLists dataLists)
        {
            PurchaseModel purchaseRow = null;

            using (SqlCommand sqlCommand = new SqlCommand("SELECT Purchases.Id, Purchases.Date, Purchases.Cost, Providers.Name AS Provider, STRING_AGG(Medications.Title, ', ') AS Medications FROM Purchases LEFT JOIN Providers ON Purchases.Provider_Id = Providers.Id LEFT JOIN Purchased_medications ON Purchases.Id = Purchased_medications.Purchase_Id LEFT JOIN Medications ON Purchased_medications.Medication_Id = Medications.Id GROUP BY Purchases.Id, Purchases.Date, Purchases.Cost, Providers.Name", databaseConnection))
            {
                try
                {
                    using (SqlDataReader dataReader = sqlCommand.ExecuteReader())
                    { 
                    while (dataReader.Read())
                    {
                        if (dataLists.PurchasesData.Any(x => x.Id == dataReader.GetInt32(0)))
                            continue;
                        purchaseRow = new PurchaseModel
                            (
                            dataReader.GetInt32(0), dataReader.GetDateTime(1), dataReader.GetDecimal(2), dataReader.GetString(3), dataReader.GetString(4)
                            );
                           
                        dataLists.Add(purchaseRow);
                    }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        private void ReadCategoriesTableData(SqlConnection databaseConnection, DataLists dataLists)
        {
            CategoryModel categoryRow = null;

            using (SqlCommand sqlCommand = new SqlCommand("SELECT Categories.* FROM Categories", databaseConnection)) {
                try
                {
                    using (SqlDataReader dataReader = sqlCommand.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            if (dataLists.CategoriesData.Any(x => x.Id == dataReader.GetInt32(0)))
                                continue;
                            categoryRow = new CategoryModel
                                (
                                dataReader.GetInt32(0), dataReader.GetString(1)
                                );
                            dataLists.Add(categoryRow);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        private void ReadMedicationFormsTableData(SqlConnection databaseConnection, DataLists dataLists)
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
                            if(dataLists.MedicationFormsData.Any(x => x.Id == dataReader.GetInt32(0)))
                                continue;
                            medicationFormRow = new MedicationFormModel
                                (
                                dataReader.GetInt32(0), dataReader.GetString(1)
                                );
                            dataLists.Add(medicationFormRow);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        private void ReadProvidersTableData(SqlConnection databaseConnection, DataLists dataLists)
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
                            if(dataLists.ProvidersData.Any(x=>x.Id == dataReader.GetInt32(0)))
                                continue;
                            providerRow = new ProviderModel
                                (
                                dataReader.GetInt32(0), dataReader.GetString(1)
                                );
                            dataLists.Add(providerRow);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        public int GetLastId(SelectedTable selectedTable)
        {
            DatabaseConnectionService.Connect();
            using (SqlConnection connection = DatabaseConnectionService.DbConnection)
            {
                using (SqlCommand command = new SqlCommand($"SELECT MAX(Id) FROM {selectedTable}", connection))
                {
                    try
                    {
                        connection.Open();
                        return (int)command.ExecuteScalar();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                        return 0;
                    }
                }
            }
        }
    }
}