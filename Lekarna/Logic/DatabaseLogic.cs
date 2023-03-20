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

        public List<MedicationModel> MedicationsData { get; set; }
        public List<WarehouseModel> WarehousesData { get; set; }
        public List<ManufacturerModel> ManufacturersData { get; set; }
        public List<SaleModel> SalesData { get; set; }
        public List<PurchaseModel> PurchasesData { get; set; }

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
                        dataReader.GetInt32(0), dataReader.GetString(1), dataReader.GetBoolean(3), dataReader.GetInt32(2), dataReader.GetString(4), dataReader.GetBoolean(5), dataReader.GetDateTime(6), dataReader.GetFloat(7)
                        );
                   MedicationsData.Add(medicationRow);
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