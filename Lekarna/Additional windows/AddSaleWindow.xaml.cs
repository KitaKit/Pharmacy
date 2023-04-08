using Pharmacy.Models;
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

            int nextId = DataLists.SalesData.Max(x => x.Id) + 1;
            string medication = null;
            List<string> checkedMedications = new List<string>();
            foreach (var item in medicationsWrapPanel.Children)
            {
                if (item is CheckBox && (item as CheckBox).IsChecked == true)
                {
                    medication = (item as CheckBox).Content.ToString();
                    checkedMedications.Add(medication);
                    DataLists.Add(new SoldMedicationModel(nextId, DataLists.MedicationsData.Find(x=>x.Title == medication).Id, int.Parse((medicationsWrapPanel.Children[medicationsWrapPanel.Children.IndexOf(item as CheckBox) + 1] as TextBox).Text)));
                }
            }
            string medications = string.Join(", ", checkedMedications);

            SaleModel newSale = new SaleModel
                (
                nextId, decimal.Parse(priceTextBox.Text), (DateTime)dateDatePicker.SelectedDate, medications
                );

            DataSave.SaveNewData(newSale, SelectedTable.Sales);
            Close();
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
