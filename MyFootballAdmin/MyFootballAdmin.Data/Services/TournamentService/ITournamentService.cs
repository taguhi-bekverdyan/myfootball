using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyFootballAdmin.Data.Models;

namespace MyFootballAdmin.Data.Services.TournamentService
{
    public interface ITournamentService
    {
        Task Create(Tournament tournament);
        Task Update(Tournament tournament);
        Task Delete(string id);
        Task<List<Tournament>> FindAll();
        Task<Tournament> FindTournamentById(string id);
        Task<Tournament> FindTournamentByName(string name);
        Task<Tournament> FindTournamentByStartDate(string startDate);
    }
}
