using System;

namespace WebSnipper.UI.Domain
{
    public class RefreshRate : ValueObject<RefreshRate>
    {
        public TimeSpan Refresh { get; }

        public RefreshRate(TimeSpan refresh) => Refresh = refresh;

        protected override bool EqualsCore(RefreshRate other) => Refresh == other.Refresh;

        protected override int GetHashCodeCore() => Refresh.GetHashCode();
    }
}