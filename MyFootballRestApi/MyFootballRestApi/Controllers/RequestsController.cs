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
    public class RequestsController : ControllerBase
    {
        private readonly IRepository<Request> _requestsRepository = new CouchbaseRepository<Request>();

        // GET: api/Player
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var requests = await _requestsRepository.GetAll(typeof(Request));
                return Ok(requests);
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }

        // GET: api/Player/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRequestById(string id)
        {
            try
            {
                var request = await _requestsRepository.Get(id);
                return Ok(request);
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }

        [HttpGet("by_user_id/{id}")]
        public async Task<IActionResult> GetRequestsByUserId([FromRoute]string id)
        {
            try
            {
                List<Request> requests = await _requestsRepository.GetAll(typeof(Request));
                var result = from r in requests where r.UserId == id select r;               
                if (result == null)
                {
                    return NotFound();
                }
                return Ok(result);
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }

        [HttpGet("by_team_id/{id}")]
        public async Task<IActionResult> GetRequestsByTeamId([FromRoute]string id)
        {
            try
            {
                List<Request> requests = await _requestsRepository.GetAll(typeof(Request));
                var result = (from r in requests where r.Team.Id == id select r).ToList();
                return Ok(requests);
            }
            catch (Exception e)
            {
                return StatusCode(500,e);
            }
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] Request request)
        {
            try
            {
                request.Id = Guid.NewGuid().ToString();
                var result = await _requestsRepository.Create(request);
                if (result == null) return BadRequest(request);
                return Created($"/api/Player/{request.Id}", result);
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }

        [HttpPost("Upsert")]
        public async Task<IActionResult> Upsert([FromBody] Request request)
        {
            try
            {

                var result = await _requestsRepository.Upsert(request);
                if (result == null) return BadRequest(request);
                return Created($"/api/Player/{request.Id}", result);
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }

        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromBody] Request request)
        {
            try
            {

                var result = await _requestsRepository.Update(request);
                if (result == null) return BadRequest(request);
                return Ok(result);
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                await _requestsRepository.Delete(id);
                return NoContent();
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }
    }
}