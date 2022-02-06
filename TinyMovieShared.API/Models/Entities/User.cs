using TinyMovieShared.API.Exceptions;
using TinyMovieShared.API.Models.Validators;

namespace TinyMovieShared.API.Models.Entities
{
    public class User : BaseEntity
    {
        public string Username { get; private set; }
        public string Password { get; private set; }
        public Role Role { get; }
        public DateTime CreatedAt { get; }
        public DateTime UpdatedAt { get; private set; }
        public Status Status { get; private set; }

        public User(string username, string password, string? role)
        {
            Id = Guid.NewGuid();
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
            Username = username;
            Password = password;
            Role = String.IsNullOrEmpty(role) ? Role.client : (Role)(Enum.IsDefined(typeof(Role), role) ? Enum.Parse(typeof(Role), role) : Role.client);
            Status = Status.active;
            _errors = new List<string>();
            Validate();
        }

        public User() { }

        public void ChangePassword(string password)
        {
            Password = password;
            Validate();
            UpdatedAt = DateTime.UtcNow;
        }

        public void ChangeStatus(Status status)
        {
            Status = status;
            Validate();
            UpdatedAt= DateTime.UtcNow;
        }

        protected override void Validate()
        {
            var validator = new UserValidator();
            var validation = validator.Validate(this);

            if (!validation.IsValid)
            {
                foreach(var error in validation.Errors)
                {
                    _errors.Add(error.ErrorMessage);
                }

                throw new DomainException("Invalid Field(s)",_errors);
            }
        }
    }

    public enum Role
    {
        admin,
        client
    }

    public enum Status
    {
        active,
        inactive
    }
}
