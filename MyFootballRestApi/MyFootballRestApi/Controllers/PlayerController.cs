using System.Collections.Generic;
using System.Net;
using System.Net.Http;
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
    public IActionResult Get()
    {
      var players = _playerRepository.GetAll(typeof(Player));
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
      var result = _playerRepository.Create( player);
      if (result == null) return BadRequest(player);
      return Created($"/api/Player/Get/{player.Id.ToString()}", result);
    }

    [HttpPost("Upsert")]
    public IActionResult Upsert([FromBody] Player player)
    {
      var result = _playerRepository.Upsert( player);
      if (result == null) return BadRequest(player);
      return Created($"/api/Player/Get/{player.Id.ToString()}", result);
    }

    [HttpPut("Update")]
    public IActionResult Update([FromBody] Player player)
    {
      var result = _playerRepository.Update( player);
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
