using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyFootballRestApi.Models;
using Newtonsoft.Json;

namespace MyFootballRestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamsController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> FindAll()
        {
            try
            {
                return Ok(await GetTeams());
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }

        [HttpGet("{guid}")]
        public async Task<IActionResult> FindTeamById([FromRoute]Guid guid)
        {
            try
            {
                List<Team> teams = await GetTeams();
                Team team = teams.FirstOrDefault(t => t.Id == guid);
                if (team != null) { return Ok(team); }
                return NotFound();
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }

        private Task<List<Team>> GetTeams()
        {
            return Task<List<Team>>.Factory.StartNew(() => {
                string userJson;
                try
                {
                    using (StreamReader sr = new StreamReader(@"JsonData/Teams.json"))
                    {
                        userJson = sr.ReadToEnd();
                    }
                }
                catch (Exception)
                {

                    throw;
                }
                return JsonConvert.DeserializeObject<List<Team>>(userJson);
            });
        }
    }
}