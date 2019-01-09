using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyFootballRestApi.Data;
using MyFootballRestApi.Models;
using System;
using System.Linq;

namespace MyFootballRestApi.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class PlayersController : ControllerBase
    {
        private readonly IRepository<Player> _playerRepository = new CouchbaseRepository<Player>();

        // GET: api/Player
        [HttpGet]
        public async Task<IActionResult> Get()
        {          
            try
            {
                var players = await _playerRepository.GetAll(typeof(Player));
                return Ok(players);
            }
            catch (Exception e)
            {
                return StatusCode(500,e);
            }
        }

        // GET: api/Player/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            try
            {
                var player = await _playerRepository.Get(id);
                return Ok(player);
            }
            catch (Exception e)
            {
                return StatusCode(500,e);
            }
        }

        [HttpGet("by_user_id/{id}")]
        public async Task<IActionResult> GetPlayerByUserId([FromRoute]string id)
        {
            try
            {
                List<Player> players = await _playerRepository.GetAll(typeof(Player));
                var player = players.FirstOrDefault(p => p.User.Id == id);
                if (player == null)
                {
                    return NotFound();
                }
                return Ok(player);
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] Player player)
        {
            try
            {
                var result = await _playerRepository.Create(player);
                if (result == null) return BadRequest(player);
                return Created($"/api/Player/{player.Id}", result);
            }
            catch (Exception e)
            {
                return StatusCode(500,e);
            }
        }

        [HttpPost("Upsert")]
        public async Task<IActionResult> Upsert([FromBody] Player player)
        {
            try
            {
                
                var result = await _playerRepository.Upsert(player);
                if (result == null) return BadRequest(player);
                return Created($"/api/Player/{player.Id}", result);
            }
            catch (Exception e)
            {
                return StatusCode(500,e);
            }
        }

        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromBody] Player player)
        {
            try
            {
                
                var result = await _playerRepository.Update(player);
                if (result == null) return BadRequest(player);
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
                await _playerRepository.Delete(id);
                return NoContent();
            }
            catch (Exception e)
            {
                return StatusCode(500,e);
            }
        }
    }

}
