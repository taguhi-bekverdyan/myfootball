using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyFootballMvc.Models
{
  public class Team : EntityBase<Team>
  {
    [Required]
    [Remote("CheckName", "Teams", ErrorMessage = "Name is not valid.")]
    [Display(Name = "Team full name")]
    public string Name { get; set; }
    [Required]
    [Display(Name = "Team short name")]
    public string ShortName { get; set; }
    
    public User President { get; set; }

    public List<Staff> StaffMembers { get; set; } = new List<Staff>();
    public List<Player> Players { get; set; } = new List<Player>();
    public List<Coach> Managers { get; set; } = new List<Coach>();
    public List<string> Stats { get; set; } = new List<string>();

    public List<String> SentRequests { get; set; }

  }
}
