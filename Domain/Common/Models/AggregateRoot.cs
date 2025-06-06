﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Common.Models
{
    public abstract class AggregateRoot<TId, TIdType> : Entity<TId> where TId : AggregateRootId<TIdType>
    {
        public new AggregateRootId<TIdType> Id { get; protected set; }
        protected AggregateRoot(TId id)
        {
            Id = id;
        }
#pragma warning disable 
        protected AggregateRoot() { }
#pragma warning restore
    }
}
