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
  public class PitchController : ControllerBase
  {
    private readonly IRepository<Pitch> _pitchRepository;

    public PitchController()
    {
      _pitchRepository = new CouchbaseRepository<Pitch>();
    }

    #region GET
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
      try
      {
        var pitches = await _pitchRepository.GetAll(typeof(Pitch));
        return Ok(pitches);
      }
      catch (Exception e)
      {
        return StatusCode(500, e);
      }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetPitchById([FromRoute]string id)
    {
      try
      {
        var Pitch = await _pitchRepository.Get(id);
        if (Pitch == null) { return NotFound(); }
        return Ok(Pitch);
      }
      catch (Exception e)
      {
        return StatusCode(500, e);
      }
    }

    [HttpGet("by_user_id/{id}")]
    public async Task<IActionResult> GetPitchesByUserId([FromRoute]string id)
    {
      try
      {
        var pitches = await _pitchRepository.GetAll(typeof(Pitch));
        var pitchesById = (from t in pitches where t.User.Id == id select t).ToList();
        return Ok(pitchesById);
      }
      catch (Exception e)
      {
        return StatusCode(500, e);
      }
    }

    #endregion

    #region POST

    [HttpPost("Create")]
    public async Task<IActionResult> Create([FromBody]Pitch pitch)
    {
      try
      {
        pitch.Id = Guid.NewGuid().ToString();
        var result = await _pitchRepository.Create(pitch);
        if (result == null) { return BadRequest(pitch); }
        return Created($"/api/Pitch/{pitch.Id}", result);
      }
      catch (Exception e)
      {
        return StatusCode(500, e);
      }
    }

    [HttpPost("Upsert")]
    public async Task<IActionResult> Upsert([FromBody] Pitch pitch)
    {
      try
      {
        var result = await _pitchRepository.Upsert(pitch);
        if (result == null) return BadRequest(pitch);
        return Created($"/api/Pitch/{pitch.Id}", result);
      }
      catch (Exception e)
      {
        return StatusCode(500, e);
      }
    }

    #endregion

    #region PUT
    [HttpPut("Update")]
    public async Task<IActionResult> Update([FromBody] Pitch pitch)
    {
      try
      {
        string id = pitch.Id;
        var result = await _pitchRepository.Update(pitch);
        if (result == null) return BadRequest(pitch);
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
    public async Task<IActionResult> Delete(string id)
    {
      try
      {
        await _pitchRepository.Delete(id);
        return NoContent();
      }
      catch (Exception e)
      {
        return StatusCode(500, e);
      }
    }

    #endregion
  }
}