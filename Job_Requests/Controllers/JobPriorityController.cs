using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Job_Requests.DataAccess.Data;
using Job_Requests.Models;

namespace Job_Requests.Controllers
{
    public class JobPriorityController : Controller
    {
        private readonly ApplicationDbContext _context;

        public JobPriorityController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: JobPriority
        public async Task<IActionResult> Index()
        {
            return View(await _context.JobPriority.ToListAsync());
        }

        // GET: JobPriority/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jobPriority = await _context.JobPriority
                .FirstOrDefaultAsync(m => m.JobPriorityId == id);
            if (jobPriority == null)
            {
                return NotFound();
            }

            return View(jobPriority);
        }

        // GET: JobPriority/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: JobPriority/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("JobPriorityId,PriorityLevel,PriorityDescription,Status,CreatedDate,UpdatedDate")] JobPriority jobPriority)
        {
            if (ModelState.IsValid)
            {
                _context.Add(jobPriority);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(jobPriority);
        }

        // GET: JobPriority/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jobPriority = await _context.JobPriority.FindAsync(id);
            if (jobPriority == null)
            {
                return NotFound();
            }
            return View(jobPriority);
        }

        // POST: JobPriority/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("JobPriorityId,PriorityLevel,PriorityDescription,Status,CreatedDate,UpdatedDate")] JobPriority jobPriority)
        {
            if (id != jobPriority.JobPriorityId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(jobPriority);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!JobPriorityExists(jobPriority.JobPriorityId))
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
            return View(jobPriority);
        }

        // GET: JobPriority/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jobPriority = await _context.JobPriority
                .FirstOrDefaultAsync(m => m.JobPriorityId == id);
            if (jobPriority == null)
            {
                return NotFound();
            }

            return View(jobPriority);
        }

        // POST: JobPriority/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var jobPriority = await _context.JobPriority.FindAsync(id);
            if (jobPriority != null)
            {
                _context.JobPriority.Remove(jobPriority);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool JobPriorityExists(int id)
        {
            return _context.JobPriority.Any(e => e.JobPriorityId == id);
        }
    }
}
