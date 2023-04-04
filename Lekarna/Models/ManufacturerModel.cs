using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

//Здесь объектно описана таблица Manufacturers из БД

// Zde je objektivně popsána tabulka Manufacturers z databáze

namespace Pharmacy
{
    public class ManufacturerModel
    {
        private const int MAX_LICENSE_LENGTH = 10;

        private int _id;
        private string _name;
        private string _country;
        private string _license;
        private DateTime _lastPurchaseDate;
        private string _providers;
        private string _medications;

        public ManufacturerModel() { }

        public int Id
        {
            get { return _id; }
            set
            {
                if (_id != 0)
                    return;
                _id = value;
            }
        }
        public string Name 
        {
            get { return _name; } 
            set
            {
                if (!(string.IsNullOrEmpty(value)) && _name != value)
                    _name = value;
            }
        }
        public string Country 
        { 
            get { return _country; }
            set
            {
                if (_country != value && !(string.IsNullOrEmpty(value)))
                    _country = value;
            }
        }
        public string License 
        {
            get { return _license; }
            set
            {
                if (_license != value && !(string.IsNullOrEmpty(value)) && value.Length <= 10)
                    _license = value;
            }
        }
        public DateTime LastPurchaseDate { get { return _lastPurchaseDate; } set { _lastPurchaseDate = value; } }
        public string Providers { get { return _providers; } set { _providers = value; } }
        public string Medications { get { return _medications; } set { _medications = value; } }

        public ManufacturerModel(string name, string country, string license, DateTime lastPurchaseDate, string providers, string medications)
        {
            Name = name;
            Country = country;
            License = license;
            LastPurchaseDate = lastPurchaseDate;
            Providers = providers;
            Medications = medications;
        }
        public ManufacturerModel(int id, string name, string country, string license, DateTime lastPurchaseDate, string providers, string medications):this(name,country,license, lastPurchaseDate, providers, medications)
        {
            _id = id;
        }
    }
    public class ManufacturerClassMap : ClassMap<ManufacturerModel>
    {
        public ManufacturerClassMap()
        {
            Map(x => x.Id).Name("Id");
            Map(x => x.Name).Name("Name");
            Map(x => x.Country).Name("Country");
            Map(x => x.License).Name("License");
            Map(x => x.LastPurchaseDate).Name("Last purchase date");
            Map(x => x.Providers).Name("Providers");
            Map(x => x.Medications).Name("Medications");
        }
    }
}
