using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
//Здесь будет описана логика работы с базой данных
namespace Pharmacy
{
    public class DatabaseLogic
    {
        private List<MedicationModel> _medicationsData = new List<MedicationModel>();
        private List<WarehouseModel> _warehousesData = new List<WarehouseModel>();
        private List<ManufacturerModel> _manufacturersData = new List<ManufacturerModel>();
        private List<SaleModel> _salesData = new List<SaleModel>();
        private List<PurchaseModel> _purchasesData = new List<PurchaseModel>();

        public List<MedicationModel> MedicationsData { get { return _medicationsData; } }
        public List<WarehouseModel> WarehousesData { get { return _warehousesData; } }
        public List<ManufacturerModel> ManufacturersData { get { return _manufacturersData; } }
        public List<SaleModel> SalesData { get { return _salesData; } }
        public List<PurchaseModel> PurchasesData { get { return _purchasesData; } }

        public void LoadData()
        {
            SqlConnection pharmacyConnection = DatabaseConnectionService.DbConnection;

            try { pharmacyConnection.Open(); }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Environment.Exit(1);
            }

            ReadMedicationsTableData(pharmacyConnection);
            ReadWarehousesTableData(pharmacyConnection);
            ReadManufacturersTableData(pharmacyConnection);
            ReadSalesTableData(pharmacyConnection);
            ReadPurchasesTableData(pharmacyConnection);

            pharmacyConnection.Close();
        }

        private void ReadMedicationsTableData(SqlConnection databaseConnection)
        {
            SqlDataReader dataReader = null;
            MedicationModel medicationRow = null;

            try
            {
                SqlCommand sqlCommand = new SqlCommand("SELECT Id_Medication, Title, Availability, Count, Prescription, Expiration_date, Price, Description FROM Medications", databaseConnection);
                dataReader = sqlCommand.ExecuteReader();
                while (dataReader.Read()) 
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