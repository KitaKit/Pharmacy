using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Pharmacy
{
    public class BoolValidation : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if(value == null || value.ToString() != "False" || value.ToString() != "True")
                return new ValidationResult(false, "");
            return ValidationResult.ValidResult;
        }
    }
}
