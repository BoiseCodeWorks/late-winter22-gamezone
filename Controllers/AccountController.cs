using System;
using System.Threading.Tasks;
using gamezone.Models;
using gamezone.Services;
using CodeWorks.Auth0Provider;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace gamezone.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class AccountController : ControllerBase
  {
    private readonly AccountService _accountService;

    private readonly GamesService _gService;

    public AccountController(AccountService accountService, GamesService gService)
    {
      _accountService = accountService;
      _gService = gService;
    }

    [HttpGet]
    [Authorize]
    public async Task<ActionResult<Account>> Get()
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        return Ok(_accountService.GetOrCreateProfile(userInfo));
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpGet("games")]
    [Authorize]
    public async Task<ActionResult<List<GameViewModel>>> GetMyGames()
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        List<GameViewModel> games = _gService.GetGamesByAccountId(userInfo.Id);
        return Ok(games);
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }
  }


}