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
    public class CarsController : ControllerBase
    {
        private readonly CarsService _serv;

        public CarsController(CarsService serv)
        {
            _serv = serv;
        }

        [HttpGet]
        public ActionResult<List<Car>> GetAll()
        {
            try
            {
                List<Car> found = _serv.GetAll();
                return Ok(found);
                
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{id}")]
        public ActionResult<Car> GetById(int id)
        {
            try
            {
                Car found = _serv.GetById(id);
                return Ok(found);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Car>> Create([FromBody] Car data)
        {
            try
            {
                Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
                data.CreatorId = userInfo.Id;
                Car created = _serv.Create(data);
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
        public async Task<ActionResult<Car>> Edit(int id, [FromBody] Car update)
        {
            try
            {
                Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
                update.CreatorId = userInfo.Id;
                update.Id = id;
                Car updated = _serv.Edit(update);
                return Ok(updated);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult<Car>> Remove(int id)
        {
            try
            {
                Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
                Car removed = _serv.Remove(id, userInfo.Id);
                return Ok(removed);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}