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
    [Route("api/VolunteerDrives")]
    public class VolunteerDrivesController : Controller
    {
        private readonly casn_appContext _context;

        public VolunteerDrivesController(casn_appContext context)
        {
            _context = context;
        }

        // GET: api/VolunteerDrives
        [HttpGet]
        public IEnumerable<VolunteerDrive> GetVolunteerDrive()
        {
            return _context.VolunteerDrive;
        }

        // GET: api/VolunteerDrives/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetVolunteerDrive([FromRoute] uint id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var volunteerDrive = await _context.VolunteerDrive.SingleOrDefaultAsync(m => m.Id == id);

            if (volunteerDrive == null)
            {
                return NotFound();
            }

            return Ok(volunteerDrive);
        }

        // PUT: api/VolunteerDrives/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVolunteerDrive([FromRoute] uint id, [FromBody] VolunteerDrive volunteerDrive)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != volunteerDrive.Id)
            {
                return BadRequest();
            }

            _context.Entry(volunteerDrive).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VolunteerDriveExists(id))
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

        // POST: api/VolunteerDrives
        [HttpPost]
        public async Task<IActionResult> PostVolunteerDrive([FromBody] VolunteerDrive volunteerDrive)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.VolunteerDrive.Add(volunteerDrive);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetVolunteerDrive", new { id = volunteerDrive.Id }, volunteerDrive);
        }

        // DELETE: api/VolunteerDrives/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVolunteerDrive([FromRoute] uint id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var volunteerDrive = await _context.VolunteerDrive.SingleOrDefaultAsync(m => m.Id == id);
            if (volunteerDrive == null)
            {
                return NotFound();
            }

            _context.VolunteerDrive.Remove(volunteerDrive);
            await _context.SaveChangesAsync();

            return Ok(volunteerDrive);
        }

        private bool VolunteerDriveExists(uint id)
        {
            return _context.VolunteerDrive.Any(e => e.Id == id);
        }
    }
}