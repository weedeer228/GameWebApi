using GameWebApi.Abstractions.AbstractClasses;
using Models;

namespace GameWebApi.Models.Requests
{
    public class AddGameRequest : AddRequest<Game>
    {
        public string Name { get; set; }
        public string Developer { get; set; }
        public string[] Genres { get; set; }
        public override Game GetNewObject() => new Game() { Name = this.Name, Developer = this.Developer };
    }
}
