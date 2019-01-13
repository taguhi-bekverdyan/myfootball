using System.ComponentModel.DataAnnotations;

namespace MyFootballMvc.Models
{
  public class Landlord
  {
    [Required(ErrorMessage = "Please enter your License")]
    public string Organization { get; set; }
    public User User { get; set; }
  }
}
