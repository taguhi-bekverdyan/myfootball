using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyFootballMvc.Models
{
    public enum ResponseParam
    {
        Accept,
        DontAccept
    }

    public class Response
    {
        public ResponseParam ResponseParam { get; set; }
        public string RequestId { get; set; }
    }

}
