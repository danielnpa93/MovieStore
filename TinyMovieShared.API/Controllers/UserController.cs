using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TinyMovieShared.API.Models.Dtos;
using TinyMovieShared.API.Services.Interfaces;

namespace TinyMovieShared.API.Controllers
{
    [ApiController]
    [Route("/api/v1/[controller]")]
    public class UserController : ApiBaseController
    {
        private readonly IUserServices _userServices;

        public UserController(IUserServices userServices)
        {
            _userServices = userServices;
        }

        [HttpPost]
        [Route("signup")]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserDTO user)
        {
            var response = await _userServices.CreateUser(user);
            return this.HandleResult(response);
        }

        [Authorize]
        [HttpPatch]
        [Route("status")]
        public async Task<IActionResult> ActiveDesactiveUser([FromBody] string status)
        {
            var userName = ((ClaimsIdentity)HttpContext.User.Identity)?.FindFirst(ClaimTypes.Name)?.Value ?? string.Empty;

            var response = await _userServices.ActiveDesactiveUser(userName,status);
            return this.HandleResult(response);

        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public async Task<IActionResult> GetUsers([FromQuery] int limit, int offset)
        {
            var response = await _userServices.GetAll(limit, offset);
            return this.HandleResult(response);
        }
        

    }
}
