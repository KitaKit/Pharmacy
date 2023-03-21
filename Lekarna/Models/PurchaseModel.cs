using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Здесь объектно описана таблица Purchases из БД

// Zde je objektivně popsána tabulka Purchases z databáze

namespace Pharmacy
{
    public class PurchaseModel
    {
        private readonly int _id;
        private DateTime _deliveryDate;
        private int _count;
        private decimal _cost;

        public int Id => _id;
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
        public decimal Cost 
        {
            get { return _cost; }
            set
            {
                if (_cost != value)
                    _cost = value;
            }
        }

        public PurchaseModel(int id, DateTime deliveryDate, int count, decimal cost)
        {
            _id = id;
            DeliveryDate = deliveryDate;
            Count = count;
            Cost = cost;
        }
    }
    public class PurchaseClassMap : ClassMap<PurchaseModel>
    {
        public PurchaseClassMap()
        {
            Map(x => x.Id).Name("Id");
            Map(x => x.DeliveryDate).Name("DeliveryDate");
            Map(x => x.Count).Name("Count");
            Map(x => x.Cost).Name("Cost");
        }
    }
}
