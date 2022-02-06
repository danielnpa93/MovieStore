using Microsoft.EntityFrameworkCore;
using TinyMovieShared.API.Data.Repositories.Interfaces;
using TinyMovieShared.API.Models.Entities;

namespace TinyMovieShared.API.Data.Repositories
{
    public class UserMovieRepository : IUserMovieRepository
    {
        private readonly ApplicationDbContext _context;

        public UserMovieRepository(ApplicationDbContext context) 
        {
            _context = context;
        }

        public async Task<bool> UserAlreadyVoted(Guid userId, Guid movieId)
        {
           return await _context.UsersMovies
                .AsNoTracking()
                .AnyAsync(um => um.UserId == userId && um.MovieId == movieId);
        }

        public async Task RegisterVote(UserMovie userMovie)
        {
            _context.Add(userMovie);
            await _context.SaveChangesAsync();
        }
    }
}
