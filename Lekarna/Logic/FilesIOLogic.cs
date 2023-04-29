using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows;
using CsvHelper;
using CsvHelper.Configuration;

namespace Pharmacy
{
    public class FilesIOLogic
    {
        private FilesConnectionService _fileConnection;
        private string _path;
        private SelectedTable _selectedTable;
        public SelectedTable SelectedTable { get { return _selectedTable; } }
        public string Path { get { return _path; } }
        private string _name;
        public string Name { get { return _name; } }
        public FilesIOLogic(FileConnectionType connectionType)
        {
            _fileConnection = new FilesConnectionService(connectionType);
            _path = _fileConnection.FilePath;
            _name = System.IO.Path.GetFileNameWithoutExtension(_path);
        }

        public FilesIOLogic(FileConnectionType connectionType, SelectedTable selectedTable) : this(connectionType)
        {
            _selectedTable = selectedTable;
        }

        public void ReadData(DataLists dataLists)
        {
            switch (_selectedTable)
            {
                case SelectedTable.Medications:
                    dataLists.MedicationsData.AddRange(GetDataFromFile(new List<MedicationModel>(), new MedicationClassMap()));
                    break;

                case SelectedTable.Warehouses:
                    dataLists.WarehousesData.AddRange(GetDataFromFile(new List<WarehouseModel>(), new WarehouseClassMap()));
                    break;

                case SelectedTable.Manufacturers:
                    dataLists.ManufacturersData.AddRange(GetDataFromFile(new List<ManufacturerModel>(), new ManufacturerClassMap()));
                    break;

                case SelectedTable.Sales:
                    dataLists.SalesData.AddRange(GetDataFromFile(new List<SaleModel>(), new SaleClassMap()));
                    break;

                case SelectedTable.Purchases:
                    dataLists.PurchasesData.AddRange(GetDataFromFile(new List<PurchaseModel>(), new PurchaseClassMap()));
                    break;
            }
        }
        public void WriteDataToNew(DataLists dataLists)
        {
            switch (_selectedTable)
            {
                case SelectedTable.Medications:
                    if (dataLists.MedicationsData.Any())
                    {
                        WriteDataToFile(dataLists.MedicationsData, new MedicationClassMap());
                        break;
                    }
                    break;

                case SelectedTable.Warehouses:
                    if (dataLists.WarehousesData.Any())
                    {
                        WriteDataToFile(dataLists.WarehousesData, new WarehouseClassMap());
                        break;
                    }
                    break;

                case SelectedTable.Manufacturers:
                    if (dataLists.ManufacturersData.Any())
                    {
                        WriteDataToFile(dataLists.ManufacturersData, new ManufacturerClassMap());
                        break;
                    }
                    break;

                case SelectedTable.Sales:
                    if (dataLists.SalesData.Any())
                    {
                        WriteDataToFile(dataLists.SalesData, new SaleClassMap());
                        break;
                    }
                    break;
                case SelectedTable.Purchases:
                    if (dataLists.PurchasesData.Any())
                    {
                        WriteDataToFile(dataLists.PurchasesData, new PurchaseClassMap());
                        break;
                    }
                    break;
            }
        }
        public void AppendData<T>(T model)
        {
            switch (_selectedTable)
            {
                case SelectedTable.Medications:
                    AppendDataToFile(model as MedicationModel, new MedicationClassMap());
                    break;
                case SelectedTable.Warehouses:
                    AppendDataToFile(model as WarehouseModel, new WarehouseClassMap());
                    break;

                case SelectedTable.Manufacturers:
                    AppendDataToFile(model as ManufacturerModel, new ManufacturerClassMap());
                    break;

                case SelectedTable.Sales:
                    AppendDataToFile(model as SaleModel, new SaleClassMap());
                    break;

                case SelectedTable.Purchases:
                    AppendDataToFile(model as PurchaseModel, new PurchaseClassMap());
                    break;
            }
        }

        private void WriteDataToFile<T, TMap>(List<T> dataList, TMap classMap) where TMap : ClassMap<T>
        {

            using (StreamWriter writer = new StreamWriter(_path, false))
            {
                using (CsvWriter csvWriter = new CsvWriter(writer, CultureInfo.InvariantCulture))
                {
                    var config = new CsvConfiguration(CultureInfo.InvariantCulture) { HasHeaderRecord = false, Delimiter = ";" };
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
                        var config = new CsvConfiguration(CultureInfo.InvariantCulture) { HasHeaderRecord = false, Delimiter = ";" };
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

        private void AppendDataToFile<T>(T model, ClassMap<T> classMap)
        {
            using (StreamWriter writer = new StreamWriter(_path, true))
            {
                using (CsvWriter csvWriter = new CsvWriter(writer, CultureInfo.InvariantCulture))
                {
                    var config = new CsvConfiguration(CultureInfo.InvariantCulture) { HasHeaderRecord = false, Delimiter = ";" };
                    csvWriter.Context.RegisterClassMap(classMap);
                    csvWriter.WriteRecord(model);
                }
            }
        }
    }
}