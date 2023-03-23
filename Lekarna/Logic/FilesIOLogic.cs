using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Common;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using CsvHelper;
using CsvHelper.Configuration;

//Здесь будет описана логика работы с CSV файлом

//Logika práce so súborom CSV bude popísaná tu

namespace Pharmacy
{
    public enum SelectedTable : int
    {
        Medications = 0,
        Warehouses = 1,
        Manufacturers = 2,
        Sales = 3,
        Purchases = 4, 
        All = 5
    }
    public class FilesIOLogic
    {
        private FilesConnectionService _fileConnection;
        private string _path;
        private SelectedTable _selectedTable;
        public string Path { get { return _path; } }
        private string _name;
        public string Name { get { return _name; } }
        public FilesIOLogic(FileConnectionType connectionType)
        {
            _fileConnection = new FilesConnectionService(connectionType);
            _path = _fileConnection.FilePath;
            _name = System.IO.Path.GetFileNameWithoutExtension(_path);
        }

        public FilesIOLogic(FileConnectionType connectionType, SelectedTable selectedTable) : this (connectionType)
        {
            _selectedTable = selectedTable;
        }

        public void ReadData(DataLists dataLists)
        {
            MessageBoxResult messageBoxResult = MessageBox.Show($"Do you want to load data from {_path} to {_selectedTable}", "Are you sure?", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                switch (_selectedTable)
                {
                    case SelectedTable.Medications:
                        dataLists.MedicationsData = GetDataFromFile(new List<MedicationModel>(), new MedicationClassMap());
                        break;

                    case SelectedTable.Warehouses:
                        dataLists.WarehousesData = GetDataFromFile(new List<WarehouseModel>(), new WarehouseClassMap());
                        break;

                    case SelectedTable.Manufacturers:
                        dataLists.ManufacturersData = GetDataFromFile(new List<ManufacturerModel>(), new ManufacturerClassMap());
                        break;

                    case SelectedTable.Sales:
                        dataLists.SalesData = GetDataFromFile(new List<SaleModel>(), new SaleClassMap());
                        break;

                    case SelectedTable.Purchases:
                        dataLists.PurchasesData = GetDataFromFile(new List<PurchaseModel>(), new PurchaseClassMap());
                        break;
                }
            }
        }
        public void WriteDataToNew(DataLists dataLists)
        {
            MessageBoxResult messageBoxResult = MessageBox.Show($"Do you want to save data from {_selectedTable} to {_path}", "Are you sure?", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                switch (_selectedTable)
                {
                    case SelectedTable.Medications:
                        if (!dataLists.IsEmpty(dataLists.MedicationsData))
                        {
                            WriteDataToNewFile(dataLists.MedicationsData, new MedicationClassMap());
                            break;
                        }
                        break;

                    case SelectedTable.Warehouses:
                        if (!dataLists.IsEmpty(dataLists.WarehousesData))
                        {
                            WriteDataToNewFile(dataLists.WarehousesData, new WarehouseClassMap());
                            break;
                        }
                        break;

                    case SelectedTable.Manufacturers:
                        if (!dataLists.IsEmpty(dataLists.ManufacturersData))
                        {
                            WriteDataToNewFile(dataLists.ManufacturersData, new ManufacturerClassMap());
                            break;
                        }
                        break;

                    case SelectedTable.Sales:
                        if (!dataLists.IsEmpty(dataLists.SalesData))
                        {
                            WriteDataToNewFile(dataLists.SalesData, new SaleClassMap());
                            break;
                        }
                        break;

                    case SelectedTable.Purchases:
                        if (!dataLists.IsEmpty(dataLists.PurchasesData))
                        {
                            WriteDataToNewFile(dataLists.PurchasesData, new PurchaseClassMap());
                            break;
                        }
                        break;
                }
            }
        }
        private void WriteDataToNewFile<T, TMap>(List<T> dataList, TMap classMap) where TMap : ClassMap<T>
        {

            using (StreamWriter writer = new StreamWriter(_path))
            {
                using (CsvWriter csvWriter = new CsvWriter(writer, CultureInfo.InvariantCulture))
                {
                    csvWriter.Context.RegisterClassMap(classMap);
                    csvWriter.WriteRecords(dataList);
                }
            }
        }

        private List<T> GetDataFromFile<T, TMap>(List<T> dataList, TMap classMap) where TMap : ClassMap<T>
        {
            using (StreamReader reader = new StreamReader(_path))
            {
                using (CsvReader csvReader = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    try
                    {
                        csvReader.Context.RegisterClassMap(classMap);
                        return csvReader.GetRecords<T>().ToList();
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("The wrong file was selected", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        return null;
                    }
                }
            }
        }

        private void AppendToFile()
        {

        }
    }
}


/*======================================================THERE ARE JUST TEST OPTIONS HERE======================================================*/
/*public void ReadFile(string filePath, DataLists dataLists)
        {
            using (StreamReader reader = new StreamReader(filePath))
            {
                using (CsvReader csvReader = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    csvReader.Context.RegisterClassMap<MedicationClassMap>();
                    dataLists.MedicationsData = csvReader.GetRecords<MedicationModel>().ToList();

                }
            }
        }*/