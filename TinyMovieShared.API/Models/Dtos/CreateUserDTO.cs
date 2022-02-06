using System.ComponentModel.DataAnnotations;

namespace TinyMovieShared.API.Models.Dtos
{
    public class CreateUserDTO
    { 

        [MinLength(3, ErrorMessage = "Username should have min of 3 characteres")]
        public string Username { get; set; }

        [MinLength(6, ErrorMessage = "Password should have min of 6 characteres")]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "Not Matches")]
        public string ConfirmPassword { get; set; }
        public string? Role { get; set; }
    }
}
