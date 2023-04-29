using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Controls;

namespace Pharmacy
{
    public class NameValidation : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            var regex = new Regex("^[a-zA-Z0-9_ -]*$");

            if (value == null || string.IsNullOrEmpty(value.ToString()) || !regex.IsMatch(value.ToString()))
            {
                return new ValidationResult(false, "");
            }

            return ValidationResult.ValidResult;
        }
    }
}
