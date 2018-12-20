using System;

namespace MyFootballRestApi.Models
{
  public class Player : EntityBase<Player>
  {
        
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int Age { get; set; }
    public User User { get; set; }
    public Position Position { get; set; }
    public byte[] Avatar { get; set; }
    public PhysicalStats PhysicalStats { get; set; }
    public bool HasTrained { get; set; }
    public int TeamId { get; set; }

    // if has teamId
    public int Number { get; set; }

    // if has teamId
    public bool IsCaptain { get; set; }
  }
}
