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
    public class HousesController : ControllerBase
    {
        private readonly HousesService _serv;

        public HousesController(HousesService serv)
        {
            _serv = serv;
        }

        [HttpGet]
        public ActionResult<List<House>> GetAll()
        {
            try
            {
                List<House> found = _serv.GetAll();
                return Ok(found);
                
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{id}")]
        public ActionResult<House> GetById(int id)
        {
            try
            {
                House found = _serv.GetById(id);
                return Ok(found);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<House>> Create([FromBody] House data)
        {
            try
            {
                Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
                data.CreatorId = userInfo.Id;
                House created = _serv.Create(data);
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
        public async Task<ActionResult<House>> Edit(int id, [FromBody] House update)
        {
            try
            {
                Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
                update.CreatorId = userInfo.Id;
                update.Id = id;
                House updated = _serv.Edit(update);
                return Ok(updated);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult<House>> Remove(int id)
        {
            try
            {
                Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
                House removed = _serv.Remove(id, userInfo.Id);
                return Ok(removed);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}