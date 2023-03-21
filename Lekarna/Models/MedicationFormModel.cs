using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Здесь объектно описана таблица Medication_forms из БД

// Zde je objektivně popsána tabulka Medication_forms z databáze

namespace Pharmacy
{
    public class MedicationFormModel
    {
        private readonly int _id;
        private string _form;

        public int Id => _id;
        public string Form 
        {
            get { return _form; }
            set
            {
                if (_form != value && !(string.IsNullOrEmpty(value)))
                    _form = value; 
            }
        }

        public MedicationFormModel(int id, string form)
        {
            _id = id;
            Form = form;
        }
    }
}
