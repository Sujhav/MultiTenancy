﻿using Domain.Interfaces.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DomainEvents
{
    public sealed record UserCreatedDomainEvents(Guid UserId):IdomainEvents
    {
    }
}
