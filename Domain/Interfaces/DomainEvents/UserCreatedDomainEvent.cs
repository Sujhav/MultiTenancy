using Domain.Interfaces.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.DomainEvents
{
    public sealed record UserCreatedDomainEvent(Guid UserId) : IDomainEvents
    {
    }
}
