using Application.Authentication.Command.Register;
using Application.Authentication.Query.Login;
using Application.Common.Dtos;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MultiTenancy.Users;

namespace MultiTenancy.Controllers
{
    [ApiController]
    [Route("/api/")]
    [AllowAnonymous]
    public class AuthenticationController : ControllerBase
    {

        private readonly ISender _mediatr;
        public AuthenticationController(ISender mediatr)
        {
            _mediatr = mediatr;
        }

        [HttpPost("/register")]
        public async Task<IActionResult> Register(UsersDto users)
        {
            var command = new RegisterCommand(new UsersDto(users.FirstName, users.LastName, users.Email, users.Password, new AddressDto(users.Address.District, users.Address.City), users.PhoneNo));


            var result = await _mediatr.Send(command);
            return Ok(result);
        }

        [HttpPost("/Login")]
        public async Task<IActionResult> Login(string Email, string Password)
        {
            var command = new LoginQuery(Email, Password);

            var result = await _mediatr.Send(command);
            return Ok(result);
        }

    }
}
