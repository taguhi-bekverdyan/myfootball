using Microsoft.AspNetCore.Mvc;
using MyFootballRestApi.Data;
using MyFootballRestApi.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace MyFootballRestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CupController : ControllerBase
    {

        private readonly IRepository<Cup> _cupRepository;

        public CupController()
        {
            _cupRepository = new CouchbaseRepository<Cup>();
        }

        #region GET
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var cup = await _cupRepository.GetAll(typeof(Cup));
                return Ok(cup);
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCupById([FromRoute]string id)
        {
            try
            {
                var cup = await _cupRepository.Get(id);
                return Ok(cup);
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }

        [HttpGet("{name}")]
        public async Task<IActionResult> GetCupByName([FromRoute]string name)
        {
            try
            {
                List<Cup> cups = await _cupRepository.GetAll(typeof(Cup));
                var cup = cups.FirstOrDefault(p => p.Tournament.Name == name);
                if (cup == null)
                {
                    return NotFound();
                }
                return Ok(cup);
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }

        [HttpGet("{startDate}")]
        public async Task<IActionResult> GetCupByStartDate([FromRoute]string startDate)
        {
            try
            {
                DateTime startdate = DateTime.ParseExact(startDate,"dd-mm-yyyy", CultureInfo.InvariantCulture);
                List<Cup> cups = await _cupRepository.GetAll(typeof(Cup));
                var cup = cups.FirstOrDefault(p => p.StartDate == startdate);
                if (cup == null)
                {
                    return NotFound();
                }
                return Ok(cup);
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }

        #endregion

        #region POST

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody]Cup cup)
        {
            try
            {
                cup.Id = Guid.NewGuid().ToString();
                var result = await _cupRepository.Create(cup);
                if (result == null) { return BadRequest(result); }
                return Created(string.Format("/api/Cups/{0}", cup.Id), result);
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }

        [HttpPost("Upsert")]
        public async Task<IActionResult> Upsert([FromBody]Cup cup)
        {
            try
            {
                var result = await _cupRepository.Upsert(cup);
                if (result == null) { return BadRequest(); }
                return Created(string.Format("/api/Leagues/{0}", cup.Id), result);
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }

        #endregion

        #region PUT

        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromBody]Cup cup)
        {
            try
            {
                var result = await _cupRepository.Update(cup);
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
                await _cupRepository.Delete(id);
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
