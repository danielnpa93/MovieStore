using TinyMovieShared.API.Models.Entities;

namespace TinyMovieShared.API.Data.Repositories.Interfaces
{
    public interface IUserMovieRepository
    {
        Task<bool> UserAlreadyVoted(Guid userId, Guid movieId);

        Task RegisterVote(UserMovie userMovie);
    }
}
