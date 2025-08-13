using Application.AppUsers.Query;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace MultiTenancy.Controllers
{
    [ApiController]
    [Route("/api/AppUsers")]
    public class AppUsersController(IMediator mediatR) : ControllerBase
    {

        [HttpGet("GetUsers")]
        public async Task<IActionResult> GetAllUsers()
        {
            var command = new GetAllUsersQuey();
            var result = await mediatR.Send(command);

            if (result.IsFailure)
            {
                return BadRequest(result.Error!.message);
            }
            return Ok(result.value);

        }
    }
}
