using FluentValidation;
using TinyMovieShared.API.Models.Entities;

namespace TinyMovieShared.API.Models.Validators
{
    public class MovieValidator : AbstractValidator<Movie>
    {
        public MovieValidator()
        {
            RuleFor(movie => movie)
              .NotEmpty()
              .WithMessage("Entity cannot be empty")
              .NotNull()
              .WithMessage("Entity cannot be null");

            RuleFor(movie => movie.Name)
                .NotEmpty()
                .WithMessage("Name cannot be empty")
                .NotNull()
                .WithMessage("Name cannot be null")
                .Length(3, 180)
                .WithMessage("Name requires 3 - 180 characters");

            RuleFor(movie => movie.Director)
                .NotEmpty()
                .WithMessage("Director cannot be empty")
                .NotNull()
                .WithMessage("Director cannot be null")
                .Length(3, 180)
                .WithMessage("Director name requires 3 - 180 characters");

            RuleFor(movie => movie.Stars)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Stars cannobe be less than 0")
                .LessThanOrEqualTo(4)
                .WithMessage("Stars cannot be higher than 4");

            RuleFor(movie => movie.Genre)
                .Must(genre => Enum.IsDefined(typeof(Genre), genre));
          
        }
    }
}
