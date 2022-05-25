using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using w10d3.Models;
using w10d3.Services;

namespace w10d2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HousesController : ControllerBase
    {
        HousesService _serv;

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
        public ActionResult<House> Create([FromBody] House data)
        {
            try
            {
                House created = _serv.Create(data);
                return Ok(created);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("{id}")]
        public ActionResult<House> Edit(int id, [FromBody] House update)
        {
            try
            {
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
        public ActionResult<House> Remove(int id)
        {
            try
            {
                House removed = _serv.Remove(id);
                return Ok(removed);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}