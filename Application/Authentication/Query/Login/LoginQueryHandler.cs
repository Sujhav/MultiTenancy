using Application.Common.Interfaces.Authentication;
using Application.Common.Interfaces.Persistance;
using Domain.Users.ValuesObjects;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Authentication.Query.Login
{
    public class LoginQueryHandler : IRequestHandler<LoginQuery, AuthenticationResult>
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        public LoginQueryHandler(IUserRepository userRepository, IPasswordHasher passwordHasher, IJwtTokenGenerator jwtTokenGenerator)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        public async Task<AuthenticationResult> Handle(LoginQuery request, CancellationToken cancellationToken)
        {
            var Emails = Email.GetEmail(request.Email);
            var userdata = await _userRepository.GetUsersByEmail(Emails);
            if (userdata is null)
            {
                throw new Exception("Invalid Credentials");
            }

            var verifyPassword = _passwordHasher.VerifyPassword(request.Password, userdata.Passsword);
            if (!verifyPassword)
            {
                throw new Exception("Invalid Credentials");
            }
            var token = _jwtTokenGenerator.GenerateToken(userdata);

            return new AuthenticationResult(userdata, token);
        }
    }
}
