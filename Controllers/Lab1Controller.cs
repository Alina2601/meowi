using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using to.Models;
using Alina.Storage;
using Serilog;
namespace to.Controllers
{
   [Route("api/[controller]")]
   [ApiController]
   public class LabController : ControllerBase
    {
        private IStorage<lab1Data> _memCache;

        public LabController(IStorage<lab1Data> memCache)
        {
            _memCache = memCache;
        }

        [HttpGet]
        public ActionResult<IEnumerable<lab1Data>> Get()
        {
            return Ok(_memCache.All);
        }

        [HttpGet("{id}")]
        public ActionResult<lab1Data> Get(Guid id)
        {
            if (!_memCache.Has(id)){
                 
            Log.Error("Product not found");
             return NotFound("No such");}

            return Ok(_memCache[id]);
        }

        [HttpPost]
        public IActionResult Post([FromBody] lab1Data value)
        {
            var validationResult = value.Validate();

            if (!validationResult.IsValid) return BadRequest(validationResult.Errors);

            _memCache.Add(value);

            return Ok($"{value.ToString()} has been added");
        }

        [HttpPut("{id}")]
        public IActionResult Put(Guid id, [FromBody] lab1Data value)
        {
            if (!_memCache.Has(id)) return NotFound("No such");

            var validationResult = value.Validate();

            if (!validationResult.IsValid) return BadRequest(validationResult.Errors);

            var previousValue = _memCache[id];
            _memCache[id] = value;

            return Ok($"{previousValue.ToString()} has been updated to {value.ToString()}");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            if (!_memCache.Has(id)) return NotFound("No such");

            var valueToRemove = _memCache[id];
            _memCache.RemoveAt(id);

            return Ok($"{valueToRemove.ToString()} has been removed");
        }
    }
}