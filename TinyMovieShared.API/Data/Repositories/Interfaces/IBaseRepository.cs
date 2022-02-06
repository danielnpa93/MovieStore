using TinyMovieShared.API.Results;

namespace TinyMovieShared.API.Data.Repositories.Interfaces
{
    public interface IBaseRepository<T> where T : class
    {
        Task<T> Create(T obj);
        Task<T> Update(T obj);
        Task<bool> Delete(Guid id);
        Task<T> GetById(Guid id);
        Task<IEnumerable<T>> GetAll();
        Task<PaginatedList<T>> GetAllPaginated(int limit, int offset, string orderBy, string order);
    }
}
