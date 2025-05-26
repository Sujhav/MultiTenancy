using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace Application.Authentication.Query.Login
{
    public class LoginQueryValidator : AbstractValidator<LoginQuery>
    {
        public LoginQueryValidator()
        {
            RuleFor(s => s.Email).NotEmpty()
                .Matches(@"[@]").WithMessage("not Valid");

        }
    }
}
