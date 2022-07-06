using GameWebApi.Models;
using Models;

namespace GameWebApi.Abstractions.Interfaces
{
    public interface IGameRepository : IRepository<Game>
    {
        Task<IEnumerable<Game>> GetAllByGenre(string[] genres, bool fullMatch);
        Task<Guid> CreateAsync(string name, string developer, string[] genres);
    }
}
