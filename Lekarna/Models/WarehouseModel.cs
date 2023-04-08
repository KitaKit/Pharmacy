using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Здесь объектно описана таблица Warehouses из БД

// Zde je objektivně popsána tabulka Warehouses z databáze

namespace Pharmacy
{
    public class WarehouseModel
    {
        private int _id;
        private string _name;
        private string _medication;
        public WarehouseModel() { }
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
                if (_name != value && !(string.IsNullOrEmpty(value)))
                    _name = value;
            }
        }
        public string Medications { get { return _medication; } set { _medication = value; } }
        public WarehouseModel(string name)
        {
            Name = name;
        }
        public WarehouseModel(int id, string name):this(name)
        {
            Id = id;
        }
    }
    public class WarehouseClassMap : ClassMap<WarehouseModel>
    {
        public WarehouseClassMap()
        {
            Map(x => x.Id).Name("Id");
            Map(x => x.Name).Name("Name");
            Map(x => x.Medications).Name("Medications");
        }
    }
}
