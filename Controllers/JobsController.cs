using Microsoft.AspNetCore.Mvc;
using w10d3.Services;
using w10d3.Models;
using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using CodeWorks.Auth0Provider;

namespace w10d3.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class JobsController : ControllerBase
    {
        private readonly JobsService _serv;

        public JobsController(JobsService serv)
        {
            _serv = serv;
        }

        [HttpGet]
        public ActionResult<List<Job>> GetAll()
        {
            try
            {
                List<Job> found = _serv.GetAll();
                return Ok(found);
                
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{id}")]
        public ActionResult<Job> GetById(int id)
        {
            try
            {
                Job found = _serv.GetById(id);
                return Ok(found);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Job>> Create([FromBody] Job data)
        {
            try
            {
                Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
                data.CreatorId = userInfo.Id;
                Job created = _serv.Create(data);
                created.Creator = userInfo;
                return Ok(created);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<ActionResult<Job>> Edit(int id, [FromBody] Job update)
        {
            try
            {
                Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
                update.CreatorId = userInfo.Id;
                update.Id = id;
                Job updated = _serv.Edit(update);
                return Ok(updated);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult<Job>> Remove(int id)
        {
            try
            {
                Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
                Job removed = _serv.Remove(id, userInfo.Id);
                return Ok(removed);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}