using Models;

namespace Abstractions
{
    public interface IGameRepository : IRepository
    {
        IEnumerable<Game> GetAll();
        Game Get(int id);
        void Create(Game game);
        void Update(Game updatedGame);
        void Delete(int id);
        void Delete(Game game);
        IEnumerable<Game> GetAllByGenre(string genre);
    }
}
