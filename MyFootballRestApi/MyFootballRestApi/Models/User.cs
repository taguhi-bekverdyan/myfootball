using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyFootballRestApi.Models
{
  public class User:EntityBase<User>
  {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Birthdate { get; set; }
        public string FavoriteTeamId { get; set; }
        public string Image { get; set; }
        public string Email { get; set; }

    }
}
