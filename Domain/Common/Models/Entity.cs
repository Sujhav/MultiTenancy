﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Common.Models
{
    public abstract class Entity<TId> : IEquatable<Entity<TId>> where TId : notnull
    {
        public TId Id { get; protected set; }

        public override bool Equals(object? obj)
        {
            return obj is Entity<TId> entity && Id.Equals(entity.Id);
        }

        public bool Equals(Entity<TId>? other)
        {
            return Equals((object?)other);
        }

        public static bool operator ==(Entity<TId> Left, Entity<TId> Right)
        {
            return Equals(Left, Right);
        }

        public static bool operator !=(Entity<TId> Left, Entity<TId> Right)
        {
            return !Equals(Left, Right);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

#pragma warning disable CS8618
        protected Entity()
        {

        }
#pragma warning restore CS8618
    }
}
