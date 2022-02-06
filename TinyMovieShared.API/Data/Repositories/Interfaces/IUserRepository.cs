using TinyMovieShared.API.Models.Entities;

namespace TinyMovieShared.API.Data.Repositories.Interfaces
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<User> GetUserByUsername(string username);
    }
}
