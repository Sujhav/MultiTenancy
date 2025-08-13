using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Interfaces.Common;
using Domain.Interfaces.DomainEvents;

namespace Domain.Common.Models
{
    public abstract class AggregateRoot<TId, TIdType> : Entity<TId> where TId : AggregateRootId<TIdType>
    {
        public new AggregateRootId<TIdType> Id { get; protected set; }
        private readonly List<IDomainEvents> _domainEvents = new();
        protected AggregateRoot(TId id)
        {
            Id = id;
        }

        protected void RaiseDomainEvent(IDomainEvents domainEvents)
        {
            _domainEvents.Add(domainEvents);
        }
#pragma warning disable 
        protected AggregateRoot() { }
#pragma warning restore
    }
}
