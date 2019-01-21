using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyFootballAdmin.Data.Models
{
    public class Referee:EntityBase<Referee>
    {
        public string License { get; set; }
        public User User { get; set; }

        public Referee()
        {
            Created = Updated = DateTime.Now;
        }
    }
}
