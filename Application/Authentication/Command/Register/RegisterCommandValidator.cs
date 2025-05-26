using FluentValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Authentication.Command.Register
{
    public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
    {
        public RegisterCommandValidator()
        {
            RuleFor(s => s.Users.FirstName)
                .NotEmpty().WithMessage("FirstName is Required")
                .Length(2, 50).WithMessage("First name must be between 2 and 50 characters.");

            RuleFor(s => s.Users.LastName)
                 .NotEmpty().WithMessage("LastName is Required")
                 .Length(2, 50).WithMessage("LastName  must be between 2 and 50 characters.");

            RuleFor(s => s.Users.Email)
                .NotEmpty().WithMessage("Email is Required")
            .EmailAddress().WithMessage("Invalid email format.");

            RuleFor(s => s.Users.Password)
                .NotEmpty().WithMessage("Password is Required")
                .Matches(@"^[a-zA-Z]").WithMessage("Password Must Start with with a letter")
                .Matches(@".*[A-Za-z0-9].*").WithMessage("Password must contain at least one alphanumeric character.")
             .Matches(@".*[!@#$%^&*(),.?\""\{\}|<>].*").WithMessage("Password must contain at least one special character.")
                .MinimumLength(8).WithMessage("Password must be at least 8 characters long.");

            RuleFor(s => s.Users.PhoneNo)
                 .NotEmpty().When(s => !string.IsNullOrEmpty(s.Users.Address.District))
                 .WithMessage("Phone number is required when address is Inserted");

        }
    }
}
