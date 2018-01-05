using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TomatoAPI;
using TomatoAPI.Data;
using TomatoAPI.Models;

namespace TomatoAPI.Controllers
{
    [Route("api/[controller]")]
    public class TomatosController : Controller
    {
        // *********** REQUIRED FOR DATABASE CALLS *********
        private readonly TomatoDbContext _context;
        public TomatosController(TomatoDbContext context) { _context = context; }
        // *********** REQUIRED FOR DATABASE CALLS *********

        // GET api/Tomatos
        [HttpGet]
        public IEnumerable<Tomato> Get()
        {
            return _context.Tomatos.ToList();
        }

        // GET api/Tomatos/5
        [HttpGet("{id}")]
        public Tomato Get(int id)
        {
            return _context.Tomatos.First(t => t.Id == id);
        }

        // POST api/Tomatos
        /*
        * Using JObject ensures that however the data is posted, we are able to
        * serialize it to the related class. JObject is basically a JSON Object
        * data type in C#. Can be assigned a specific data type with the
        * ToObject method which requires the datatype to serialize to.
        */
        [HttpPost]
        public void Post([FromForm] Tomato tomato)
        {
            _context.Tomatos.Add(tomato);
            _context.SaveChanges(); // EF requires you to commit your changes by default
        }

        // PUT api/Tomatos/5
        [HttpPut("{id}")]
        public void Put(int id, [FromForm] Tomato tomato)
        {
            tomato.Id = id; // Ensure an id is attached
            _context.Tomatos.Update(tomato);
            _context.SaveChanges();
        }

        // DELETE api/Tomatos/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
                // Check if element exists
            if ( _context.Tomatos.Where(t => t.Id == id).Count() > 0 ) {
                _context.Tomatos.Remove(_context.Tomatos.First(t => t.Id == id));
                _context.SaveChanges();
            }
        }
    }
}
