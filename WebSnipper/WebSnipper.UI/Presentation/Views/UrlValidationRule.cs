using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Windows.Controls;
using ValidationResult = System.Windows.Controls.ValidationResult;

namespace WebSnipper.UI.Presentation.Views
{
    public class UrlValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            return new ValidationResult(true, null);}
    }
}