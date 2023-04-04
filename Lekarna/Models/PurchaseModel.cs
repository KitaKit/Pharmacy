using CsvHelper.Configuration;
using System;

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
        private string _medication;
        private string _manufacturer;
        private string _provider;
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
        public string Medication { get { return _medication; } set { _medication = value; } }
        public string Manufacturer { get { return _manufacturer; } set { _manufacturer = value; } }
        public string Provider { get { return _provider; } set { _provider = value; } }
        public PurchaseModel(DateTime deliveryDate, int count, decimal cost, string medication, string manufacturer, string provider)
        {
            DeliveryDate = deliveryDate;
            Count = count;
            Cost = cost;
            Medication = medication;
            Manufacturer = manufacturer;
            Provider = provider;
        }
        public PurchaseModel(int id, DateTime deliveryDate, int count, decimal cost, string medication, string manufacturer, string provider) :this(deliveryDate, count, cost, medication, manufacturer, provider)
        {
            _id = id;
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
            Map(x => x.Medication).Name("Medications");
            Map(x => x.Provider).Name("Provider");
            Map(x => x.Manufacturer).Name("Manufacturer");
        }
    }
}
