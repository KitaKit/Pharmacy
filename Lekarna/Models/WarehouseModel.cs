using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Здесь объектно описана таблица Warehouses из БД

// Zde je objektivně popsána tabulka Warehouses z databáze

namespace Pharmacy.Models
{
    public class WarehouseModel
    {
        private readonly int _id;
        private string _name;

        public string Name 
        {
            get { return _name; }
            set 
            {
                if (_name != value && !(string.IsNullOrEmpty(value)))
                    _name = value;
            }
        }
    }
}
