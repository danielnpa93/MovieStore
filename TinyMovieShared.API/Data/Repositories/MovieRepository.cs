using Microsoft.EntityFrameworkCore;
using TinyMovieShared.API.Data.Repositories.Interfaces;
using TinyMovieShared.API.Models.Entities;
using TinyMovieShared.API.Results;

namespace TinyMovieShared.API.Data.Repositories
{
    public class MovieRepository : BaseRepository<Movie>, IMovieRepository
    {
        public MovieRepository(ApplicationDbContext context) : base(context)
        {
        }

    }
}
