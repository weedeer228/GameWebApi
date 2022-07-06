using GameWebApi.Abstractions.AbstractClasses;
using Models;

namespace GameWebApi.Models.Requests
{
    public class UpdateGameRequest : UpdateRequest<Game>
    {
        public string Name { get; set; }
        public string Developer { get; set; }
        public string[] Genres { get; set; }
        public override async Task<Game> Update(Game game)
        {
            game.Name = Name;
            game.Developer = Developer;
            return game;
        }
    }



}
