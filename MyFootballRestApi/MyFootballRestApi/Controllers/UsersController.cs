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
        private readonly IRepository<Player> _playerRepository;
        private readonly IRepository<Coach> _coachRepository;
        private readonly IRepository<Referee> _refereeRepository;
        private readonly IRepository<Staff> _staffRepository;

        public UsersController()
        {
            _usersRepository = new CouchbaseRepository<User>();
            _playerRepository = new CouchbaseRepository<Player>();
            _coachRepository = new CouchbaseRepository<Coach>();
            _refereeRepository = new CouchbaseRepository<Referee>();
            _staffRepository = new CouchbaseRepository<Staff>();
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
                var result = await _usersRepository.Create(user);
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
                var result = await _usersRepository.Upsert(user);
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
                var result = await _usersRepository.Update(user);
                if (result == null) return BadRequest(user);

                List<Player> players = await _playerRepository.GetAll(typeof(Player));
                var player = players.FirstOrDefault(p => p.User.Id == user.Id);
                if (player != null) {
                    player.User = user;
                }
                await _playerRepository.Update(player);

                List<Coach> coaches = await _coachRepository.GetAll(typeof(Coach));
                var coach = coaches.FirstOrDefault(p => p.User.Id == user.Id);
                if (coach != null)
                {
                    coach.User = user;
                }
                await _coachRepository.Update(coach);

                List<Referee> referees = await _refereeRepository.GetAll(typeof(Referee));
                var referee = referees.FirstOrDefault(p => p.User.Id == user.Id);
                if (referee != null)
                {
                    referee.User = user;
                }
                await _refereeRepository.Update(referee);

                List<Staff> staff = await _staffRepository.GetAll(typeof(Staff));
                var st = staff.FirstOrDefault(p => p.User.Id == user.Id);
                if (st != null)
                {
                    st.User = user;
                }
                await _staffRepository.Update(st);

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