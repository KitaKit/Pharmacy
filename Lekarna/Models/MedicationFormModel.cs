using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Models
{
    public class MedicationFormModel
    {
        private readonly int _id;
        private string _form;

        public int Id { get { return _id; } }
        public string Form 
        {
            get { return _form; }
            set
            {
                if (_form != value && !(string.IsNullOrEmpty(value)))
                    _form = value; 
            }
        }
    }
}
