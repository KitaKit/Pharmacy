using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Здесь объектно описана таблица Providers из БД

// Zde je objektivně popsána tabulka Providers z databáze

namespace Pharmacy
{
    public class ProviderModel
    {
        private readonly int _id;
        private string _name;

        public int Id { get { return _id; } }
        public string Name
        {
            get { return _name;}
            set
            {
                if (!(string.IsNullOrEmpty(value)) && value != _name)
                    _name = value;
            }
        }

        public ProviderModel(int id, string name)
        {
            _id = id;
            Name = name;
        }
    }
}
