using FluentValidation;
using TinyMovieShared.API.Models.Entities;

namespace TinyMovieShared.API.Models.Validators
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(user => user)
              .NotEmpty()
              .WithMessage("Entity cannot be empty")
              .NotNull()
              .WithMessage("Entity cannot be null");

            RuleFor(user => user.Username)
                .NotEmpty()
                .WithMessage("Username cannot be empty")
                .NotNull()
                .WithMessage("Username cannot be null")
                .Length(3, 180)
                .WithMessage("Username Requires 3 - 180 characters");

            RuleFor(user => user.Password)
                .NotEmpty()
                .WithMessage("Password cannot be empty")
                .NotNull()
                .WithMessage("Password cannot be null");
        }
    }
}
