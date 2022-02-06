using AutoMapper;
using TinyMovieShared.API.Data.Repositories.Interfaces;
using TinyMovieShared.API.Models.Dtos;
using TinyMovieShared.API.Models.Entities;
using TinyMovieShared.API.Result;
using TinyMovieShared.API.Services.Interfaces;

namespace TinyMovieShared.API.Services
{
    public class MovieServices : IMovieServices
    {
        private readonly IMovieRepository _repository;
        private readonly IUserRepository _userRepository;
        private readonly IUserMovieRepository _userMovieRepository;
        private readonly IMapper _mapper;

        public MovieServices(IMovieRepository repository, IUserRepository userRepository, IUserMovieRepository userMovieRepository, IMapper mapper)
        {
            _repository = repository;
            _userRepository = userRepository;
            _userMovieRepository = userMovieRepository;
            _mapper = mapper;
        }

        public async Task<ResultEnvelope> GetAll(int? limit, int? offset, string orderBy, string order)
        {
            var validLimit = Math.Max(limit ?? 5, 1);
            var validOffset = Math.Max(offset ?? 1, 1);

            var orderByOptions = new string[] { "Name", "Stars", "Director" };
            var orderOptions = new string[] { "asc", "desc" };

            var validOrder = order;
            var validOrderBy = orderBy;

            if (!orderByOptions.Contains(orderBy)) {
                validOrderBy = "Name";
            }
            if (!orderOptions.Contains(order)) {
                validOrder = "asc";
            }

            var movies = await _repository.GetAllPaginated(validLimit, validOffset, validOrderBy, validOrder);

            var moviesViewModel = _mapper.Map<IEnumerable<MovieDTO>>(movies);

            return ResultEnvelope.Success(new { items = moviesViewModel, totalPages = movies.TotalPages, pageIndex = movies.PageIndex });
        }

        public async Task<ResultEnvelope> GetById(string id)
        {
            var parser = Guid.TryParse(id, out var guid);

            if (!parser)
            {
                return ResultEnvelope.Failure("Movie not found", 404);
            }

            var movie = await _repository.GetById(guid);

            if(movie == null)
            {
                return ResultEnvelope.Failure("Movie not found", 404);
            }

            var movieViewModel = _mapper.Map<MovieDTO>(movie);

            return ResultEnvelope.Success(movieViewModel);
        }

        public async Task<ResultEnvelope> Register(RegisterMovieDTO movie)
        {
            var entityMovie = _mapper.Map<Movie>(movie);

            await _repository.Create(entityMovie);

            return ResultEnvelope.Success();
        }

        public Task<ResultEnvelope> Unregister(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<ResultEnvelope> Vote(int stars, string id, string username)
        {
            var parser = Guid.TryParse(id, out var guid);

            if(stars < 0 || stars > 4)
            {
                return ResultEnvelope.Failure("Invalid vote");
            }

            if (!parser)
            {
                return ResultEnvelope.Failure("Movie not found", 404);
            }

            var movie = await _repository.GetById(guid);

            if (movie == null)
            {
                return ResultEnvelope.Failure("Movie not found", 404);
            }

            var user = await _userRepository.GetUserByUsername(username);

            if (user == null)
            {
                return ResultEnvelope.Failure("Invalid Operation", 400);
            }

            var movieAlreadyVotedByUser = await _userMovieRepository.UserAlreadyVoted(user.Id, movie.Id);

            if (movieAlreadyVotedByUser)
            {
                return ResultEnvelope.Failure("Already voted");
            }

            var newStars = (movie.TotalVotes * movie.Stars ?? 0 + stars) / (movie.TotalVotes + 1);
            movie.ChangeStars(newStars);
            movie.ChangeTotalVotes(movie.TotalVotes + 1);

            await _repository.Update(movie);
            await _userMovieRepository.RegisterVote(new UserMovie() { MovieId = movie.Id, UserId = user.Id });

            return ResultEnvelope.Success();
        }
    }
}
