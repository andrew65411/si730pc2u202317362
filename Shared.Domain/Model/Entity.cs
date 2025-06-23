using System;

namespace Hialpesa.Shared.Domain.Model
{
    /// <summary>
    /// Representa una entidad con identidad única.
    /// </summary>
    public abstract class Entity<T>
    {
        public T Id { get; protected set; }

        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (!(obj is Entity<T> other)) return false;

            if (ReferenceEquals(this, other)) return true;

            if (GetType() != other.GetType()) return false;

            return Id.Equals(other.Id);
        }

        public static bool operator ==(Entity<T> left, Entity<T> right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Entity<T> left, Entity<T> right)
        {
            return !(left == right);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}