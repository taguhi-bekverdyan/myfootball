using MyFootballRestApi.Models;

namespace MyFootballMvc.Models
{
  public class Landlord : EntityBase<Landlord>
  {
    public string Organization { get; set; }
    public User User { get; set; }
  }
}
