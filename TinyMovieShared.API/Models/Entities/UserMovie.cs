namespace TinyMovieShared.API.Models.Entities
{
    public class UserMovie
    {
        public Guid UserId { get; set; }
        public Guid MovieId { get; set; }
    }
}
