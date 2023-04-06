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
    /// Interaction logic for AddSaleWindow.xaml
    /// </summary>
    public partial class AddSaleWindow : Window
    {
        public AddSaleWindow()
        {
            InitializeComponent();
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            //в начале будет проверка на валидность данных (в чате с ботом есть как примерно)

            string checkedMedications = string.Empty;
            foreach (var item in medicationsWrapPanel.Children)
            {
                if (item is CheckBox && (item as CheckBox).IsChecked == true)
                    checkedMedications = string.Concat((item as CheckBox).Content.ToString(), ", ");
            }

            SaleModel newSale = new SaleModel
                (
                DataLists.SalesData.Max(x => x.Id) + 1, decimal.Parse(priceTextBox.Text), (DateTime)dateDatePicker.SelectedDate, checkedMedications
                );

            DataSave.SaveNewData(newSale, SelectedTable.Sales);
        }

        private void addSaleWindow_Loaded(object sender, RoutedEventArgs e)
        {
            foreach (var row in DataLists.MedicationsData)
            {
                CheckBox checkBox = new CheckBox();
                checkBox.Content = row.Title;
                checkBox.Margin = new Thickness(1, 1, 1, 1);
                medicationsWrapPanel.Children.Add(checkBox);

                TextBox textBox = new TextBox();
                textBox.Width = 30;
                textBox.Margin = new Thickness(1, 1, 5, 1);
                medicationsWrapPanel.Children.Add(textBox);
            }
        }
    }
}
