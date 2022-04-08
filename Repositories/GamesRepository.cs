using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using gamezone.Models;

namespace gamezone.Repositories
{
  public class GamesRepository
  {
    private readonly IDbConnection _db;

    public GamesRepository(IDbConnection db)
    {
      _db = db;
    }

    internal List<Game> GetAll()
    {
      string sql = @"
      SELECT
      g.*,
      a.*
      FROM games g
      JOIN accounts a WHERE a.id = g.creatorId;
      ";
      // NOTE     first (g) second (a) third (return type)
      return _db.Query<Game, Account, Game>(sql, (game, account) =>
      {
        // NOTE this is the populate creator
        game.Creator = account;
        return game;
      }).ToList();
    }

    internal Game GetById(int id)
    {
      string sql = @"
      SELECT 
        g.*,
        a.* 
      FROM games g
      JOIN accounts a ON g.creatorId = a.id
      WHERE g.id = @id;
      ";
      return _db.Query<Game, Account, Game>(sql, (game, account) =>
      {
        // NOTE this is the populate creator
        game.Creator = account;
        return game;
      }, new { id }).FirstOrDefault();
    }

    internal List<GameViewModel> GetGamesByAccountId(string id)
    {
      string sql = @"
          SELECT
            a.*,
            gp.*,
            g.*
          FROM gameplayers gp
          JOIN games g ON gp.gameId = g.id
          JOIN accounts a ON g.creatorId = a.id
          WHERE gp.accountId = @id;
      ";
      List<GameViewModel> games = _db.Query<Account, GamePlayer, GameViewModel, GameViewModel>(sql, (a, gp, g) =>
      {
        g.Creator = a;
        g.Score = gp.Score;
        g.GamePlayerId = gp.Id;
        return g;
      }, new { id }).ToList<GameViewModel>();
      return games;
    }

    internal Game Create(Game gameData)
    {
      string sql = @"
      INSERT INTO games
      (name, description, recommendedPlayers, creatorId)
      VALUES
      (@Name, @Description, @RecommendedPlayers, @CreatorId);
      SELECT LAST_INSERT_ID();
      ";
      int id = _db.ExecuteScalar<int>(sql, gameData);
      gameData.Id = id;
      return gameData;
    }

    internal string Remove(int id)
    {
      string sql = @"
        DELETE FROM games WHERE id = @id LIMIT 1;
      ";
      int rowsAffected = _db.Execute(sql, new { id });
      if (rowsAffected > 0)
      {
        return "delorted";
      }
      throw new Exception("could not delete");
    }

  }
}