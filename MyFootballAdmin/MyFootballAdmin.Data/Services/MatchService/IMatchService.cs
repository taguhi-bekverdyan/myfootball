using MyFootballAdmin.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFootballAdmin.Data.Services.MatchService
{
    public interface IMatchService
    {
        Task Create(Match match);
        Task Update(Match match);
        Task Delete(string id);
        Task<List<Match>> FindAll();
        Task<Match> FindMatchById(string id);
    }
}
