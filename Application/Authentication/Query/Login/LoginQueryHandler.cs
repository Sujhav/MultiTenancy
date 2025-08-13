using Application.Common.Interfaces.Authentication;
using Application.Common.Interfaces.Persistance;
using Domain.Users.ValuesObjects;
using Infrastructure.Persistance.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Authentication.Query.Login
{
    public class LoginQueryHandler : IRequestHandler<LoginQuery, LoginResponse>
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        public LoginQueryHandler(IUserRepository userRepository, IPasswordHasher passwordHasher, IJwtTokenGenerator jwtTokenGenerator, IRefreshTokenRepository refreshTokenRepository)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _jwtTokenGenerator = jwtTokenGenerator;
            _refreshTokenRepository = refreshTokenRepository;
        }

        public async Task<LoginResponse> Handle(LoginQuery request, CancellationToken cancellationToken)
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

            var refreshToken = _jwtTokenGenerator.GenerateRefreshToken();

            await _refreshTokenRepository.Add(userdata, refreshToken);

            //return new AuthenticationResult(userdata, token);
            return new LoginResponse(token, refreshToken);
        }
    }
}
