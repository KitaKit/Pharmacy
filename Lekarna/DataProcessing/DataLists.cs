using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Pharmacy
{
    public static class DataLists
    {
        private static List<MedicationModel> _medicationsData = new List<MedicationModel>();
        private static List<WarehouseModel> _warehousesData = new List<WarehouseModel>();
        private static List<ManufacturerModel> _manufacturersData = new List<ManufacturerModel>();
        private static List<SaleModel> _salesData = new List<SaleModel>();
        private static List<PurchaseModel> _purchasesData = new List<PurchaseModel>();
        private static List<CategoryModel> _categoriesData = new List<CategoryModel>();
        private static List<MedicationFormModel> _medicationFormsData = new List<MedicationFormModel>();

        public static List<MedicationModel> MedicationsData { get { return _medicationsData; } set { _medicationsData = value; } }
        public static List<WarehouseModel> WarehousesData { get { return _warehousesData; } set { _warehousesData = value; } }
        public static List<ManufacturerModel> ManufacturersData { get { return _manufacturersData; } set { _manufacturersData = value; } }
        public static List<SaleModel> SalesData { get { return _salesData; } set { _salesData = value; } }
        public static List<PurchaseModel> PurchasesData { get { return _purchasesData; } set { _purchasesData = value; } }
        public static List<CategoryModel> CategoriesData { get { return _categoriesData; } set { _categoriesData = value; } }
        public static List<MedicationFormModel>MedicationFormsData { get { return _medicationFormsData; } set { _medicationFormsData = value; } }

        static public void AddToDataList <T>(T data, List<T> dataList)
        {
            dataList.Add(data);
        }

        static public bool IsEmpty<T>(List<T> dataList)
        {
            return !dataList.Any();
        }
    }
}
