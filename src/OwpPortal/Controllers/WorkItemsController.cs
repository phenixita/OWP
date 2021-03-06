using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using owp_web.Models;
using owp_web.Helpers;
using Microsoft.Extensions.Options;
using Microsoft.Graph;

namespace owp_web.Controllers
{
    [Authorize(Roles = "DeliveryLeader")]
    public class WorkItemsController : Controller
    {
        private readonly OwpContext _context;
        private GraphAPI _api = new GraphAPI();

        public WorkItemsController(OwpContext context)
        {
            _context = context;
        }

        // GET: WorkItems
        public async Task<IActionResult> Index()
        {
            return View(await _context.WorkItemList.Where(wi=>wi.Status!=WorkItemStatus.Done).ToListAsync());
        }

        // GET: WorkItems/Details/5
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

        // GET: WorkItems/Create
        public IActionResult Create()
        {
            return View(new WorkItemViewModel ());
        }

        // POST: WorkItems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("WorkItemId,Description,WorkItemType,CreatedOn,LastChangedOn,Status,Address,WorkItemPriority")] WorkItem workItem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(workItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(workItem);
        }

        // GET: WorkItems/Edit/5
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

            var viewModel = new WorkItemViewModel (workItem);
            return View(viewModel);
        }

        // POST: WorkItems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("WorkItemId,Description,WorkItemType,CreatedOn,LastChangedOn,Status,Address,AssignmentId,Latitude,Longitude,WorkItemPriority,ImageUrl,Email")] WorkItem workItem)
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

                    var changedEntity = _context.ChangeTracker
                       .Entries()
                       .Where(x => x.State == EntityState.Modified)
                       .FirstOrDefault();

                    WorkItem originalWorkItem = (WorkItem)changedEntity.GetDatabaseValues().ToObject();

                    var assignmentProperty = changedEntity
                       .Properties
                       .Where(x => x.Metadata.Name == "AssignmentId")
                       .FirstOrDefault();

                    if (assignmentProperty.IsModified)
                    {
                        Message message = new Message();

                        if (string.IsNullOrEmpty(originalWorkItem.AssignmentId))
                        {
                            message.Body = new ItemBody
                            {
                                Content = $"Hi {workItem.AssignedTo.PrincipalDisplayName}! \r\n\r\n<br><br>The Issue <a href=\"https://owp.azurewebsites.net/worker/edit/{workItem.WorkItemId}\">#{workItem.WorkItemId}</a> has been assigned to you.",
                                ContentType = BodyType.Html
                            };
                            message.Subject = $"New Issue #{workItem.WorkItemId} has been assigned to you!";

                            workItem.Status = WorkItemStatus.Assigned;
                        }
                        else if(originalWorkItem.AssignmentId != workItem.AssignmentId)
                        {
                            message.Body = new ItemBody
                            {
                                Content = $"Hi {workItem.AssignedTo.PrincipalDisplayName}! \r\n\r\n<br><br>The Issue <a href=\"https://owp.azurewebsites.net/worker/edit/{workItem.WorkItemId}\">#{workItem.WorkItemId}</a> has been REassigned to you.",
                                ContentType = BodyType.Html
                            };
                            message.Subject = $"New Issue #{workItem.WorkItemId} has been REassigned to you!";

                            workItem.Status = WorkItemStatus.Resolved;
                        }

                        if (message != null && message.Body != null && !string.IsNullOrEmpty(message.Body.ToString()) && message.Subject != null && !string.IsNullOrEmpty(message.Subject))
                        {
                            await _api.SendEmailByPrincipalIdAsync(workItem.AssignedTo.PrincipalId, message);
                        }

                        if (originalWorkItem.Status != workItem.Status)
                        {
                            if(workItem.Status == WorkItemStatus.Done && !string.IsNullOrEmpty(workItem.Email))
                            {
                                message = new Message
                                { 
                                    Body = new ItemBody
                                    {
                                        Content = $"{workItem.WorkItemId} solved!",
                                        ContentType = BodyType.Text
                                    },
                                    Subject = $"{workItem.WorkItemId} solved!"
                                };

                                EmailAddress emailAddress = new EmailAddress { Address = workItem.Email };

                                await _api.SendEmailByEmailAsync(emailAddress, message);

                                workItem.Email = null;
                            }

                            _context.Update(workItem);
                        }
                    }

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

        // GET: WorkItems/Delete/5
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

        // POST: WorkItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var workItem = await _context.WorkItem.FindAsync(id);
            _context.WorkItem.Remove(workItem);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WorkItemExists(long id)
        {
            return _context.WorkItem.Any(e => e.WorkItemId == id);
        }
    }
}
