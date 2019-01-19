using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyFootballRestApi.Models
{
    public class Email: EntityBase<Email>
    {
        public string Address { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
    }
}
