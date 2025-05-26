﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.Authentication
{
    public interface IPasswordHasher
    {
        string HashPassoword(string Password);
        bool VerifyPassword(string Password, string PasswordHash);
    }
}
