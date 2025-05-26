using Application.Common.Dtos;
using Application.Common.Interfaces.Authentication;
using Application.Common.Interfaces.Persistance;
using Domain.Users;
using Domain.Users.ValuesObjects;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Authentication.Command.Register
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, AuthenticationResult>
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        public RegisterCommandHandler(IUserRepository userRepository, IPasswordHasher passwordHasher, IJwtTokenGenerator jwtTokenGenerator)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _jwtTokenGenerator = jwtTokenGenerator;
        }
        public async Task<AuthenticationResult> Handle(RegisterCommand command, CancellationToken cancellationToken)
        {
            var Emailvalue = Email.GetEmail(command.Users.Email);
            var data = await _userRepository.GetUsersByEmail(Emailvalue);
            if (data is not null)
            {
                throw new Exception("duplicate Email");
            }
            var users = command.Users;
            var addrss = command.Users.Address;
            var HashedPassword = _passwordHasher.HashPassoword(users.Password);
            var userss = Users.CreateUsers(users.FirstName, users.LastName,
                Email.GetEmail(users.Email), HashedPassword,
                Address.GetAddress(addrss.District, addrss.City), PhoneNumber.GetPhoneNumber(users.PhoneNo)
                );

            await _userRepository.Add(userss);

            var token = _jwtTokenGenerator.GenerateToken(userss);

            return new AuthenticationResult(userss, token);
        }

    }
}
