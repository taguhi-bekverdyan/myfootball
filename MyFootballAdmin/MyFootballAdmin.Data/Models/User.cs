using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyFootballAdmin.Data.Models
{
  public class User:EntityBase<User>
  {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Birthdate { get; set; }
        public string FavoriteTeamId { get; set; }
        

    }
}
