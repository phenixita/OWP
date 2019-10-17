using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using owp_web.Models;

namespace owp_web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GeoController : ControllerBase
    {
        private readonly OwpContext _context;

        public GeoController(OwpContext context)
        {
            _context = context;
        }

        // GET: api/Geo
        [HttpGet]
        public async Task<ActionResult<IEnumerable<WorkItem>>> GetWorkItem()
        {
            return await _context.WorkItem.ToListAsync();
        }

        // GET: api/Geo/5
        [HttpGet("{id}")]
        public async Task<ActionResult<WorkItem>> GetWorkItem(long id)
        {
            var workItem = await _context.WorkItem.FindAsync(id);

            if (workItem == null)
            {
                return NotFound();
            }

            return workItem;
        }

        // PUT: api/Geo/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWorkItem(long id, WorkItem workItem)
        {
            if (id != workItem.WorkItemId)
            {
                return BadRequest();
            }

            _context.Entry(workItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WorkItemExists(id))
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

        // POST: api/Geo
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<WorkItem>> PostWorkItem(WorkItem workItem)
        {
            _context.WorkItem.Add(workItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetWorkItem", new { id = workItem.WorkItemId }, workItem);
        }

        // DELETE: api/Geo/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<WorkItem>> DeleteWorkItem(long id)
        {
            var workItem = await _context.WorkItem.FindAsync(id);
            if (workItem == null)
            {
                return NotFound();
            }

            _context.WorkItem.Remove(workItem);
            await _context.SaveChangesAsync();

            return workItem;
        }

        private bool WorkItemExists(long id)
        {
            return _context.WorkItem.Any(e => e.WorkItemId == id);
        }
    }
}
