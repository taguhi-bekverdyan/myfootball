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
      var players = _playerRepository.GetAll();
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
    public IActionResult Create([FromBody] Player player, string id)
    {
      var result = _playerRepository.Create(id, player);
      if (result == null) return BadRequest(player);
      return Created($"/api/Player/Get/{id}", result);
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
