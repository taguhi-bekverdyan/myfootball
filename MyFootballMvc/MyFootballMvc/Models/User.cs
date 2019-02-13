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
        public DateTime Birthdate { get; set; } = DateTime.Now;

        public Guid FavoriteTeamId { get; set; }

        public string Image { get; set; }
        
        public string Email { get; set; }

    }
}
