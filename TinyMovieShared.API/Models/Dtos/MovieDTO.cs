namespace TinyMovieShared.API.Models.Dtos
{
    public class MovieDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Director { get; set; }
        public float? Stars { get; set; }
        public long TotalVotes { get; set; }
        public string Genre { get; set; }
        public string CreatedAt { get; set; }
    }
}
