using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyFootballMvc.Models;
using MyFootballRestApi.Data;

namespace MyFootballRestApi.Controllers
{
  [Route("api/[controller]")]
    [ApiController]
    public class LandlordController : ControllerBase
    {

        private readonly IRepository<Landlord> _landlordRepository;

        public LandlordController()
        {
            _landlordRepository = new CouchbaseRepository<Landlord>();
        }



        #region GET
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var landlords = await _landlordRepository.GetAll(typeof(Landlord));
                return Ok(landlords);
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetlandlordById([FromRoute]string id)
        {
            try
            {
                var landlord = await _landlordRepository.Get(id);
                return Ok(landlord);
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }

        [HttpGet("by_user_id/{id}")]
        public async Task<IActionResult> GetLandlordByUserId([FromRoute]string id)
        {
            try
            {
                List<Landlord> landlord = await _landlordRepository.GetAll(typeof(Landlord));
                var r = landlord.FirstOrDefault(p => p.User.Id == id);
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
        public async Task<IActionResult> Create([FromBody] Landlord landlord)
        {
            try
            {
                landlord.Id = Guid.NewGuid().ToString();
                var result = await _landlordRepository.Create(landlord);
                if (result == null) { return BadRequest(result); }
                return Created(string.Format("/api/Landlord/{0}", landlord.Id), result);
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }

        [HttpPost("Upsert")]
        public async Task<IActionResult> Upsert([FromBody] Landlord landlord)
        {
            try
            {
                var result = await _landlordRepository.Upsert(landlord);
                if (result == null) { return BadRequest(); }
                return Created(string.Format("/api/Landlord/{0}", landlord.Id), result);
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }

        #endregion

        #region PUT

        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromBody]Landlord landlord)
        {
            try
            {
                var result = await _landlordRepository.Update(landlord);
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
                await _landlordRepository.Delete(id);
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