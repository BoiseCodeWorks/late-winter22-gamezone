using gamezone.Models;
using gamezone.Repositories;

namespace gamezone.Services
{
  public class GamePlayersService
  {
    private readonly GamePlayersRepository _gpRepo;

    public GamePlayersService(GamePlayersRepository gpRepo)
    {
      _gpRepo = gpRepo;
    }

    internal GamePlayer Create(GamePlayer gamePlayerData)
    {
      return _gpRepo.Create(gamePlayerData);
    }
  }
}