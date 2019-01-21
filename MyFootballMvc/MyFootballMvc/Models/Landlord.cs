using System.ComponentModel.DataAnnotations;

namespace MyFootballMvc.Models
{
  public class Landlord : EntityBase<Landlord>
  {
    [Required(ErrorMessage = "Please enter your Organization name")]
    public string Organization { get; set; }
    public User User { get; set; }
  }
}
