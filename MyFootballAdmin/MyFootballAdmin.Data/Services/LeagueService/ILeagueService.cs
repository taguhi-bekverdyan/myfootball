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
        Task Create(League league);
        Task Update(League league);
        Task Delete(string id);
        Task<List<League>> FindAll();
        Task<League> FindLeagueById(string id);
        Task<League> FindLeagueByName(string id);
        Task<League> FindLeagueByStartDate(string startDate);
    }
}
