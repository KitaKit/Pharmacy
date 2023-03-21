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
using CsvHelper;
using CsvHelper.Configuration;

//Здесь будет описана логика работы с CSV файлом

//Logika práce so súborom CSV bude popísaná tu

namespace Pharmacy
{
    public class FilesIOLogic
    {
        private FilesConnectionService _fileConnection = new FilesConnectionService();
        private string _path;
        public string Path { get { return _path; } }
        private string _name;
        public string Name { get { return _name; } }
        public FilesIOLogic()
        {
            _path = _fileConnection.FilePath;
            _name = System.IO.Path.GetFileNameWithoutExtension(_path);
        }

        public void ReadData(DataLists dataLists)
        { 
            switch (_name)
            {
                case "Medications":
                    MedicationClassMap medicationMap = new MedicationClassMap();
                    ReadData(dataLists.MedicationsData, medicationMap);
                    break;

                case "Warehouses":
                    WarehouseClassMap warehouseMap = new WarehouseClassMap();
                    ReadData(dataLists.WarehousesData, warehouseMap);
                    break;

                case "Manufacturers":
                    ManufacturerClassMap manufacturerMap = new ManufacturerClassMap();
                    ReadData(dataLists.ManufacturersData, manufacturerMap);
                    break;

                case "Sales":
                    SaleClassMap saleMap = new SaleClassMap();
                    ReadData(dataLists.SalesData, saleMap);
                    break;

                case "Purchases":
                    PurchaseClassMap purchaseMap = new PurchaseClassMap();
                    ReadData(dataLists.PurchasesData, purchaseMap);
                    break;

                default:
                    MessageBox.Show("Wrong name of file!", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                    break;
            }
        }
        private void ReadData<T, TMap>(List<T> dataList, TMap classMap) where TMap : ClassMap<T>
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