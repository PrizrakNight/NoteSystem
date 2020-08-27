using System.Globalization;
using System.Windows.Controls;

namespace NoteSystem.WpfApp.ValidationRules
{
    public class NameValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value is string @string)
            {
                if (@string.Length < 3 || @string.Length > 10)
                    return new ValidationResult(false, "The name must be at least 3 characters long and at most 10");
            }
            else return new ValidationResult(false, "");

            return new ValidationResult(true, value);
        }
    }
}
