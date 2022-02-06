using TinyMovieShared.API.Models.Dtos;
using TinyMovieShared.API.Result;

namespace TinyMovieShared.API.Services.Interfaces
{
    public interface IAuthorizationServices
    {
        Task<ResultEnvelope> HandleAuthorization(UserLoginDTO userLogin);
    }
}
