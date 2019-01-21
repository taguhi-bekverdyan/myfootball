using MyFootballAdmin.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFootballAdmin.Data.Services.TeamsService
{
    public interface ITeamsService
    {
        Task Create(Team team);
        Task Update(Team team);
        Task Delete(string id);
        Task<List<Team>> FindAll();
        Task<Team> FindTeamById(string id);
    }
}
