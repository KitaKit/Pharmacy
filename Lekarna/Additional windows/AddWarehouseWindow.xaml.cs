using System.Linq;
using System.Windows;
using System.Windows.Controls;
namespace Pharmacy.Additional_windows
{
    public partial class AddWarehouseWindow : Window
    {
        private DataLists _dataLists = null;
        public AddWarehouseWindow(DataLists dataLists)
        {
            InitializeComponent();
            _dataLists = dataLists;
            DataContext = new WarehouseModel();
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            if(Validation.GetHasError(nameTextBox) || string.IsNullOrEmpty(nameTextBox.Text))
                return;

            WarehouseModel newWarehouse = new WarehouseModel(_dataLists.WarehousesData.Max(x => x.Id) + 1, nameTextBox.Text);
           
            ChangeData.SaveNew(newWarehouse, SelectedTable.Warehouses, _dataLists);
            Close();
        }
    }
}
