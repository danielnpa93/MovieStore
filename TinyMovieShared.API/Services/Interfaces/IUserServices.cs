using TinyMovieShared.API.Models.Dtos;
using TinyMovieShared.API.Result;

namespace TinyMovieShared.API.Services.Interfaces
{
    public interface IUserServices
    {
        public Task<ResultEnvelope> CreateUser(CreateUserDTO user);

        public Task<ResultEnvelope> ActiveDesactiveUser(string username, string status);

        public Task<ResultEnvelope> GetAll(int? limit, int? offset);
    }
}
