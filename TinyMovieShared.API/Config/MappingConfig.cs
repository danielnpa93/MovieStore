using AutoMapper;
using TinyMovieShared.API.Models.Dtos;
using TinyMovieShared.API.Models.Entities;

namespace TinyMovieShared.API.Config
{
    public class MappingConfig
    {
        public static MapperConfiguration ResgisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<CreateUserDTO, User>()
                .ConstructUsing(user => new User(user.Username, user.Password, user.Role));

                config.CreateMap<User, UserDTO>();

                config.CreateMap<DateTime, string>()
                .ConvertUsing(new DateTimeTypeConverter());

                config.CreateMap<RegisterMovieDTO, Movie>()
                .ConstructUsing(movie => new Movie(movie.Name,movie.Director, movie.Genre));

                config.CreateMap<Movie, MovieDTO>();
            });

            return mappingConfig;
        }
    }

    public class DateTimeTypeConverter : ITypeConverter<DateTime, string>
    {
        public string Convert(DateTime source, string destination, ResolutionContext context)
        {
            return source.ToString("yyyy-MM-ddTHH:mm:ssZ");
        }
    }
}
