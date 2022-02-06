namespace TinyMovieShared.API.Config
{
    public interface ISettings
    {
        public string TokenKey { get; set; }
    }

    public class Settings : ISettings
    {
        public string TokenKey { get; set; }

        public Settings(string tokenKey)
        {
            TokenKey = tokenKey;
        }

    }
}
