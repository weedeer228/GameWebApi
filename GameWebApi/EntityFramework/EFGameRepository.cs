using GameWebApi.Abstractions.Interfaces;
using GameWebApi.Models;
using Microsoft.EntityFrameworkCore;
using Models;

namespace GameWebApi.EntityFramework
{
    public class EFGameRepository : IGameRepository
    {

        private EFGameDBContext _context;
        public EFGameRepository(EFGameDBContext context)
        {
            _context = context;
        }
        private async Task<GameGenre> GetOrCreateGameGenre(string genre, Guid gameId)
        {
            if (await _context.GameGenres.SingleOrDefaultAsync(x => x.GameId == gameId && x.GenreName == genre)
                is GameGenre existingGenre
                && existingGenre is not null)
                return existingGenre;


            var newGenre = new Genre(genre);
            if (!_context.Genres.Any(x => x.Name == genre))
                await _context.Genres.AddAsync(newGenre);
            else
                newGenre = await _context.Genres.SingleAsync(x => x.Name == genre);
            var gameGenre = new GameGenre() { GameId = gameId, GenreId = newGenre.ID, GenreName = newGenre.Name };
            await _context.GameGenres.AddAsync(gameGenre);
            newGenre.GameGenres.Add(gameGenre);
            return gameGenre;
        }
        public async Task<Game> Get(Guid id)
        {
            var game = await _context.Games.Include(x => x.GameGenres).FirstOrDefaultAsync(x => x.ID == id);
            if (game == null) return null;
            return game;
        }
        public async Task Update(Game updatedGame)
        {
            var currentGame = await Get(updatedGame.ID);
            currentGame.Name = updatedGame.Name;
            currentGame.GameGenres = updatedGame.GameGenres;
            currentGame.Developer = updatedGame.Developer;
            _context.Games.Update(currentGame);
            // await _context.SaveChangesAsync();
        }
        public async Task Update(Game updatedGame, string[] genres)
        {
            var currentGame = await Get(updatedGame.ID);
            await Update(updatedGame);
            currentGame.GameGenres.Clear();
            foreach (var genre in genres)
                currentGame.GameGenres.Add(await GetOrCreateGameGenre(genre, currentGame.ID));
            await _context.SaveChangesAsync();
        }
        public async Task Delete(Guid id)
        {
            var game = await Get(id);
            if (game is null) return;
            _context.Games.Remove(await Get(id));
            await _context.SaveChangesAsync();
        }
        public async Task Delete(Game game)
        {
            _context.Games.Remove(game);
            await _context.SaveChangesAsync();
        }
        public async Task<IEnumerable<Game>> GetAllByGenre(string[] genreNames, bool fullMatch)
        {
            var genreIds = _context.Genres.Where(x => genreNames.Any(y => x.Name == y)).Select(x => x.ID);
            if (fullMatch)
                return await _context.Games.Include(x => x.GameGenres).Where(x => genreIds.All(y => x.GameGenres.Any(z => z.GenreId == y))).ToListAsync();
            return await _context.Games.Include(x => x.GameGenres).Where(x => genreIds.Any(y => x.GameGenres.Any(z => z.GenreId == y))).ToListAsync();

        }
        public async Task<Guid> CreateAsync(string name, string developer, string[] genres)
        {
            var game = new Game() { Name = name, Developer = developer };
            foreach (var genre in genres)
                game.GameGenres.Add(await GetOrCreateGameGenre(genre, game.ID));
            _context.Games.Add(game);
            await _context.SaveChangesAsync();
            return game.ID;
        }
        public async Task<IEnumerable<Game>> GetAllAsync() => await _context.Games.Include(x => x.GameGenres).ToListAsync();

    }
}
