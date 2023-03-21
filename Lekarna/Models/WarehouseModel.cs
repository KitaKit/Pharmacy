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
        private readonly int _id;
        private string _name;

        public int Id => _id;
        public string Name 
        {
            get { return _name; }
            set 
            {
                if (_name != value && !(string.IsNullOrEmpty(value)))
                    _name = value;
            }
        }

        public WarehouseModel(int id, string name)
        {
            _id = id;
            Name = name;
        }
    }
    public class WarehouseClassMap : ClassMap<WarehouseModel>
    {
        public WarehouseClassMap()
        {
            Map(x => x.Id).Name("Id");
            Map(x => x.Name).Name("Name");
        }
    }
}
