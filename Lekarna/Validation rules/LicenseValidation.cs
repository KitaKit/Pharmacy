using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Pharmacy
{
    public class LicenseValidation : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value.ToString().Length > 10 || value.ToString().Length < 10 || value == null || string.IsNullOrEmpty(value.ToString()))
                return new ValidationResult(false, "");
            return ValidationResult.ValidResult;
        }
    }
}
