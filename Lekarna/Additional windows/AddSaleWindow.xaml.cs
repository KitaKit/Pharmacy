﻿using System;
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
            SaleModel newSale = new SaleModel
                (
                decimal.Parse(priceTextBox.Text), dateDatePicker.SelectedDate.Value
                );
            DataSaveService.SaveNewData(newSale, SelectedTable.Sales);
        }
    }
}
