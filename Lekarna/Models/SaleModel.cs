using CsvHelper.Configuration;
using System;

namespace Pharmacy
{
    public class SaleModel
    {
        private int _id;
        private decimal _price;
        private DateTime _date;
        private string _medications;

        public SaleModel() { }

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
        public decimal Price
        {
            get { return _price; }
            set
            {
                if (_price != value)
                    _price = value;
            }
        }
        public DateTime Date
        {
            get { return _date; }
            set
            {
                if (_date != value && value != null)
                    _date = value.Date;
            }
        }
        public string Medications { get { return _medications; } set { _medications = value; } }

        public SaleModel(decimal price, DateTime date, string medications)
        {
            Price = price;
            Date = date;
            Medications = medications;
        }
        public SaleModel(int id, decimal price, DateTime date, string medications):this(price, date, medications)
        {
            Id = id;
        }
    }
    public class SaleClassMap : ClassMap<SaleModel>
    {
        public SaleClassMap()
        {
            Map(x => x.Id).Name("Id");
            Map(x => x.Price).Name("Price");
            Map(x => x.Date).Name("Date").TypeConverterOption.Format("dd.MM.yyyy");
            Map(x => x.Medications).Name("Medications");
        }
    }
}
