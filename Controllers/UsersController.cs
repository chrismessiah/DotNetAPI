using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DotNetAPI;
using DotNetAPI.Data;
using DotNetAPI.Models;

namespace DotNetAPI.Controllers
{
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        // *********** REQUIRED FOR DATABASE CALLS *********
        private readonly UserDbContext _context;
        public UsersController(UserDbContext context) { _context = context; }
        // *********** REQUIRED FOR DATABASE CALLS *********

        // GET api/Users
        [HttpGet]
        public IEnumerable<User> Get()
        {
            return _context.Users.ToList();
        }

        // GET api/Users/5
        [HttpGet("{id}")]
        public User Get(int id)
        {
            return _context.Users.First(t => t.Id == id);
        }

        // POST api/Users
        /*
        * Using JObject ensures that however the data is posted, we are able to
        * serialize it to the related class. JObject is basically a JSON Object
        * data type in C#. Can be assigned a specific data type with the
        * ToObject method which requires the datatype to serialize to.
        */
        [HttpPost]
        public void Post([FromForm] User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges(); // EF requires you to commit your changes by default
        }

        // PUT api/Users/5
        [HttpPut("{id}")]
        public void Put(int id, [FromForm] User user)
        {
            user.Id = id; // Ensure an id is attached
            _context.Users.Update(user);
            _context.SaveChanges();
        }

        // DELETE api/Users/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
                // Check if element exists
            if ( _context.Users.Where(t => t.Id == id).Count() > 0 ) {
                _context.Users.Remove(_context.Users.First(t => t.Id == id));
                _context.SaveChanges();
            }
        }
    }
}
