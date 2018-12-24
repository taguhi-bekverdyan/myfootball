using Microsoft.AspNetCore.Mvc;
using MyFootballRestApi.Data;
using MyFootballRestApi.Models;
using System;
using System.Threading.Tasks;

namespace MyFootballRestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MatchController : ControllerBase
    {

        private readonly IRepository<Match> _matchRepository;

        public MatchController()
        {
            _matchRepository = new CouchbaseRepository<Match>();
        }


        #region GET
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var match = await _matchRepository.GetAll(typeof(Match));
                return Ok(match);
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetLeagueById([FromRoute]string id)
        {
            try
            {
                var match = await _matchRepository.Get(id);
                return Ok(match);
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }

        #endregion

        #region POST

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] Match match)
        {
            try
            {
                var result = await _matchRepository.Create(match);
                if (result == null) { return BadRequest(result); }
                return Created(string.Format("/api/Leagues/{0}", match.Id), result);
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }

        [HttpPost("Upsert")]
        public async Task<IActionResult> Upsert([FromBody] Match match)
        {
            try
            {
                var result = await _matchRepository.Upsert(match);
                if (result == null) { return BadRequest(); }
                return Created(string.Format("/api/Leagues/{0}", match.Id), result);
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }

        #endregion

        #region PUT

        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromBody]Match match)
        {
            try
            {
                var result = await _matchRepository.Update(match);
                if (result == null) { return BadRequest(result); }
                return Ok(result);
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }

        #endregion

        #region DELETE

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete([FromRoute]string id)
        {
            try
            {
                await _matchRepository.Delete(id);
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }

        #endregion

    }
}