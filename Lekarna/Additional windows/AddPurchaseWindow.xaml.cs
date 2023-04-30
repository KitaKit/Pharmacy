using Pharmacy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
namespace Pharmacy.Additional_windows
{
    public partial class AddPurchaseWindow : Window
    {
        private DataLists _dataLists = null;
        private DataLists _changedData = null;
        private DataLists _deletedData = null;
        public AddPurchaseWindow(DataLists dataLists, DataLists changedData, DataLists deletedData)
        {
            InitializeComponent();
            _dataLists = dataLists;
            _changedData = changedData;
            _deletedData = deletedData;
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            if (!WrapPanelCustomValidation.IsValid(medicationsWrapPanel) || Validation.GetHasError(deliveryDateDatePicker) || deliveryDateDatePicker.SelectedDate == null || providerComboBox.SelectedItem == null)
                return;

            DatabaseIOLogic db = new DatabaseIOLogic();
            int nextId = db.GetLastId(SelectedTable.Purchases) + 1;
            string medication = null;
            decimal cost = 0;
            List<string> checkedMedications = new List<string>();
            MedicationModel medicationModel = null;
            int medicationCount = 0;

            var checkBoxes = medicationsWrapPanel.Children.OfType<CheckBox>().Where(x => x.IsChecked == true);
            foreach (var item in checkBoxes)
            {
                if (item is CheckBox && item.IsChecked == true)
                {
                    medication = item.Content.ToString();
                    checkedMedications.Add(medication);
                    medicationModel = _dataLists.MedicationsData.Find(x => x.Title == medication);
                    medicationCount = int.Parse((medicationsWrapPanel.Children[medicationsWrapPanel.Children.IndexOf(item) + 1] as TextBox).Text);
                    if (medicationModel.Count == 0)
                        medicationModel.Availability = true;
                    medicationModel.Count += medicationCount;
                    _dataLists.Add(new PurchasedMedicationModel(medicationModel.Id, nextId, medicationCount));
                    _changedData.Add(medicationModel);
                    cost += (medicationModel.Price*medicationCount);
                }
            }
            string medications = string.Join(", ", checkedMedications);

            PurchaseModel newPurchase = new PurchaseModel
                (
                nextId, (DateTime)deliveryDateDatePicker.SelectedDate, cost, providerComboBox.SelectedValue.ToString(), medications
                );

            ChangeData.SaveAll(_deletedData, _changedData, _dataLists);
            ChangeData.SaveNew(newPurchase, SelectedTable.Purchases, _dataLists);
            Close();
        }

        private void addPurchaseWindow_Loaded(object sender, RoutedEventArgs e)
        {
            foreach (var row in _dataLists.MedicationsData)
            {
                DataContext = new PurchaseModel(DateTime.Now, 0, null, null);

                CheckBox checkBox = new CheckBox();
                checkBox.Content = row.Title;
                checkBox.Margin = new Thickness(1, 1, 1, 1);
                medicationsWrapPanel.Children.Add(checkBox);

                TextBox textBox = new TextBox();
                textBox.Width = 50;
                textBox.Margin = new Thickness(1, 1, 1, 1);
                medicationsWrapPanel.Children.Add(textBox);

                providerComboBox.ItemsSource = _dataLists.ProvidersData.Select(x => x.Name);
            }
        }
    }
}
