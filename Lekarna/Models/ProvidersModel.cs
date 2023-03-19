using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Models
{
    public class ProvidersModel
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

    }
}
