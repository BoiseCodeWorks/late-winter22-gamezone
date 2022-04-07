namespace gamezone.Models
{
  public class Game
  {
    public int? Id { get; set; }
    public string Name { get; set; }
    public string creatorId { get; set; }
    public int RecommendedPlayers { get; set; }
    public string Description { get; set; }

    // virtuals
    public Account? Creator { get; set; }
  }
}