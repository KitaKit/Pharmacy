using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Models
{
    public class CatigoriesModel
    {
        private readonly int _id;
        private string _name;
        
        public int Id { get { return _id; } }
        public string Name 
        { 
            get { return _name; } 
            set 
            {   if (!(string.IsNullOrEmpty(value)) && _name != value)
                    _name = value;
            }
        }
    }
}
