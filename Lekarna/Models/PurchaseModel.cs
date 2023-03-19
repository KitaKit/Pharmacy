using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Здесь объектно описана таблица Purchases из БД

// Zde je objektivně popsána tabulka Purchases z databáze

namespace Pharmacy.Models
{
    public class PurchaseModel
    {
        private readonly int _id;
        private DateTime _deliveryDate;
        private int _count;
        private float _cost;

        public int Id { get { return _id; } }
        public DateTime DeliveryDate
        { 
            get { return _deliveryDate; }
            set 
            {
                if (_deliveryDate != value && value != null)
                    _deliveryDate = value;
            }
        }
        public int Count 
        {
            get { return _count; }
            set 
            {
                if (value != _count)
                    _count = value;
            }
        }
        public float Cost 
        {
            get { return _cost; }
            set
            {
                if (_cost != value)
                    _cost = value;
            }
        }
    }
}
