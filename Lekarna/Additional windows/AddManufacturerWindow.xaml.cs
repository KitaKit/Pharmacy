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
    /// Interaction logic for AddManufacturerWindow.xaml
    /// </summary>
    public partial class AddManufacturerWindow : Window
    {
        public AddManufacturerWindow()
        {
            InitializeComponent();
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            // в начале будет проверка на валидность данных(в чате с ботом есть как примерно)

            ManufacturerModel newManufacturer = new ManufacturerModel
                (
                DataLists.ManufacturersData.Max(x => x.Id) + 1, nameTextBox.Text, countryTextBox.Text, licenseTextBox.Text
                );

            DataSave.SaveNewData(newManufacturer, SelectedTable.Manufacturers);
            Close();
        }
    }
}
