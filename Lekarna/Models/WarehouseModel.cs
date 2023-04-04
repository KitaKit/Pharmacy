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
        private string _medications;
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
        public string Medications { get { return _medications;} set { _medications = value; } }

        public WarehouseModel(string name, string medications)
        {
            Name = name;
            _medications = medications;
        }
        public WarehouseModel(int id, string name, string medications):this(name, medications)
        {
            _id = id;
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
