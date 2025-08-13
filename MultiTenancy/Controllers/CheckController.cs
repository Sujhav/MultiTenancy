using Infrastructure.Service.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MultiTenancy.Controllers
{
    [ApiController]
    [Route("/Check")]
    //[ApiExplorerSettings(IgnoreApi = true)]
    //[HasPermission(Permission.AccessMember)]

    public class CheckController : ControllerBase
    {
        private readonly IAuthorizationService _authorizationService;
        public CheckController(IAuthorizationService authorizationService)
        {
            _authorizationService = authorizationService;
        }

        [HttpGet("/valid")]
        [Authorize]
        public async Task<IActionResult> CheckValidaty()
        {
            return Ok("haha you are valied");
        }

        [HttpGet("/index")]
        //[Authorize(Roles = "Admin")]
        [Authorize(Policy = "RequireAdmin")]
        public async Task<IActionResult> Index()
        {
            //if (User.HasClaim(c => c.Type == ClaimTypes.Role && c.Value == "Admin"))
            //{
            //    return Ok("hi hr");
            //}
            //var checkit = await _authorizationService.AuthorizeAsync(User, "RequireAdmin");
            if (User.IsInRole("Admin"))
            {
                return Ok("hi Admin");
            }
            return Ok("helllo users");
        }

        [HasPermission(Permission.ReadMember)]
        [HttpGet("/GetAllUsers")]

        public async Task<IActionResult> GetAllUsers()
        {
            return Ok("hello");
        }
    }
}
