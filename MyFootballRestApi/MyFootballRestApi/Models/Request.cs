using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyFootballRestApi.Models
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
        Referee,
        Staff,
        Coach
    }

    public class Request:EntityBase<Request>
    {
        [Required]
        public Team Team { get; set; }
        [Required]
        public string UserId { get; set; }
        [Required]
        public RequestStatus RequestStatus { get; set; }
        [Required]
        public RequestTo RequestTo { get; set; }
        public string Message { get; set; }
    }
}
