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
    public class FilesIOLogic
    {
        private FilesConnectionService _fileConnection;
        private string _path;
        public string Path { get { return _path; } }
        private string _name;
        public string Name { get { return _name; } }
        public FilesIOLogic(MenuItem item)
        {
            _fileConnection = new FilesConnectionService(item);
            _path = _fileConnection.FilePath;
            _name = System.IO.Path.GetFileNameWithoutExtension(_path);
        }

        public void ReadData(DataLists dataLists)
        { 
            switch (_name)
            {
                case "Medications":
                    MedicationClassMap medicationMap = new MedicationClassMap();
                    List<MedicationModel> medicationsData = dataLists.MedicationsData;
                    ReadData(ref medicationsData, medicationMap);
                    dataLists.MedicationsData = medicationsData;
                    break;

                case "Warehouses":
                    WarehouseClassMap warehouseMap = new WarehouseClassMap();
                    List<WarehouseModel> warehousesData = dataLists.WarehousesData;
                    ReadData(ref warehousesData, warehouseMap);
                    dataLists.WarehousesData = warehousesData;
                    break;

                case "Manufacturers":
                    ManufacturerClassMap manufacturerMap = new ManufacturerClassMap();
                    List<ManufacturerModel> manufacturersData = dataLists.ManufacturersData;
                    ReadData(ref manufacturersData, manufacturerMap);
                    dataLists.ManufacturersData = manufacturersData;
                    break;

                case "Sales":
                    SaleClassMap saleMap = new SaleClassMap();
                    List<SaleModel> salesData = dataLists.SalesData;
                    ReadData(ref salesData, saleMap);
                    dataLists.SalesData = salesData;
                    break;

                case "Purchases":
                    PurchaseClassMap purchaseMap = new PurchaseClassMap();
                    List<PurchaseModel> purchasesData = dataLists.PurchasesData;
                    ReadData(ref purchasesData, purchaseMap);
                    dataLists.PurchasesData = purchasesData;
                    break;

                default:
                    MessageBox.Show("Wrong name of file!", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                    break;
            }
        }
        public void WriteData(DataLists dataLists)
        {
            switch (_name)
            {
                case "Medications":
                    if (!dataLists.IsEmpty(dataLists.MedicationsData))
                    {
                        MedicationClassMap medicationMap = new MedicationClassMap();
                        WriteData(dataLists.MedicationsData, medicationMap);
                        break;
                    }
                    break;

                case "Warehouses":
                    if (!dataLists.IsEmpty(dataLists.WarehousesData))
                    {
                        WarehouseClassMap warehouseMap = new WarehouseClassMap();
                        WriteData(dataLists.WarehousesData, warehouseMap);
                        break;
                    }
                    break;

                case "Manufacturers":
                    if (!dataLists.IsEmpty(dataLists.ManufacturersData))
                    {
                        ManufacturerClassMap manufacturerMap = new ManufacturerClassMap();
                        WriteData(dataLists.ManufacturersData, manufacturerMap);
                        break;
                    }
                    break;

                case "Sales":
                    if (!dataLists.IsEmpty(dataLists.SalesData))
                    {
                        SaleClassMap saleMap = new SaleClassMap();
                        WriteData(dataLists.SalesData, saleMap);
                        break;
                    }
                    break;

                case "Purchases":
                    if (!dataLists.IsEmpty(dataLists.PurchasesData))
                    {
                        PurchaseClassMap purchaseMap = new PurchaseClassMap();
                        WriteData(dataLists.PurchasesData, purchaseMap);
                        break;
                    }
                    break;

                default:
                    MessageBox.Show("Wrong name of file!", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                    break;
            }
        }
        private void WriteData<T, TMap>(List<T> dataList, TMap classMap) where TMap : ClassMap<T>
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

        private void ReadData<T, TMap>(ref List<T> dataList, TMap classMap) where TMap : ClassMap<T>
        {
            using (StreamReader reader = new StreamReader(_path))
            {
                using (CsvReader csvReader = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    csvReader.Context.RegisterClassMap(classMap);
                    dataList = csvReader.GetRecords<T>().ToList();
                }
            }
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