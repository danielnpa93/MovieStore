using Microsoft.EntityFrameworkCore;
using TinyMovieShared.API.Data.Repositories.Interfaces;
using TinyMovieShared.API.Models.Entities;
using TinyMovieShared.API.Results;

namespace TinyMovieShared.API.Data.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T: BaseEntity
    {
        private protected readonly ApplicationDbContext _context;

        public BaseRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public virtual async Task<T> Create(T obj)
        {
            _context.Add(obj);
            await _context.SaveChangesAsync();

            return obj;
        }

        public virtual async Task<bool> Delete(Guid id)
        {
            var obj = await GetById(id);

            if (obj == null)
            {
                _context.Set<T>()
                    .Remove(obj);
                await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public virtual async Task<IEnumerable<T>> GetAll()
        {
            return await _context.Set<T>()
                 .AsNoTracking()
                 .ToListAsync();
        }

        public virtual async Task<T> GetById(Guid id)
        {

            return await _context.Set<T>()
                 .AsNoTracking()
                 .FirstOrDefaultAsync(obj => obj.Id == id);
        }

        public virtual async Task<T> Update(T obj)
        {
            _context.Update(obj);
            await _context.SaveChangesAsync();

            return obj;
        }

        public async Task<PaginatedList<T>> GetAllPaginated(int limit, int offset, string orderBy, string order)
        {
            var count = await _context.Set<T>().CountAsync();

            if (order == "asc")
            {
                var orderAscMovieList = await _context.Set<T>()
                 .OrderByDynamic(x => $"x.{orderBy}")
                 .Skip((offset - 1) * limit)
                 .Take(limit)
                 .ToListAsync();

                return new PaginatedList<T>(orderAscMovieList, count, offset, limit);
            }

            var orderDescMovieList = await _context.Set<T>()
                .OrderByDescendingDynamic(x => $"x.{orderBy}")
                .Skip((offset - 1) * limit)
                .Take(limit)
                .ToListAsync();

            return new PaginatedList<T>(orderDescMovieList, count, offset, limit);
        }

    }
}
