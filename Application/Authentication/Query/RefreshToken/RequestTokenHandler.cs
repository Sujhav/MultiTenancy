using Application.Authentication.Query.Login;
using Infrastructure.Persistance.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Authentication.Query.RefreshToken
{
    public class RequestTokenHandler : IRequestHandler<RefreshTokenQuery, LoginResponse>
    {
        private readonly ILoginWithRefreshTokenRepository _repository;
        public RequestTokenHandler(ILoginWithRefreshTokenRepository loginWithRefresh)
        {
            _repository = loginWithRefresh;
        }
        public async Task<LoginResponse> Handle(RefreshTokenQuery request, CancellationToken cancellationToken)
        {

            var data = await _repository.handle(request);
            return new LoginResponse(data.AccessToken, data.RefreshToken);
        }

    }
}
