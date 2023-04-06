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
        public AddWarehouseWindow()
        {
            InitializeComponent();
        }
        //private void addWarehouseWindow_Loaded(object sender, RoutedEventArgs e)
        //{
        //    foreach (var row in DataLists.MedicationsData)
        //    {
        //        CheckBox checkBox = new CheckBox();
        //        checkBox.Content = row.Title;
        //        checkBox.Margin = new Thickness(1, 1, 1, 1);
        //        //medicationsWrapPanel.Children.Add(checkBox);
        //    }
        //}

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            //в начале будет проверка на валидность данных (в чате с ботом есть как примерно)
            WarehouseModel newWarehouse = new WarehouseModel();
            newWarehouse.Name = nameTextBox.Text;
            DataSave.SaveNewData(newWarehouse, SelectedTable.Warehouses);
        }
    }
}
