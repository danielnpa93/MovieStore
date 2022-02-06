using AutoMapper;
using TinyMovieShared.API.Data.Repositories.Interfaces;
using TinyMovieShared.API.Models.Dtos;
using TinyMovieShared.API.Models.Entities;
using TinyMovieShared.API.Result;
using TinyMovieShared.API.Services.Interfaces;

namespace TinyMovieShared.API.Services
{
    public class UserServices : IUserServices
    {
        private readonly IUserRepository _repository;
        private readonly IMapper _mapper;

        public UserServices(IUserRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ResultEnvelope> CreateUser(CreateUserDTO user)
        {
            var hasUserWithSameUsername = await _repository.GetUserByUsername(user.Username);

            if(hasUserWithSameUsername != null)
            {
                return ResultEnvelope.Failure("Username already exists");
            }

            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);

            var entityUser = _mapper.Map<User>(user);

            await _repository.Create(entityUser);

            var userViewModel = _mapper.Map<UserDTO>(entityUser);

            return ResultEnvelope.Success(userViewModel);

        }

        public async Task<ResultEnvelope> ActiveDesactiveUser(string username, string status)
        {

            var user = await _repository.GetUserByUsername(username);

            if (user == null)
            {
                return ResultEnvelope.Failure("User not exists", 404);
            }

            var parseSuccess = Enum.TryParse<Status>(status, out var newStatus);

            if (!parseSuccess && newStatus.Equals(Status.active))
            {
                return ResultEnvelope.Failure("Invalid Status");
            }

            user.ChangeStatus(newStatus);

            await _repository.Update(user);

            return ResultEnvelope.Success();

        }

        public async Task<ResultEnvelope> GetAll(int? limit, int? offset)
        {
            var validLimit = Math.Max(limit ?? 5, 1);
            var validOffset = Math.Max(offset ?? 1, 1);
         
            var users = await _repository.GetAllPaginated(validLimit, validOffset, "Name" , "asc");

            var usersViewModel = _mapper.Map<IEnumerable<UserDTO>>(users);

            return ResultEnvelope.Success(new { items = usersViewModel, totalPages = users.TotalPages, pageIndex = users.PageIndex });
        }
    }
}
