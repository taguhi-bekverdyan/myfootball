using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyFootballRestApi.Data;
using MyFootballRestApi.Models;

namespace MyFootballRestApi.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class PlayerController : ControllerBase
  {
    private readonly IRepository<Player> _playerRepository = new CouchbaseRepository<Player>();

    // GET: api/Player
    [HttpGet]
    public async Task<IActionResult> Get()
    {
      var players = await _playerRepository.GetAll(typeof(Player));
      return Ok(players);
    }

    // GET: api/Player/5
    [HttpGet("Get/{id}")]
    public IActionResult Get(string id)
    {
      var player = _playerRepository.Get(id);
      return Ok(player);
    }

    [HttpPost("Create")]
    public IActionResult Create([FromBody] Player player)
    {
      var result = _playerRepository.Create(player.Id.ToString(), player);
      if (result == null) return BadRequest(player);
      return Created($"/api/Player/Get/{player.Id.ToString()}", result);
    }

    [HttpPost("Upsert")]
    public IActionResult Upsert([FromBody] Player player, string id)
    {
      var result = _playerRepository.Upsert(id, player);
      if (result == null) return BadRequest(player);
      return Created($"/api/Player/Get/{id}", result);
    }

    [HttpPut("Update")]
    public IActionResult Update([FromBody] Player player, string id)
    {
      var result = _playerRepository.Update(id, player);
      if (result == null) return BadRequest(player);
      return Ok(result);
    }

    [HttpDelete("Delete/{id}")]
    public IActionResult Delete(string id)
    {
      _playerRepository.Delete(id);
      return NoContent();
    }
  }
}
