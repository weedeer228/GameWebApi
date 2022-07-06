using GameWebApi.EntityFramework;
using GameWebApi.Models;
using GameWebApi.Models.Requests;
using Microsoft.AspNetCore.Mvc;

namespace GameWebApi.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class GamesController : ControllerBase
    {
        private readonly EFGameRepository _repository;
        public GamesController(EFGameDBContext context)
        {
            _repository = new(context);

        }
        [HttpGet]
        public IActionResult GetGames() => Ok(_repository.GetAllAsync());
        [HttpGet("GetByGenres")]
        public async Task<IActionResult> GetGamesByGenre([FromQuery] string[] value, [FromQuery] bool fullMatch = false)
        {
            var result = await _repository.GetAllByGenre(value, fullMatch);
            return Ok(result.ToList());
        }
        [HttpPost]
        public async Task<IActionResult> AddGame([FromQuery] AddGameRequest request) => Ok(await _repository.Get(await _repository.CreateAsync(request.Name, request.Developer, request.Genres)));
        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateGame([FromRoute] Guid id, [FromQuery] UpdateGameRequest request)
        {
            var game = await _repository.Get(id);
            var updatedGame = await request.Update(game);
            await _repository.Update(updatedGame, request.Genres);
            return Ok(game);
        }
        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetGame([FromRoute] Guid id)
        {
            var game = await _repository.Get(id);
            if (game is null) return NotFound();
            return Ok(game);
        }
        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteGame([FromRoute] Guid id)
        {
            var game = await _repository.Get(id);
            if (game is null) return BadRequest("game didnt exist");
            await _repository.Delete(id);
            return Ok(game);
        }


    }
}
