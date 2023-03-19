using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;
//Здесь будет описана объектно таблица Medications из БД
namespace Pharmacy.Models
{
    public class MedicationsModel
    {
        private readonly int _id;
        private string _title;
        private int _count;
        private bool _availability;
        private string _description;
        private bool _prescription;
        private DateTime _expirationDate;
        private float _price;

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
        public float Price 
        { 
            get { return _price; }
            set
            { 
                if (_price != value)
                    _price = value;
            }
        }
    }
}
