using MyFootballAdmin.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFootballAdmin.Data.Services.LeagueService
{
    public interface ILeagueService
    {
        //Task Insert(League league);
        Task Update(League league);
        Task Delete(Guid id);
        Task<List<League>> FindAll();
        Task<League> FindLeagueById(Guid id);
    }
}
