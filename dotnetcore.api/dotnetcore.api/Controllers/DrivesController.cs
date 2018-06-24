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
    [Route("api/Drives")]
    public class DrivesController : Controller
    {
        private readonly casn_appContext _context;

        public DrivesController(casn_appContext context)
        {
            _context = context;
        }

        // GET: api/Drives
        [HttpGet]
        public IEnumerable<Drive> GetDrive()
        {
            return _context.Drive;
        }

        // GET: api/Drives/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDrive([FromRoute] uint id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var drive = await _context.Drive.SingleOrDefaultAsync(m => m.Id == id);

            if (drive == null)
            {
                return NotFound();
            }

            return Ok(drive);
        }

        // PUT: api/Drives/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDrive([FromRoute] uint id, [FromBody] Drive drive)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != drive.Id)
            {
                return BadRequest();
            }

            _context.Entry(drive).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DriveExists(id))
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

        // POST: api/Drives
        [HttpPost]
        public async Task<IActionResult> PostDrive([FromBody] Drive drive)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Drive.Add(drive);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDrive", new { id = drive.Id }, drive);
        }

        // DELETE: api/Drives/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDrive([FromRoute] uint id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var drive = await _context.Drive.SingleOrDefaultAsync(m => m.Id == id);
            if (drive == null)
            {
                return NotFound();
            }

            _context.Drive.Remove(drive);
            await _context.SaveChangesAsync();

            return Ok(drive);
        }

        private bool DriveExists(uint id)
        {
            return _context.Drive.Any(e => e.Id == id);
        }
    }
}