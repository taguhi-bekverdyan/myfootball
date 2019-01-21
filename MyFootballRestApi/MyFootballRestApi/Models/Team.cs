using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyFootballRestApi.Models
{
  public class Team : EntityBase<Team>
  {
    [Required]
    public string Name { get; set; }
    [Required]
    public string ShortName { get; set; }
    [Required]
    public User President { get; set; }

    public List<Staff> StaffMembers { get; set; }
    public List<Player> Players { get; set; }
    public List<Coach> Managers { get; set; }
    public List<string> Stats { get; set; }
  }
}
