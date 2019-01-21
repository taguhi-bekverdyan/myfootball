using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyFootballMvc.Models
{
    public enum RequestStatus
    {
        InProgress,
        Canceled,
        Accepted
    }

    public enum RequestTo
    {
        Player,
        Staff,
        Coach
    }

    public class Request : EntityBase<Request>
    {
        public Team Team { get; set; }
        public string UserId { get; set; }
        public RequestStatus RequestStatus { get; set; }
        [Required]
        public RequestTo RequestTo { get; set; }
        public string Message { get; set; }
    }
}
