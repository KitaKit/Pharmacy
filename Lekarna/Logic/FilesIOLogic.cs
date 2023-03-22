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
                    dataLists.MedicationsData = GetDataFromFile(new List<MedicationModel>(), new MedicationClassMap());
                    break;

                case "Warehouses":
                    dataLists.WarehousesData = GetDataFromFile(new List<WarehouseModel>(), new WarehouseClassMap());
                    break;

                case "Manufacturers":
                    dataLists.ManufacturersData = GetDataFromFile(new List<ManufacturerModel>(), new ManufacturerClassMap());
                    break;

                case "Sales":
                    dataLists.SalesData = GetDataFromFile(new List<SaleModel>(), new SaleClassMap());
                    break;

                case "Purchases":
                    dataLists.PurchasesData = GetDataFromFile(new List<PurchaseModel>(), new PurchaseClassMap());
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
                        WriteDataToNewFile(dataLists.MedicationsData, new MedicationClassMap());
                        break;
                    }
                    break;

                case "Warehouses":
                    if (!dataLists.IsEmpty(dataLists.WarehousesData))
                    {
                        WriteDataToNewFile(dataLists.WarehousesData, new WarehouseClassMap());
                        break;
                    }
                    break;

                case "Manufacturers":
                    if (!dataLists.IsEmpty(dataLists.ManufacturersData))
                    {
                        WriteDataToNewFile(dataLists.ManufacturersData, new ManufacturerClassMap());
                        break;
                    }
                    break;

                case "Sales":
                    if (!dataLists.IsEmpty(dataLists.SalesData))
                    {
                        WriteDataToNewFile(dataLists.SalesData, new SaleClassMap());
                        break;
                    }
                    break;

                case "Purchases":
                    if (!dataLists.IsEmpty(dataLists.PurchasesData))
                    {
                        WriteDataToNewFile(dataLists.PurchasesData, new PurchaseClassMap());
                        break;
                    }
                    break;

                default:
                    MessageBox.Show("Wrong name of file!", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                    break;
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
                    csvReader.Context.RegisterClassMap(classMap);
                    return csvReader.GetRecords<T>().ToList();
                }
            }
        }

        private void WriteDataToSameFile()
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