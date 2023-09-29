using System;
using System.Collections.Generic;
using Clinic.SharedKernel.Domain.Abstractions.Events;

namespace Clinic.SharedKernel.Domain.Abstractions.Model
{
    public abstract class Entity<TId> : IEquatable<Entity<TId>>
    {
        public TId Id { get; protected set; }
        private List<DomainEvent> _domainEvents;
        public List<DomainEvent> DomainEvents => _domainEvents;

        protected Entity()
        {
        }

        protected Entity(TId id)
        {
            if (Equals(id, default(TId)))
            {
                throw new ArgumentException("The ID cannot be the default value.", "id");
            }

            Id = id;
        }

        public void AddDomainEvent(DomainEvent eventItem)
        {
            _domainEvents = _domainEvents ?? new List<DomainEvent>();
            _domainEvents.Add(eventItem);
        }

        public void RemoveDomainEvent(DomainEvent eventItem)
        {
            _domainEvents?.Remove(eventItem);
        }

        public void ClearDomainEvents()
        {
            _domainEvents?.Clear();
        }

        public bool IsTransient()
        {
            return Equals(this.Id, default(TId));
        }

        public bool Equals(Entity<TId> other)
        {
            if (other == null) return false;
            return Id.Equals(other.Id);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((Entity<TId>)obj);
        }
        public static bool operator ==(Entity<TId> left, Entity<TId> right)
        {
            return left?.Equals(right) ?? object.Equals(right, null);
        }

        public static bool operator !=(Entity<TId> left, Entity<TId> right)
        {
            return !(left == right);
        }
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
