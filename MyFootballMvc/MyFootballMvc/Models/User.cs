using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyFootballMvc.Models
{
    
    public class User : EntityBase<User>
    {
        [MaxLength(80), Required, Display(Name = "First Name")]
        public string FirstName { get; set; }
        [MaxLength(80), Required, Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Required, Display(Name = "Date of birth")]
        public DateTime Birthdate { get; set; }

        public Guid FavoriteTeamId { get; set; }

        public Player Player { get; set; }
        public Coach Coach { get; set; }
        public Staff Staff { get; set; }
        public Referee Referee { get; set; }

    }
}
