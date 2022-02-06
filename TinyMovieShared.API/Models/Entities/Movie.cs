using TinyMovieShared.API.Exceptions;
using TinyMovieShared.API.Models.Validators;

namespace TinyMovieShared.API.Models.Entities
{
    public class Movie : BaseEntity
    {
        public string Name { get; private set; }
        public string Director { get; private set; }
        public float? Stars { get; private set; }
        public long TotalVotes { get; private set; }
        public string Genre { get; private set; }
        public DateTime CreatedAt { get; }
        public DateTime UpdatedAt { get; private set; }

        public Movie(string name, string director, string genre)
        {
            Id = Guid.NewGuid();
            Name = name;
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
            Director = director;
            Genre = genre;
            TotalVotes = 0;
            _errors = new List<string>();
            Validate();
        }

        public void ChangeName(string name)
        {
            Name = name;
            Validate();
            UpdatedAt = DateTime.UtcNow;
        }

        public void ChangeStars(float stars)
        {
            Stars = stars;
            Validate();
            UpdatedAt = DateTime.UtcNow;
        }

        public void ChangeDirector(string director)
        {
            Director = director;
            Validate();
            UpdatedAt = DateTime.UtcNow;
        }

        public void ChangeGenre(string genre)
        {
            Genre = genre;
            Validate();
            UpdatedAt = DateTime.UtcNow;
        }

        public void ChangeTotalVotes(long totalVotes)
        {
            TotalVotes = totalVotes;
            Validate();
            UpdatedAt = DateTime.UtcNow;
        }
        protected override void Validate()
        {
            var validator = new MovieValidator();
            var validation = validator.Validate(this);

            if (!validation.IsValid)
            {
                foreach (var error in validation.Errors)
                {
                    _errors.Add(error.ErrorMessage);
                }

                throw new DomainException("Invalid Field(s)", _errors);
            }
        }
    }

    public enum Genre
    {
        action,
        fiction,
        drama,
        comedy,
        horror,
        fanstasy,
        romance,
        mystery
    }
}
