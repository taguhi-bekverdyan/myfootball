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
    public class CoachController : ControllerBase
    {

        private readonly IRepository<Coach> _coachRepository;
        public CoachController()
        {
            _coachRepository = new CouchbaseRepository<Coach>();
        }

        #region GET
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var coaches = await _coachRepository.GetAll(typeof(Coach));
                return Ok(coaches);
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCoachById([FromRoute]string id)
        {
            try
            {
                var coach = await _coachRepository.Get(id);
                return Ok(coach);
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }

        [HttpGet("by_user_id/{id}")]
        public async Task<IActionResult> GetCoachByUserId([FromRoute]string id) {
            try
            {
                List<Coach> couches = await _coachRepository.GetAll(typeof(Coach));
                var coach = couches.FirstOrDefault(c => c.User.Id == id);
                if (couches == null) {
                    return NotFound();
                }
                return Ok(coach);
            }
            catch (Exception e)
            {
                return StatusCode(500,e);
            }
        }

        [HttpGet("free_coaches")]
        public async Task<IActionResult> GetFreeCouches()
        {
            try
            {
                List<Coach> all = await _coachRepository.GetAll(typeof(Coach));
                List<Coach> freeCoaches = (from c in all
                                           where c.CoachStatus == CoachStatus.FreeCoach
                                           select c).ToList();
                return Ok(freeCoaches);
            }
            catch (Exception e)
            {
                return StatusCode(500,e);
            }
        }

        #endregion

        #region POST

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] Coach coach)
        {
            try
            {
                coach.Id = Guid.NewGuid().ToString();
                var result = await _coachRepository.Create(coach);
                if (result == null) { return BadRequest(result); }
                return Created(string.Format("/api/Coach/{0}", coach.Id), result);
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }

        [HttpPost("Upsert")]
        public async Task<IActionResult> Upsert([FromBody] Coach coach)
        {
            try
            {
                var result = await _coachRepository.Upsert(coach);
                if (result == null) { return BadRequest(); }
                return Created(string.Format("/api/Coach/{0}", coach.Id), result);
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }

        #endregion

        #region PUT

        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromBody]Coach coach)
        {
            try
            {
                var result = await _coachRepository.Update(coach);
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
                await _coachRepository.Delete(id);
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