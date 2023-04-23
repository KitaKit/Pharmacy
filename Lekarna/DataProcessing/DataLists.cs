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
    public class DataLists
    {
        private List<MedicationModel> _medicationsData = new List<MedicationModel>();
        private List<WarehouseModel> _warehousesData = new List<WarehouseModel>();
        private List<ManufacturerModel> _manufacturersData = new List<ManufacturerModel>();
        private List<SaleModel> _salesData = new List<SaleModel>();
        private List<PurchaseModel> _purchasesData = new List<PurchaseModel>();
        private List<CategoryModel> _categoriesData = new List<CategoryModel>();
        private List<MedicationFormModel> _medicationFormsData = new List<MedicationFormModel>();
        private List<ProviderModel> _providersData = new List<ProviderModel>();
        private List<SoldMedicationModel> _soldMedicationsData = new List<SoldMedicationModel>();
        private List<PurchasedMedicationModel> _purchasedMedicationsData = new List<PurchasedMedicationModel>();

        public List<MedicationModel> MedicationsData { get { return _medicationsData; } set { _medicationsData = value; } }
        public List<WarehouseModel> WarehousesData { get { return _warehousesData; } set { _warehousesData = value; } }
        public List<ManufacturerModel> ManufacturersData { get { return _manufacturersData; } set { _manufacturersData = value; } }
        public List<SaleModel> SalesData { get { return _salesData; } set { _salesData = value; } }
        public List<PurchaseModel> PurchasesData { get { return _purchasesData; } set { _purchasesData = value; } }
        public List<CategoryModel> CategoriesData { get { return _categoriesData; } set { _categoriesData = value; } }
        public List<MedicationFormModel>MedicationFormsData { get { return _medicationFormsData; } set { _medicationFormsData = value; } }
        public List<ProviderModel> ProvidersData { get { return _providersData; } set { _providersData = value; } }
        public List<SoldMedicationModel> SoldMedicationsData { get { return _soldMedicationsData; } set { _soldMedicationsData = value; } }
        public List<PurchasedMedicationModel> PurchasedMedicationsData { get { return _purchasedMedicationsData; } set { _purchasedMedicationsData = value; } }

        public void Add<T>(T data)
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
        public void Delete<T>(T data)
        {
            if (data is MedicationModel)
            {
                var model = data as MedicationModel;
                MedicationsData.Remove(model);

                string reference = WarehousesData.Find(x => x.Name == model.Warehouse).Medications;
                WarehousesData.Find(x => x.Name == model.Warehouse).Medications = reference.Remove(reference.IndexOf(model.Title), model.Title.Length).TrimStart(',', ' ');

                reference = ManufacturersData.Find(x => x.Name == model.Manufacturer).Medications;
                ManufacturersData.Find(x => x.Name == model.Manufacturer).Medications = reference.Remove(reference.IndexOf(model.Title), model.Title.Length).TrimStart(',', ' ');
            }
            else if (data is WarehouseModel)
                WarehousesData.Remove(data as WarehouseModel);
            else if (data is ManufacturerModel)
                ManufacturersData.Remove(data as ManufacturerModel);
            else if (data is SaleModel)
                SalesData.Remove(data as SaleModel);
            else if (data is PurchaseModel)
                PurchasesData.Remove(data as PurchaseModel);
            else if (data is CategoryModel)
                CategoriesData.Remove(data as CategoryModel);
            else if (data is MedicationFormModel)
                MedicationFormsData.Remove(data as MedicationFormModel);
            else if (data is ProviderModel)
                ProvidersData.Remove(data as ProviderModel);
            else if (data is SoldMedicationModel)
                SoldMedicationsData.Remove(data as SoldMedicationModel);
            else if (data is PurchasedMedicationModel)
                PurchasedMedicationsData.Remove(data as PurchasedMedicationModel);
        }
    }
}
