using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyFootballRestApi.Data;
using MyFootballRestApi.Models;

namespace MyFootballRestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamsController : ControllerBase
    {

        private readonly IRepository<Team> _teamsRepository;

        public TeamsController()
        {
            _teamsRepository = new CouchbaseRepository<Team>();
        }

        #region GET
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var teams = _teamsRepository.GetAll(typeof(Team));
                return Ok(teams);
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetTeamById([FromRoute]string id)
        {
            try
            {
                var team = _teamsRepository.Get(id);
                if (team == null) { return NotFound(); }
                return Ok(team);
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }
        #endregion

        #region POST

        [HttpPost]
        public IActionResult Create([FromBody]Team team,string id)
        {
            try
            {
                var result = _teamsRepository.Create(id,team);
                if (result == null) { return BadRequest(team); }
                return Created($"/api/Teams/{id}",result);
            }
            catch (Exception e)
            {
                return StatusCode(500,e);
            }
        }

        [HttpPost("Upsert")]
        public IActionResult Upsert([FromBody] Team team, string id)
        {
            try
            {
                var result = _teamsRepository.Upsert(id, team);
                if (result == null) return BadRequest(team);
                return Created($"/api/teams/{id}", result);
            }
            catch (Exception e)
            {
                return StatusCode(500,e);
            }
        }

        #endregion

        #region PUT
        [HttpPut]
        public IActionResult Update([FromBody] Team team, string id)
        {
            try
            {
                var result = _teamsRepository.Update(id, team);
                if (result == null) return BadRequest(team);
                return Ok(result);
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }
        #endregion

        #region DELETE

        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {      
            try
            {
                _teamsRepository.Delete(id);
                return NoContent();
            }
            catch (Exception e)
            {
                return StatusCode(500,e);
            }
        }

        #endregion

    }
}