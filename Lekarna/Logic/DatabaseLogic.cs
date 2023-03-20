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

        public bool LoadData(TabControl tabControl)
        {
            SqlConnection pharmacyConnection = DatabaseConnectionService.DbConnection;

            try { pharmacyConnection.Open(); }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Environment.Exit(1);
            }

            ReadMedicationsTableData(pharmacyConnection);
            //ReadWarehousesTableData(pharmacyConnection);
            //ReadManufacturersTableData(pharmacyConnection);
            //ReadSalesTableData(pharmacyConnection);
            //ReadPurchasesTableData(pharmacyConnection);

            pharmacyConnection.Close();
            return true;
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
        }
        private void ReadManufacturersTableData(SqlConnection databaseConnection)
        {
            SqlDataReader dataReader = null;
            ManufacturerModel manufacturerRow = null;
        }
        private void ReadSalesTableData(SqlConnection databaseConnection)
        {
            SqlDataReader dataReader = null;
            SaleModel saleRow = null;
        }
        private void ReadPurchasesTableData(SqlConnection databaseConnection)
        {
            SqlDataReader dataReader = null;
            PurchaseModel purchaseRow = null;
        }
    }
}