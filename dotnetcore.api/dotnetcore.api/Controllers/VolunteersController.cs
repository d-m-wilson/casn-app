using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using dotnetcore.api.Entities;

namespace dotnetcore.api.Controllers
{
    [Produces("application/json")]
    [Route("api/Volunteers")]
    public class VolunteersController : Controller
    {
        private readonly casn_appContext _context;

        public VolunteersController(casn_appContext context)
        {
            _context = context;
        }

        // GET: api/Volunteers
        [HttpGet]
        public IEnumerable<Volunteer> GetVolunteer()
        {
            return _context.Volunteer;
        }

        // GET: api/Volunteers/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetVolunteer([FromRoute] uint id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var volunteer = await _context.Volunteer.SingleOrDefaultAsync(m => m.Id == id);

            if (volunteer == null)
            {
                return NotFound();
            }

            return Ok(volunteer);
        }

        // PUT: api/Volunteers/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVolunteer([FromRoute] uint id, [FromBody] Volunteer volunteer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != volunteer.Id)
            {
                return BadRequest();
            }

            _context.Entry(volunteer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VolunteerExists(id))
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

        // POST: api/Volunteers
        [HttpPost]
        public async Task<IActionResult> PostVolunteer([FromBody] Volunteer volunteer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Volunteer.Add(volunteer);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetVolunteer", new { id = volunteer.Id }, volunteer);
        }

        // DELETE: api/Volunteers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVolunteer([FromRoute] uint id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var volunteer = await _context.Volunteer.SingleOrDefaultAsync(m => m.Id == id);
            if (volunteer == null)
            {
                return NotFound();
            }

            _context.Volunteer.Remove(volunteer);
            await _context.SaveChangesAsync();

            return Ok(volunteer);
        }

        private bool VolunteerExists(uint id)
        {
            return _context.Volunteer.Any(e => e.Id == id);
        }
    }
}