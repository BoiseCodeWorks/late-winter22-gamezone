using System.Data;
using Dapper;
using gamezone.Models;

namespace gamezone.Repositories
{
  public class GamePlayersRepository
  {
    private readonly IDbConnection _db;

    public GamePlayersRepository(IDbConnection db)
    {
      _db = db;
    }

    internal GamePlayer Create(GamePlayer gamePlayerData)
    {
      string sql = @"
    INSERT INTO gameplayers
    (gameId, accountId, score)
    VALUES
    (@GameId, @AccountId, @Score);
    SELECT LAST_INSERT_ID();
      ";
      int id = _db.ExecuteScalar<int>(sql, gamePlayerData);
      gamePlayerData.Id = id;
      return gamePlayerData;
    }
  }
}