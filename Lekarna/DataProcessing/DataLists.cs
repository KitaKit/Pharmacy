using System;
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

        public List<MedicationModel> MedicationsData { get { return _medicationsData; } set { _medicationsData = value; } }
        public List<WarehouseModel> WarehousesData { get { return _warehousesData; } set { _warehousesData = value; } }
        public List<ManufacturerModel> ManufacturersData { get { return _manufacturersData; } set { _manufacturersData = value; } }
        public List<SaleModel> SalesData { get { return _salesData; } set { _salesData = value; } }
        public List<PurchaseModel> PurchasesData { get { return _purchasesData; } set { _purchasesData = value; } }

        public void AddToDataList <T>(T data, List<T> dataList)
        {
            dataList.Add(data);
        }

        public void ShowDataToDataGrid<T>(DataGrid dataGrid, List<T> dataList) //метод для отображения данных в окне приложения, мы присваиваем каждой секции свой источник данных для отображения
                                                                               //metoda pro zobrazení dat v okně aplikace, každé sekci přiřadíme jiný zdroj dat, který se má zobrazit.
        {
            dataGrid.ItemsSource = dataList;
        }

        public bool IsEmpty<T>(List<T> dataList)
        {
            return !dataList.Any();
        }
    }
}
