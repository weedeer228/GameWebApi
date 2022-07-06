using Models;

namespace GameWebApi.Models
{
    public class GameGenre
    {
        public GameGenre()
        {
            Id = Guid.NewGuid();

        }
        public Guid Id { get; set; }
        public Guid GameId { get; set; }
        public Guid GenreId { get; set; }
        public string GenreName { get; set; }
    }
}
