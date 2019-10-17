using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using owp_web.Data;
using owp_web.Models;

namespace owp_web.Controllers
{
    [AllowAnonymous]
    public class CitizenItemsController : Controller
    {
        private readonly IOwpContext _context;

        public CitizenItemsController(IOwpContext context)
        {
            _context = context;
        }

        // GET: CitizenItems
        public async Task<IActionResult> Index()
        {
            return View(await _context.WorkItem.ToListAsync());
        }

        // GET: CitizenItems/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workItem = await _context.WorkItem
                .FirstOrDefaultAsync(m => m.WorkItemId == id);
            if (workItem == null)
            {
                return NotFound();
            }

            return View(workItem);
        }

        // GET: CitizenItems/Create
        public IActionResult Create()
        {
            return View(new WorkItemViewModel());
        }

        // POST: CitizenItems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("WorkItemId,Description,WorkItemType,CreatedOn,LastChangedOn,Status,Address,Latitude,Longitude")] WorkItem workItem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(workItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Edit), new { Id = workItem.WorkItemId });
            }
            return View(new WorkItemViewModel(workItem));
        }

        // GET: CitizenItems/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workItem = await _context.WorkItem.FindAsync(id);
            if (workItem == null)
            {
                return NotFound();
            }

            return View(new WorkItemViewModel(workItem));
        }

        // POST: CitizenItems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("WorkItemId,Description,WorkItemType,CreatedOn,LastChangedOn,Status,Address")] WorkItem workItem)
        {
            if (id != workItem.WorkItemId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(workItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WorkItemExists(workItem.WorkItemId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(new WorkItemViewModel(workItem));
        }

        // GET: CitizenItems/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workItem = await _context.WorkItem
                .FirstOrDefaultAsync(m => m.WorkItemId == id);
            if (workItem == null)
            {
                return NotFound();
            }

            return View(workItem);
        }


        // POST: CitizenItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var workItem = await _context.WorkItem.FindAsync(id);
            _context.WorkItem.Remove(workItem);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: CitizenItems/Search?searchText=22
        public async Task<IActionResult> Search(string searchText)
        {
            ViewData["currentSearch"] = searchText;

            if(!string.IsNullOrEmpty(searchText))
            {
                var workItem = await _context.WorkItem.FirstOrDefaultAsync(w => searchText.Equals(w.WorkItemId.ToString()));
                return View(workItem);
            }

            return View(null);
        }

        private bool WorkItemExists(long id)
        {
            return _context.WorkItem.Any(e => e.WorkItemId == id);
        }
    }
}
