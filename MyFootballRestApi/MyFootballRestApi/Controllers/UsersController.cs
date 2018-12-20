using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyFootballRestApi.Data;
using MyFootballRestApi.Models;
using Newtonsoft.Json;

namespace MyFootballRestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {

        private readonly IRepository<User> _usersRepository;

        public UsersController()
        {
            _usersRepository = new CouchbaseRepository<User>();
        }

        #region GET
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var users = await _usersRepository.GetAll(typeof(User));
                return Ok(users);
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTeamById([FromRoute]string id)
        {
            try
            {
                var user = await _usersRepository.Get(id);
                if (user == null) { return NotFound(); }
                return Ok(user);
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }
        #endregion

        #region POST

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody]User user)
        {
            try
            {
                string id = user.Id;
                var result = await _usersRepository.Create(id, user);
                if (result == null) { return BadRequest(user); }
                return Created($"/api/Users/{id}", result);
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }

        [HttpPost("Upsert")]
        public async Task<IActionResult> Upsert([FromBody] User user)
        {
            try
            {
                string id = user.Id;
                var result = await _usersRepository.Upsert(id, user);
                if (result == null) return BadRequest(user);
                return Created($"/api/users/{id}", result);
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }

        #endregion

        #region PUT
        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromBody] User user)
        {
            try
            {
                string id = user.Id.ToString();
                var result = await _usersRepository.Update(id, user);
                if (result == null) return BadRequest(user);
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
                await _usersRepository.Delete(id);
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