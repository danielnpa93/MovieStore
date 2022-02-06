using TinyMovieShared.API.Models.Dtos;
using TinyMovieShared.API.Result;

namespace TinyMovieShared.API.Services.Interfaces
{
    public interface IMovieServices
    {
        public Task<ResultEnvelope> Register(RegisterMovieDTO movie);

        public Task<ResultEnvelope> Unregister(string id);

        public Task<ResultEnvelope> Vote(int stars, string id, string username);

        public Task<ResultEnvelope> GetById(string id);

        public Task<ResultEnvelope> GetAll(int? limit, int? offset, string orderBy, string order);

    }
}
