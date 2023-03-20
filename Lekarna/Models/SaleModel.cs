using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Здесь объектно описана таблица Sales из БД

// Zde je objektivně popsána tabulka Sales z databáze

namespace Pharmacy
{
    public class SaleModel
    {
        private readonly int _id;
        private decimal _price;
        private DateTime _date;

        public int Id { get { return _id; } }
        public decimal Price 
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

        public SaleModel(int id, decimal price, DateTime date) 
        {
            _id = id;
            Price = price;
            Date = date;
        }
    }
}
