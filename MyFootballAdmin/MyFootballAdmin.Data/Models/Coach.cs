
using System;

namespace MyFootballAdmin.Data.Models

{
    public class Coach:EntityBase<Coach>
    {
        
        public string License { get; set; }
        public CoachStatus CoachStatus { get; set; }
        public string TeamId { get; set; }
        public User User { get; set; }
        public string Avatar { get; set; }

        public Coach()
        {
            Created = Updated = DateTime.Now;
        }
    }
}
