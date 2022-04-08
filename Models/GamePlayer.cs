namespace gamezone.Models
{
  public class GamePlayer
  {
    public int Id { get; set; }
    public int GameId { get; set; }
    public string AccountId { get; set; }
    public int Score { get; set; }
  }
}