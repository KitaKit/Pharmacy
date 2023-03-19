using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Models
{
    public class WarehousesModel
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
