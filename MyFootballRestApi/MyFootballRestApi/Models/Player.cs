namespace MyFootballRestApi.Models
{
  public class Player : EntityBase<Player>
  {
    public string firstName { get; set; }
    public string lastName { get; set; }
    public int age { get; set; }
    public User user { get; set; }
    public Position position { get; set; }
    public byte[] avatar { get; set; }
    public PhysicalStats physicalStats { get; set; }
    public bool hasTrained { get; set; }
    public int teamId { get; set; }

    // if has teamId
    public int number { get; set; }

    // if has teamId
    public bool isCaptain { get; set; }
  }
}
