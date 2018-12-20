using Microsoft.AspNetCore.Mvc;
using MyFootballRestApi.Data;
using MyFootballRestApi.Models;

namespace MyFootballRestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TournamentController : ControllerBase
    {

        private readonly IRepository<Tournament> _tournamentRepository = new CouchbaseRepository<Tournament>();

        // GET: api/Tournament
        [HttpGet]
        public IActionResult Get()
        {
            var players = _tournamentRepository.GetAll(typeof(Tournament));
            return Ok(players);
        }

        // GET: api/Tournament/5
        [HttpGet("Get/{id}")]
        public IActionResult Get(string id)
        {
            var player = _tournamentRepository.Get(id);
            return Ok(player);
        }

        [HttpPost("Create")]
        public IActionResult Create([FromBody] Tournament tournament)
        {
            var result = _tournamentRepository.Create( tournament);
            if (result == null) return BadRequest(tournament);
            return Created($"/api/Player/Get/{tournament.Id.ToString()}", result);
        }

        [HttpPost("Upsert")]
        public IActionResult Upsert([FromBody] Tournament tournament)
        {
            var result = _tournamentRepository.Upsert(tournament);
            if (result == null) return BadRequest(tournament);
            return Created($"/api/Player/Get/{tournament.Id.ToString()}", result);
        }

        [HttpPut("Update")]
        public IActionResult Update([FromBody] Tournament tournament)
        {
            var result = _tournamentRepository.Update( tournament);
            if (result == null) return BadRequest(tournament);
            return Ok(result);
        }

        [HttpDelete("Delete/{id}")]
        public IActionResult Delete(string id)
        {
            _tournamentRepository.Delete(id);
            return NoContent();
        }
    }
}