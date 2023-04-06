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
            //PurchaseModel newPurchase = new PurchaseModel
            //    (
            //    deliveryDateDatePicker.SelectedDate.Value, int.Parse(countTextBox.Text), decimal.Parse(costTextBox.Text)
            //    );
            PurchaseModel newPurchase = new PurchaseModel();
            newPurchase.DeliveryDate = (DateTime)deliveryDateDatePicker.SelectedDate;
            newPurchase.Cost = decimal.Parse (costTextBox.Text);
            foreach(var item in medicationsWrapPanel.Children)
            {
                if (item is CheckBox && (item as CheckBox).IsChecked == true)
                    newPurchase.Medications = string.Concat((item as CheckBox).Content.ToString(), ", ");
            }
            newPurchase.Provider = providerComboBox.SelectedValue.ToString();
            newPurchase.Id = DataLists.PurchasesData.Max(x => x.Id) + 1;
            DataSave.SaveNewData(newPurchase, SelectedTable.Purchases);
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
            }
        }
    }
}
