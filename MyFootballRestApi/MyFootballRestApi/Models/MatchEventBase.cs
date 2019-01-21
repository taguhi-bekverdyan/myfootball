using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFootballRestApi.Models
{
    public abstract class MatchEventBase
    {
        Guid ClubId { get; set; }
        byte Time { get; set; }
    }
}
