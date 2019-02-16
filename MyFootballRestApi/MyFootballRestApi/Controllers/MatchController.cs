using Microsoft.AspNetCore.Mvc;
using MyFootballRestApi.Data;
using MyFootballRestApi.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyFootballRestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MatchController : ControllerBase
    {

        private readonly IRepository<Match> _matchRepository;
        private readonly IRepository<League> _leagueRepository;

        public MatchController()
        {
            _matchRepository = new CouchbaseRepository<Match>();
            _leagueRepository = new CouchbaseRepository<League>();
        }


        #region GET
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var leagues = await _leagueRepository.GetAll(typeof(League));
                List<Match> matches = new List<Match>();
                foreach (var league in leagues)
                {
                    foreach (var tour in league.Tour)
                    {
                        if (tour.Matches != null && tour.Matches.Count != 0)
                        {
                            matches.AddRange(tour.Matches);
                        }
                    }
                }
                return Ok(matches);
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }

        [HttpGet("{leagueId}/{id}")]
        public async Task<IActionResult> GetMatchById([FromRoute]string id, [FromRoute]string leagueId)
        {
            try
            {
                var league = await _leagueRepository.Get(leagueId);
                var tours = league.Tour;
                Match match = new Match();
                foreach (var tour in tours)
                {
                    foreach(var mh in tour.Matches)
                    {
                        if(mh.Id == id)
                        {
                            match = mh;
                            break;
                        }
                    }
                }
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
                match.Id = Guid.NewGuid().ToString();
                var result = await _matchRepository.Create(match);
                if (result == null) { return BadRequest(result); }
                return Created(string.Format("/api/Matches/{0}", match.Id), result);
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