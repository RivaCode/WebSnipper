using System;

namespace Domain.Models
{
    public class Website : ValueObject<Website>
    {
        public UrlHolder UrlHolder { get; }
        public PageProperties Properties { get; }

        public Website(
            UrlHolder urlHolder,
            PageProperties properties)
        {
            UrlHolder = urlHolder;
            Properties = properties;
        }

        public Website ChangeScannedDate(DateTime newScannedDate)
        {
            if (DateTime.Parse(Properties.ScanDate) > newScannedDate)
            {
                throw new ArgumentException();
            }
            return new Website(
                UrlHolder,
                new PageProperties(Properties.Name, newScannedDate, Properties.Description));
        }

        protected override bool EqualsCore(Website other) => UrlHolder == other.UrlHolder;

        protected override int GetHashCodeCore() => UrlHolder.GetHashCode();
    }
}