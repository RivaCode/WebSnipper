using System;

namespace Domain.Models
{
    public abstract class ValueObject<T> : IEquatable<T>
        where T : ValueObject<T>
    {
        #region IEquatable<ValueObject<T>> members

        public bool Equals(T other) => EqualsCore(other);

        #endregion

        public override bool Equals(object obj)
        {
            var valueObject = obj as T;
            return !ReferenceEquals(valueObject, null) && EqualsCore(valueObject);
        }

        public override int GetHashCode() => GetHashCodeCore();

        protected abstract bool EqualsCore(T other);
        protected abstract int GetHashCodeCore();

        #region Operators

        public static bool operator ==(ValueObject<T> left, ValueObject<T> right)
        {
            if (ReferenceEquals(left, null) && ReferenceEquals(right, null))
            {
                return true;
            }

            if (ReferenceEquals(left, null) || ReferenceEquals(right, null))
            {
                return false;
            }

            return left.Equals(right);
        }

        public static bool operator !=(ValueObject<T> left, ValueObject<T> right) => !(left == right);

        #endregion
    }
}