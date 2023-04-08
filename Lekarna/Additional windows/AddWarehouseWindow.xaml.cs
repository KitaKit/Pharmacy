using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Pharmacy.Additional_windows
{
    /// <summary>
    /// Interaction logic for AddWarehouseWindow.xaml
    /// </summary>
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
