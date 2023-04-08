using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Pharmacy
{
    public class CountryValidation : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            var regex = new Regex(@"^[^\d]+$");

            if (value == null || string.IsNullOrEmpty(value.ToString()) || !regex.IsMatch(value.ToString()))
            {
                return new ValidationResult(false, "");
            }

            return ValidationResult.ValidResult;
        }
    }
}
