using Pharmacy.Models;
using System;
using System.CodeDom;
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
        private static List<ProviderModel> _providersData = new List<ProviderModel>();
        private static List<SoldMedicationModel> _soldMedicationsData = new List<SoldMedicationModel>();
        private static List<PurchasedMedicationModel> _purchasedMedicationsData = new List<PurchasedMedicationModel>();

        public static List<MedicationModel> MedicationsData { get { return _medicationsData; } set { _medicationsData = value; } }
        public static List<WarehouseModel> WarehousesData { get { return _warehousesData; } set { _warehousesData = value; } }
        public static List<ManufacturerModel> ManufacturersData { get { return _manufacturersData; } set { _manufacturersData = value; } }
        public static List<SaleModel> SalesData { get { return _salesData; } set { _salesData = value; } }
        public static List<PurchaseModel> PurchasesData { get { return _purchasesData; } set { _purchasesData = value; } }
        public static List<CategoryModel> CategoriesData { get { return _categoriesData; } set { _categoriesData = value; } }
        public static List<MedicationFormModel>MedicationFormsData { get { return _medicationFormsData; } set { _medicationFormsData = value; } }
        public static List<ProviderModel> ProvidersData { get { return _providersData; } set { _providersData = value; } }
        public static List<SoldMedicationModel> SoldMedicationsData { get { return _soldMedicationsData; } set { _soldMedicationsData = value; } }
        public static List<PurchasedMedicationModel> PurchasedMedicationsData { get { return _purchasedMedicationsData; } set { _purchasedMedicationsData = value; } }

        static public void Add <T>(T data)
        {
            if (data is MedicationModel)
                MedicationsData.Add(data as MedicationModel);
            else if (data is WarehouseModel)
                WarehousesData.Add(data as WarehouseModel);
            else if (data is ManufacturerModel)
                ManufacturersData.Add(data as ManufacturerModel);
            else if (data is SaleModel)
                SalesData.Add(data as SaleModel);
            else if (data is PurchaseModel)
                PurchasesData.Add(data as PurchaseModel);
            else if (data is CategoryModel)
                CategoriesData.Add(data as CategoryModel);
            else if (data is MedicationFormModel)
                MedicationFormsData.Add(data as MedicationFormModel);
            else if (data is ProviderModel)
                ProvidersData.Add(data as ProviderModel);
            else if (data is SoldMedicationModel)
                SoldMedicationsData.Add(data as SoldMedicationModel);
            else if (data is PurchasedMedicationModel)
                PurchasedMedicationsData.Add(data as PurchasedMedicationModel);
        }

        static public bool IsEmpty<T>(List<T> dataList)
        {
            return !dataList.Any();
        }
    }
}
