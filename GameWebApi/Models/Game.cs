
using GameWebApi.Models;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class Game
    {
        public Game()
        {
            ID = Guid.NewGuid();
            GameGenres = new HashSet<GameGenre>();

        }
        public Guid ID { get; set; }
        public string Name { get; set; }
        public string Developer { get; set; }
        public ICollection<GameGenre> GameGenres { get; set; }

    }
}
