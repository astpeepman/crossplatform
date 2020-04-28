using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Lab2_1.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;


namespace Lab2_1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly appsContext _context;

        public UsersController(appsContext context)
        {
            _context = context;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Users>>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public IEnumerable<Users> GetUser()
        {
            return _context.Users;
        }

        [HttpGet("paid")]
        public IEnumerable<Users> GetPaidUsers()
        {
            return _context.getUserPaid(_context.Users);

        }

        [HttpGet("AppsOfUsers")]
        public Dictionary<string, List<string>> getAppsOfUs()
        {
            return _context.GetAppsOfUsers();
        }
        




        // PUT: api/Users/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsers(long id, Users users)
        {
            if (id != users.Id)
            {
                return BadRequest();
            }

            _context.Entry(users).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsersExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Users
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
       

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Users>> PostUsers(Users user)
        {
            user.SetPhone();
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUsers", new { id = user.Id }, user);
        }

        //[HttpPatch]
        //public IActionResult JsonPatchWithModelState(
        //[FromBody] JsonPatchDocument<Users> patchDoc)
        //{
        //    if (patchDoc != null)
        //    {
        //        var customer = CreateCustomer();

        //        patchDoc.ApplyTo(customer, ModelState);

        //        if (!ModelState.IsValid)
        //        {
        //            return BadRequest(ModelState);
        //        }

        //        return new ObjectResult(customer);
        //    }
        //    else
        //    {
        //        return BadRequest(ModelState);
        //    }
        //}

        [HttpPost("addApp/{userid}/{appId}")]
        public string AddAppForUser(long appId, long userid)
        {
            return _context.SetAppsIdForUser(appId, userid); ;
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Users>> DeleteUsers(long id)
        {
            var users = await _context.Users.FindAsync(id);
            if (users == null)
            {
                return NotFound();
            }

            _context.Users.Remove(users);
            await _context.SaveChangesAsync();

            return users;
        }

        private bool UsersExists(long id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}
