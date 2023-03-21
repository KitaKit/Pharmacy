﻿using CsvHelper.Configuration;
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

        private readonly int _id;
        private string _name;
        private string _country;
        private string _license;

        public int Id => _id;
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

        public ManufacturerModel(int id, string name, string country, string license)
        {
            _id = id;
            Name = name;
            Country = country;
            License = license;
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
        }
    }
}
