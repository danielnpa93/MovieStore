using Microsoft.AspNetCore.Mvc;
using TinyMovieShared.API.Models.Dtos;
using TinyMovieShared.API.Services.Interfaces;

namespace TinyMovieShared.API.Controllers
{
    [ApiController]
    [Route("/api/v1/[controller]")]
    public class AuthController : ApiBaseController
    {
        private readonly IAuthorizationServices _authorizationServices;

        public AuthController(IAuthorizationServices authorizationServices)
        {
            _authorizationServices = authorizationServices;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDTO userLogin)
        {
            var response = await _authorizationServices.HandleAuthorization(userLogin);
            return this.HandleResult(response);
          
        }
    }
}
