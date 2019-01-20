using MyFootballAdmin.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFootballAdmin.Data.Services.CupService
{
    public interface ICupService
    {
        Task Create(Cup cup);
        Task Update(Cup cup);
        Task Delete(string id);
        Task<List<Cup>> FindAll();
        Task<Cup> FindCupById(string id);
        Task<Cup> FindCupByName(string id);
        Task<Cup> FindCupByStartDate(string startDate);
    }
}
