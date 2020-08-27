using System.Globalization;
using System.Windows.Controls;

namespace NoteSystem.WpfApp.ValidationRules
{
    public class TextValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value is string @string)
            {
                if (@string.Length < 3 || @string.Length > 150)
                    return new ValidationResult(false, "Text must be between 3 and 150 characters");
            }
            else return new ValidationResult(false, "Text required field");

            return new ValidationResult(true, value);
        }
    }
}
