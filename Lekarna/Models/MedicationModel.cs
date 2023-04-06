using CsvHelper.Configuration;
using System;

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
        private string _warehouse;
        private string _medicationForm;
        private string _category;
        private string _manufacturer;

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
        public string Warehouse { get { return _warehouse; } set { _warehouse = value; } }
        public string Form { get { return _medicationForm; } set { _medicationForm = value; } }
        public string Category { get { return _category; } set { _category = value; } }
        public string Manufacturer { get { return _manufacturer; } set { _manufacturer = value; } }

        public MedicationModel(int id, string title, bool availability, int count, string description, bool prescription, DateTime expirationDate, decimal price,  string warehouse, string medicationForm, string manufacturer, string category) : this(title,availability,count,description,prescription,expirationDate,price, warehouse, medicationForm, manufacturer, category)
        {
            _id = id;
        }
        public MedicationModel(string title, bool availability, int count, string description, bool prescription, DateTime expirationDate, decimal price, string warehouse, string medicationForm, string manufacturer, string category)
        {
            Title = title;
            _availability = availability;
            Count = count;
            Description = description;
            Price = price;
            ExpirationDate = expirationDate;
            _prescription = prescription;
            _warehouse = warehouse;
            _manufacturer = manufacturer;
            _category = category;
            _medicationForm = medicationForm;
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
            Map(x => x.Category).Name("Category");
            Map(x => x.Form).Name("Form");
            Map(x => x.Warehouse).Name("Warehouse");
            Map(x => x.Manufacturer).Name("Manufacturer");
        }
    }
}
