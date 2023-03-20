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
        private readonly int _id;
        private string _title;
        private int _count;
        private bool _availability;
        private string _description;
        private bool _prescription;
        private DateTime _expirationDate;
        private decimal _price;

        public int Id { get { return _id; } }
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
}
