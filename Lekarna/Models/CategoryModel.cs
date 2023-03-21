using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Здесь объектно описана таблица Categories из БД

// Zde je objektivně popsána tabulka Categories z databáze

namespace Pharmacy
{
    public class CategoryModel
    {
        private readonly int _id;
        private string _name;
        
        public int Id => _id;
        public string Name 
        { 
            get { return _name; } 
            set 
            {   if (!(string.IsNullOrEmpty(value)) && _name != value)
                    _name = value; 
            }
        }

        public CategoryModel(int id, string name)
        {
            _id = id;
            Name = name;
        }
    }
}
