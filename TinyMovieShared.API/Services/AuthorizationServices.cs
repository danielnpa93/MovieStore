using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TinyMovieShared.API.Config;
using TinyMovieShared.API.Data.Repositories.Interfaces;
using TinyMovieShared.API.Models.Dtos;
using TinyMovieShared.API.Result;
using TinyMovieShared.API.Services.Interfaces;

namespace TinyMovieShared.API.Services
{
    public class AuthorizationServices : IAuthorizationServices
    {
        private readonly ISettings _settings;
        private readonly IUserRepository _userRepository;

        public AuthorizationServices(ISettings settings, IUserRepository userRepository)
        {
            _settings = settings;
            _userRepository = userRepository;
        }

        public async Task<ResultEnvelope> HandleAuthorization(UserLoginDTO userLogin)
        {
            var user = await _userRepository.GetUserByUsername(userLogin.Username);
            if (user == null || !BCrypt.Net.BCrypt.Verify(userLogin.Password, user.Password))
            {
                return ResultEnvelope.Failure("Invalid username or password");

            }

            if(user.Status == Models.Entities.Status.inactive)
            {
                return ResultEnvelope.Failure("Inactive user");
            }

            var token = GenerateToken(user.Username, user.Role.ToString());

            return ResultEnvelope.Success(new {token = token });
        }

        private string GenerateToken(string username, string role)
        {

            var tokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(_settings.TokenKey);

            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                    new Claim[]
                    {
                        new Claim (ClaimTypes.Name, username),
                        new Claim(ClaimTypes.Role, role)
                    }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };


            var token = tokenHandler.CreateToken(tokenDescription);

            return tokenHandler.WriteToken(token);
        }
    }
}
