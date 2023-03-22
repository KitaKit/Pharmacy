using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

//Здесь объектно описана таблица Medications из БД

// Zde je objektivně popsána tabulka Medications z databáze

namespace Pharmacy
{
    public class MedicationModel
    {
        private int _id;
        private string _title;
        private int _count;
        private bool _availability;
        private string _description;
        private bool _prescription;
        private DateTime _expirationDate;
        private decimal _price;

        public MedicationModel() { }

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
        public bool Availability { get { return _availability; } set { _availability = value; } }
        public bool Prescription { get { return _prescription; } set { _prescription = value; } }
        public string Title 
        {
            get { return _title; }
            set
            {
                if (_title != value && !(string.IsNullOrEmpty(value)))
                    _title = value;
            }
        }
        public int Count 
        {
            get { return _count; }
            set
            {
                if (_count != value)
                    _count = value;
            }
        }
        public string Description 
        {
            get { return _description; }
            set
            { 
                if (_description != value && !(string.IsNullOrEmpty(value)))
                    _description = value; 
            }
        }
        public DateTime ExpirationDate 
        {
            get { return _expirationDate; }
            set
            {
                if (_expirationDate != value && value != null)
                    _expirationDate = value; 
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

        public MedicationModel(int id, string title, bool availability, int count, string description, bool prescription, DateTime expirationDate,decimal price)
        {
            _id = id;
            Title = title;
            _availability = availability;
            Count = count;
            Description = description;
            Price = price;
            ExpirationDate = expirationDate;
            _prescription = prescription;
        }
    }
    public class MedicationClassMap : ClassMap<MedicationModel>
    {
        public MedicationClassMap() 
        {
            Map(x => x.Id).Name("Id");
            Map(x => x.Title).Name("Title");
            Map(x => x.Availability).Name("Availability");
            Map(x => x.Count).Name("Count");
            Map(x => x.Prescription).Name("Prescription");
            Map(x => x.ExpirationDate).Name("ExpirationDate");
            Map(x => x.Price).Name("Price");
            Map(x => x.Description).Name("Description");
        }
    }
}
