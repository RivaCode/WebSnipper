using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Windows.Controls;
using ValidationResult = System.Windows.Controls.ValidationResult;
using Validator = System.Web.WebPages.Validator;

namespace WebSnipper.UI.Presentation.Views
{
    public class UrlValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            return new ValidationResult(true, null);
            if (value == null) return new ValidationResult(true, null);

            if (!string.IsNullOrWhiteSpace(value as string))
            {
                var result = Validator.Url("Not a valid URL").Validate(new ValidationContext(value));
                const string httpPrefix = "http://";

                string uri = (string)value;
                //if (!uri.StartsWith(httpPrefix))
                //{
                //    uri = $"{httpPrefix}{uri}";
                //}
                //if(Uri.IsWellFormedUriString(uri, UriKind.RelativeOrAbsolute))
                    if (Uri.TryCreate(uri, UriKind.RelativeOrAbsolute, out Uri parsedUri))
                    //    && Uri.UriSchemeHttp == parsedUri.Scheme)
                {
                    return new ValidationResult(true, null);
                }
                return new ValidationResult(false, "Not a valid URL");
            }
            return new ValidationResult(false, "URL field is required");
        }
    }
}