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
using System.Windows.Navigation;
using System.Windows.Shapes;

//Здесь будет описана логика основных взаимодействий с окном программы и его содержимым

//Tady bude popsána logika hlavních interakcí s oknem programu a jeho obsahem.

namespace Pharmacy
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void menuItemLoadFromDataBase_Click(object sender, RoutedEventArgs e)
        {
            DatabaseLogic dataBase = new DatabaseLogic();
            dataBase.LoadData();
            dataGridMedications.ItemsSource = dataBase.MedicationsData;
            dataGridWarehouses.ItemsSource = dataBase.WarehousesData;
            dataGridManufacturers.ItemsSource = dataBase.ManufacturersData;
            dataGridSales.ItemsSource = dataBase.SalesData;
            dataGridPurchases.ItemsSource = dataBase.PurchasesData;
        }
    }
}
