using System.ComponentModel.DataAnnotations;

namespace MyFootballMvc.Models
{
  public class Landlord : EntityBase<Landlord>
  {
    [Required(ErrorMessage = "Please enter your Organization name")]
    public string Organization { get; set; }

    [Required(ErrorMessage = "You must provide a phone number")]
    [Display(Name = "Phone")]
    [DataType(DataType.PhoneNumber)]
    [RegularExpression(@"^\(?[-. ]?([0-9]{2})[-. ]?([0-9]{6})$", ErrorMessage = "Not a valid phone number")]
    public string PhoneNumber { get; set; }

    public User User { get; set; }
  }
}
