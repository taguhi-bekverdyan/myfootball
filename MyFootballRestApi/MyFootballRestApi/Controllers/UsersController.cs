using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyFootballRestApi.Models;
using Newtonsoft.Json;

namespace MyFootballRestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {

        public List<User> Users { get;private set; }

        public UsersController()
        {
            Users = GetUsers().Result;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> FindAll()
        {
            try
            {
                return Ok(await GetUsers());
            }
            catch (Exception e)
            {
                return StatusCode(500,e);
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Insert([FromBody] User user)
        {
            try
            {
                Users.Add(user);
                await InsertUsers();
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(500,e);
            }
        }

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> Update([FromBody]User user)
        {
            try
            {
                Users = await GetUsers();
                User us = Users.FirstOrDefault(u => u.Id == user.Id);
                if (us != null)
                {
                    Users.Remove(us);
                    Users.Add(user);
                    await InsertUsers();
                }
                return NotFound();
            }
            catch (Exception e)
            {
                return StatusCode(500,e);
            }
        }

        [HttpGet("{id}")]
        [Authorize]
        public Task<IActionResult> FindUserById([FromRoute] string id)
        {
            return Task<IActionResult>.Factory.StartNew(()=> {
                try
                {
                    User user = Users.FirstOrDefault(u => u.Id == id);
                    if (user != null) { return Ok(user); }
                    return NotFound();
                }
                catch (Exception e)
                {
                    return StatusCode(500, e);
                }
            });          
        }

        private Task<List<User>> GetUsers()
        {
            return Task<List<User>>.Factory.StartNew(()=> {
                string userJson;
                try
                {
                    using (StreamReader sr = new StreamReader(@"JsonData/Users.json"))
                    {
                        userJson = sr.ReadToEnd();
                    }
                }
                catch (Exception)
                {

                    throw;
                }
                return JsonConvert.DeserializeObject<List<User>>(userJson);
            });            
        }

        private async Task InsertUsers()
        {
            string usersJson = JsonConvert.SerializeObject(Users);
            using (StreamWriter sw = new StreamWriter(@"JsonData/Users.json"))
            {
                await sw.WriteAsync(usersJson);
            }
        }

    }
}