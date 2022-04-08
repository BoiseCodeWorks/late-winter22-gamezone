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
  public class GamePlayersController : ControllerBase
  {
    private readonly GamePlayersService _gs;

    public GamePlayersController(GamePlayersService gs)
    {
      _gs = gs;
    }
    [Authorize]
    [HttpPost]
    public async Task<ActionResult<GamePlayer>> Create([FromBody] GamePlayer gamePlayerData)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        gamePlayerData.AccountId = userInfo.Id;
        return Ok(_gs.Create(gamePlayerData));

      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }
  }
}