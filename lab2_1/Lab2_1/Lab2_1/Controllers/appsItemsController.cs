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


namespace Lab2_1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class appsItemsController : ControllerBase
    {
        private readonly appsContext _context;

        public appsItemsController(appsContext context)
        {
            _context = context;
        }

        // GET: api/appsItems
        [HttpGet]
        [Authorize]
        public IEnumerable<appsItem> GetappsItems()
        {
            return _context.getapps(_context.appsItems);
        }

        //GET: api/appsItems/secret
        [HttpGet("secret")]
        [Authorize(Roles ="admin")]
        public async Task<ActionResult<IEnumerable<appsItem>>> GetAllItems()
        {
            return await _context.appsItems.ToListAsync();
        }

        // GET: api/appsItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<appsItem>> GetappsItem(long id)
        {
            var appsItem = await _context.appsItems.FindAsync(id);

            if (appsItem == null)
            {
                return NotFound();
            }

            return appsItem;
        }

        // PUT: api/appsItems/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutappsItem(long id, appsItem appsItem)
        {
            if (id != appsItem.Id)
            {
                return BadRequest();
            }

            _context.Entry(appsItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!appsItemExists(id))
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



        // POST: api/appsItems
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
       
        public async Task<ActionResult<appsItem>> PostappsItem(appsItem appsItem)
        {
            appsItem.secret = false;
            _context.appsItems.Add(appsItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetappsItem", new { id = appsItem.Id }, appsItem);
        }

        [HttpPost("secret")]
        [Authorize(Roles ="admin")]
        public async Task<ActionResult<appsItem>> PostSecretapps(appsItem appsItem)
        {
            appsItem.secret = true;
            _context.appsItems.Add(appsItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetappsItem", new { id = appsItem.Id }, appsItem);
        }

        // DELETE: api/appsItems/5
        [HttpDelete("{id}")]
        [Authorize(Roles ="admin")]
        public async Task<ActionResult<appsItem>> DeleteappsItem(long id)
        {
            var appsItem = await _context.appsItems.FindAsync(id);
            if (appsItem == null)
            {
                return NotFound();
            }

            _context.appsItems.Remove(appsItem);
            await _context.SaveChangesAsync();

            return appsItem;
        }

        private bool appsItemExists(long id)
        {
            return _context.appsItems.Any(e => e.Id == id);
        }

        
    }
}
