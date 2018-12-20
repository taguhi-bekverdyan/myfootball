using Microsoft.AspNetCore.Mvc;
using MyFootballRestApi.Data;
using MyFootballRestApi.Models;
using System.Threading.Tasks;
using System;

namespace MyFootballRestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TournamentController : ControllerBase
    {

        private readonly IRepository<Tournament> _tournamentRepository = new CouchbaseRepository<Tournament>();

        // GET: api/Tournament
        [HttpGet]
        public async Task<IActionResult> Get()
        {           
            try
            {
                var players = await _tournamentRepository.GetAll(typeof(Tournament));
                return Ok(players);
            }
            catch (Exception e)
            {
                return StatusCode(500,e);
            }
        }

        // GET: api/Tournament/5
        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            try
            {
                var player = _tournamentRepository.Get(id);
                return Ok(player);
            }
            catch (Exception e)
            {
                return StatusCode(500,e);
            }
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] Tournament tournament)
        {
            try
            {
                string id = tournament.Id;
                var result = await _tournamentRepository.Create(id,tournament);
                if (result == null) return BadRequest(tournament);
                return Created($"/api/Tournament/{id}", result);
            }
            catch (Exception e)
            {
                return StatusCode(500,e);
            }
        }

        [HttpPost("Upsert")]
        public async Task<IActionResult> Upsert([FromBody] Tournament tournament)
        {
            try
            {
                string id = tournament.Id;
                var result = await _tournamentRepository.Upsert(id,tournament);
                if (result == null) return BadRequest(tournament);
                return Created($"/api/Tournament/{id}", result);
            }
            catch (Exception e)
            {
                return StatusCode(500,e);
            }
        }

        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromBody] Tournament tournament)
        {
            try
            {
                string id = tournament.Id;
                var result = await _tournamentRepository.Update(id,tournament);
                if (result == null) return BadRequest(tournament);
                return Ok(result);
            }
            catch (Exception e)
            {
                return StatusCode(500,e);
            }
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                await _tournamentRepository.Delete(id);
                return NoContent();
            }
            catch (Exception e)
            {
                return StatusCode(500,e);
            }
        }
    }
}