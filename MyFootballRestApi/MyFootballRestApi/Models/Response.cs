using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyFootballRestApi.Models
{
    public class Response:EntityBase<Response>
    {
        public Request Request { get; set; }
        public string UserId { get; set; }
    }
}
