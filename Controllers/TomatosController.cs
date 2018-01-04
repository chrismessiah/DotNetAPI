using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace TomatoAPI.Controllers
{
    [Route("api/[controller]")]
    public class TomatosController : Controller
    {
        // GET api/Tomatos
        [HttpGet]
        public IEnumerable<Tomato> Get()
        {
            using (TomatoDb db = new TomatoDb())
            {
                return db.Tomatos.ToList();
            }
        }

        // GET api/Tomatos/5
        [HttpGet("{id}")]
        public Tomato Get(int id)
        {
            using (TomatoDb db = new TomatoDb())
            {
                return db.Tomatos.First(t => t.Id == id);
            }
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
            using (TomatoDb db = new TomatoDb())
            {
                db.Tomatos.Add(tomato);
                db.SaveChanges(); // EF requires you to commit your changes by default
            }
        }

        // PUT api/Tomatos/5
        [HttpPut("{id}")]
        public void Put(int id, [FromForm] Tomato tomato)
        {
            tomato.Id = id; // Ensure an id is attached
            using (TomatoDb db = new TomatoDb())
            {
                db.Tomatos.Update(tomato);
                db.SaveChanges();
            }
        }

        // DELETE api/Tomatos/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            using (TomatoDb db = new TomatoDb())
            {
                // Check if element exists
                if ( db.Tomatos.Where(t => t.Id == id).Count() > 0 ) {
                    db.Tomatos.Remove(db.Tomatos.First(t => t.Id == id));
                    db.SaveChanges();
                }

            }
        }
    }
}
