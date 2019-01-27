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
    public class StaffController : ControllerBase
    {


        private readonly IRepository<Staff> _staffRepository;
        private readonly IRepository<Team> _teamsRepository;
        private readonly IRepository<Request> _requestsRepository;

        public StaffController()
        {
            _staffRepository = new CouchbaseRepository<Staff>();
            _teamsRepository = new CouchbaseRepository<Team>();
            _requestsRepository = new CouchbaseRepository<Request>();
        }

        #region GET
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var staff = await _staffRepository.GetAll(typeof(Staff));
                return Ok(staff);
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetStaffById([FromRoute]string id)
        {
            try
            {
                var staff = await _staffRepository.Get(id);
                return Ok(staff);
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }

        [HttpGet("by_user_id/{id}")]
        public async Task<IActionResult> GetStaffByUserId([FromRoute]string id)
        {
            try
            {
                List<Staff> staff = await _staffRepository.GetAll(typeof(Staff));
                var s = staff.FirstOrDefault(p => p.User.Id == id);
                if (s == null)
                {
                    return NotFound();
                }
                return Ok(s);
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }

        [HttpGet("free_staffs/{id}")]
        public async Task<IActionResult> GetFreeStaffs([FromRoute]string id)
        {
            try
            {
                List<Staff> all = await _staffRepository.GetAll(typeof(Staff));
                List<Staff> freeStaffs = (from s in all
                                          where s.StaffStatus == StaffStatus.FreeWorker
                                          select s).ToList();

                Team team = await _teamsRepository.Get(id);
                if (team.SentRequests != null) {
                    foreach (var item in team.SentRequests)
                    {
                        Request req = await _requestsRepository.Get(item);
                        if (req.RequestTo == RequestTo.Staff &&
                            req.RequestStatus == RequestStatus.InProgress)
                        {
                            freeStaffs.RemoveAll(x => x.User.Id == req.UserId);
                        }
                    }
                }
                

                return Ok(freeStaffs);
            }
            catch (Exception e)
            {
                return StatusCode(500,e);
            }
        }

        #endregion

        #region POST

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] Staff staff)
        {
            try
            {
                staff.Id = Guid.NewGuid().ToString();
                var result = await _staffRepository.Create(staff);
                if (result == null) { return BadRequest(result); }
                return Created(string.Format("/api/Staff/{0}", staff.Id), result);
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }

        [HttpPost("Upsert")]
        public async Task<IActionResult> Upsert([FromBody] Staff staff)
        {
            try
            {
                var result = await _staffRepository.Upsert(staff);
                if (result == null) { return BadRequest(); }
                return Created(string.Format("/api/Staff/{0}", staff.Id), result);
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }

        #endregion

        #region PUT

        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromBody]Staff staff)
        {
            try
            {
                var result = await _staffRepository.Update(staff);
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
                await _staffRepository.Delete(id);
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