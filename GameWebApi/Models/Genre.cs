using Models;
using System.ComponentModel.DataAnnotations;

namespace GameWebApi.Models
{
    public class Genre
    {
        [Key]
        public Guid ID { get; set; }
        public Genre(string name)
        {
            ID = Guid.NewGuid();
            Name = name;
            GameGenres = new  HashSet<GameGenre>();

        }

        public string Name { get; set; }

        public ICollection<GameGenre> GameGenres { get; set; }
    }
}
