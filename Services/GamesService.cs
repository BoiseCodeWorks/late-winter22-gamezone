using System;
using System.Collections.Generic;
using gamezone.Models;
using gamezone.Repositories;

namespace gamezone.Services
{
  public class GamesService
  {
    private readonly GamesRepository _gamesRepo;

    public GamesService(GamesRepository gamesRepo)
    {
      _gamesRepo = gamesRepo;
    }

    internal List<Game> GetAll()
    {
      return _gamesRepo.GetAll();
    }

    internal Game Create(Game gameData)
    {
      return _gamesRepo.Create(gameData);
    }

    internal string Remove(int id, Account user)
    {
      Game game = _gamesRepo.GetById(id);
      if (game.creatorId != user.Id)
      {
        throw new Exception("you can't do that nice try.");
      }
      return _gamesRepo.Remove(id);
    }

    internal List<GameViewModel> GetGamesByAccountId(string id)
    {
      return _gamesRepo.GetGamesByAccountId(id);
    }
  }
}