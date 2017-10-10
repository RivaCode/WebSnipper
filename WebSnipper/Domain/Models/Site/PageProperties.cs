using System;
using Domain.Util;
using Optional;

namespace Domain.Models
{
    public class PageProperties : ValueObject<PageProperties>
    {
        public string Name { get; }
        private DateTime ScannedAt { get; }
        public Option<string> Description { get; }
        public string ScanDate => ScannedAt.ToShortDateString();

        public PageProperties(
            string name,
            DateTime scannedAt,
            Option<string> description)
        {
            Name = name.ValidateArgument(nameof(name));
            ScannedAt = scannedAt;
            Description = description;
        }

        protected override bool EqualsCore(PageProperties other)
            => Name == other.Name &&
               Description == other.Description;

        protected override int GetHashCodeCore()
            => Name.GetHashCode() ^
               Description.GetHashCode();
    }
}
