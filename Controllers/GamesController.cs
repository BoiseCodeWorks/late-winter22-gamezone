using System.Collections.Generic;
using System.Threading.Tasks;
using CodeWorks.Auth0Provider;
using gamezone.Models;
using gamezone.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace gamezone.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class GamesController : ControllerBase
  {
    private readonly GamesService _gameService;

    public GamesController(GamesService gameService)
    {
      _gameService = gameService;
    }

    [HttpGet]
    public ActionResult<List<Game>> GetAll()
    {
      try
      {
        List<Game> games = _gameService.GetAll();
        return Ok(games);
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<Game>> Create([FromBody] Game gameData)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        gameData.creatorId = userInfo.Id;
        Game game = _gameService.Create(gameData);
        game.Creator = userInfo;
        return Created($"api/games/{game.Id}", game);
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpDelete("{id}")]
    [Authorize]
    public async Task<ActionResult<string>> Remove(int id)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        return Ok(_gameService.Remove(id, userInfo));
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }

  }
}