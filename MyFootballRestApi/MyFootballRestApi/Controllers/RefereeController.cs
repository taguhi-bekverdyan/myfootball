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
    public class RefereeController : ControllerBase
    {

        private readonly IRepository<Referee> _refereeRepository;

        public RefereeController()
        {
            _refereeRepository = new CouchbaseRepository<Referee>();
        }



        #region GET
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var referees = await _refereeRepository.GetAll(typeof(Referee));
                return Ok(referees);
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetrefereeById([FromRoute]string id)
        {
            try
            {
                var referee = await _refereeRepository.Get(id);
                return Ok(referee);
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }

        [HttpGet("by_user_id/{id}")]
        public async Task<IActionResult> GetRefereeByUserId([FromRoute]string id)
        {
            try
            {
                List<Referee> referee = await _refereeRepository.GetAll(typeof(Referee));
                var r = referee.FirstOrDefault(p => p.User.Id == id);
                if (r == null)
                {
                    return NotFound();
                }
                return Ok(r);
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }

        #endregion

        #region POST

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] Referee referee)
        {
            try
            {
                referee.Id = Guid.NewGuid().ToString();
                var result = await _refereeRepository.Create(referee);
                if (result == null) { return BadRequest(result); }
                return Created(string.Format("/api/Coach/{0}", referee.Id), result);
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }

        [HttpPost("Upsert")]
        public async Task<IActionResult> Upsert([FromBody] Referee referee)
        {
            try
            {
                var result = await _refereeRepository.Upsert(referee);
                if (result == null) { return BadRequest(); }
                return Created(string.Format("/api/Coach/{0}", referee.Id), result);
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }

        #endregion

        #region PUT

        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromBody]Referee referee)
        {
            try
            {
                var result = await _refereeRepository.Update(referee);
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
                await _refereeRepository.Delete(id);
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