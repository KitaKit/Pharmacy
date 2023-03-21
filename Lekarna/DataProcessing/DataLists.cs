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

        public List<MedicationModel> MedicationsData => _medicationsData;
        public List<WarehouseModel> WarehousesData => _warehousesData;
        public List<ManufacturerModel> ManufacturersData => _manufacturersData;
        public List<SaleModel> SalesData => _salesData;
        public List<PurchaseModel> PurchasesData => _purchasesData;

        public void AddToDataList <T>(T data, List<T> list)
        {
            list.Add(data);
        }

        public void ShowDataToDataGrid<T>(DataGrid dataGrid, List<T> dataList) //метод для отображения данных в окне приложения, мы присваиваем каждой секции свой источник данных для отображения
                                                                               //metoda pro zobrazení dat v okně aplikace, každé sekci přiřadíme jiný zdroj dat, který se má zobrazit.
        {
            dataGrid.ItemsSource = dataList;
        }
    }
}
