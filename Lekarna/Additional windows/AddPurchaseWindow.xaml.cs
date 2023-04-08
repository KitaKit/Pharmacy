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
    /// Interaction logic for AddPurchaseWindow.xaml
    /// </summary>
    public partial class AddPurchaseWindow : Window
    {
        public AddPurchaseWindow()
        {
            InitializeComponent();
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            //в начале будет проверка на валидность данных (в чате с ботом есть как примерно)

            int nextId = DataLists.PurchasesData.Max(x => x.Id) + 1;
            string medication = null;
            List<string> checkedMedications = new List<string>();
            foreach (var item in medicationsWrapPanel.Children)
            {
                if (item is CheckBox && (item as CheckBox).IsChecked == true)
                {
                    medication = (item as CheckBox).Content.ToString();
                    checkedMedications.Add(medication);
                    DataLists.Add(new PurchasedMedicationModel(DataLists.MedicationsData.Find(x => x.Title == medication).Id, nextId, int.Parse((medicationsWrapPanel.Children[medicationsWrapPanel.Children.IndexOf(item as CheckBox) + 1] as TextBox).Text)));
                }
            }
            string medications = string.Join(", ", checkedMedications);

            PurchaseModel newPurchase = new PurchaseModel
                (
                DataLists.PurchasesData.Max(x => x.Id) + 1, (DateTime)deliveryDateDatePicker.SelectedDate, decimal.Parse(costTextBox.Text), providerComboBox.SelectedValue.ToString(), medications
                );

            DataSave.SaveNewData(newPurchase, SelectedTable.Purchases);
            Close();
        }

        private void addPurchaseWindow_Loaded(object sender, RoutedEventArgs e)
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

                providerComboBox.ItemsSource = DataLists.ProvidersData.Select(x => x.Name);
            }
        }
    }
}
