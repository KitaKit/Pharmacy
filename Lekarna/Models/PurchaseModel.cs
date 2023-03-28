﻿using CsvHelper.Configuration;
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
        private int _id;
        private DateTime _deliveryDate;
        private int _count;
        private decimal _cost;
        public PurchaseModel() { }
        public int Id
        {
            get { return _id; }
            set
            {
                if (_id != 0)
                    return;
                _id = value;
            }
        }
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

        public PurchaseModel(DateTime deliveryDate, int count, decimal cost)
        {
            DeliveryDate = deliveryDate;
            Count = count;
            Cost = cost;
        }
        public PurchaseModel(int id, DateTime deliveryDate, int count, decimal cost) :this(deliveryDate, count, cost)
        {
            _id = id;
        }
    }
    public class PurchaseClassMap : ClassMap<PurchaseModel>
    {
        public PurchaseClassMap()
        {
            Map(x => x.DeliveryDate).Name("DeliveryDate");
            Map(x => x.Count).Name("Count");
            Map(x => x.Cost).Name("Cost");
        }
    }
}
