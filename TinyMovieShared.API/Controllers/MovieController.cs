using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TinyMovieShared.API.Models.Dtos;
using TinyMovieShared.API.Services.Interfaces;

namespace TinyMovieShared.API.Controllers
{
    [ApiController]
    [Route("/api/v1/[controller]")]
    public class MovieController : ApiBaseController
    {
        private readonly IMovieServices _movieServices;

        public MovieController(IMovieServices movieServices)
        {
            _movieServices = movieServices;
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterMovieDTO movie)
        {
            var response = await _movieServices.Register(movie);
            return this.HandleResult(response);
        }

        [Authorize(Roles = "client,admin")]
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int limit, int offset, string orderBy, string order)
        {
            var response = await _movieServices.GetAll(limit, offset, orderBy, order);
            return this.HandleResult(response);
        }

        [Authorize(Roles = "client,admin")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var response = await _movieServices.GetById(id);
            return this.HandleResult(response);
        }

        [Authorize(Roles = "client")]
        [HttpPost]
        [Route("vote")]
        public async Task<IActionResult> Vote(int stars, string id)
        {
            var userName = ((ClaimsIdentity)HttpContext.User.Identity)?.FindFirst(ClaimTypes.Name)?.Value ?? string.Empty;
            var response = await _movieServices.Vote(stars, id, userName);
            return this.HandleResult(response);
        }

    }
}
