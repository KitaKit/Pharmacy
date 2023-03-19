using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Models
{
    public class SalesModel
    {
        private readonly int _id;
        private float _price;
        private DateTime _date;

        public int Id { get { return _id; } }
        public float Price 
        {
            get { return _price; }
            set 
            {
                if( _price != value )
                    _price = value;
            }
        }
        public DateTime Date 
        { 
            get { return _date; }
            set 
            {
                if (_date != value && value != null)
                    _date = value;
            }
        }
    }
}
