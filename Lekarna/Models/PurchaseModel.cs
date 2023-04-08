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
        private decimal _cost;
        private string _medication;
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
        public decimal Cost 
        {
            get { return _cost; }
            set
            {
                if (_cost != value)
                    _cost = value;
            }
        }
        public string Medications { get { return _medication; } set { _medication = value; } }
        public string Provider { get { return _provider; } set { _provider = value; } }
        public PurchaseModel(DateTime deliveryDate, decimal cost, string provider, string medication)
        {
            DeliveryDate = deliveryDate;
            Cost = cost;
            Medications = medication;
            Provider = provider;
        }
        public PurchaseModel(int id, DateTime deliveryDate, decimal cost, string provider, string medication) : this(deliveryDate, cost, provider, medication)
        {
            Id = id;
        }
    }
    public class PurchaseClassMap : ClassMap<PurchaseModel>
    {
        public PurchaseClassMap()
        {
            Map(x => x.Id).Name("Id");
            Map(x => x.DeliveryDate).Name("DeliveryDate");
            Map(x => x.Cost).Name("Cost");
            Map(x => x.Medications).Name("Medications");
            Map(x => x.Provider).Name("Provider");
        }
    }
}
