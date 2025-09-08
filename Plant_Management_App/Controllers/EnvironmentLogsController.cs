using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Plant_Management_App;
using Plant_Management_App.Models;

namespace Plant_Management_App.Controllers
{
    public class EnvironmentLogsController : Controller
    {
        private readonly EnvironmentLogsContext _context;

        public EnvironmentLogsController(EnvironmentLogsContext context)
        {
            _context = context;
        }

        // GET: EnvironmentLogs
        public async Task<IActionResult> Index()
        {
            var environmentLogsContext = _context.EnvironmentLog.Include(e => e.Greenhouse);
            return View(await environmentLogsContext.ToListAsync());
        }

        // GET: EnvironmentLogs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var environmentLog = await _context.EnvironmentLog
                .Include(e => e.Greenhouse)
                .FirstOrDefaultAsync(m => m.LogID == id);
            if (environmentLog == null)
            {
                return NotFound();
            }

            return View(environmentLog);
        }

        // GET: EnvironmentLogs/Create
        public IActionResult Create()
        {            
            ViewBag.GreenhouseID = new SelectList(_context.Greenhouse.OrderBy(s => s.Name), "GreenhouseID", "Name");
            return View();
        }

        // POST: EnvironmentLogs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LogID,GreenhouseID,LogDate,TemperatureC,HumidityPercent,Notes")] EnvironmentLog environmentLog)
        {
            if (ModelState.IsValid)
            {
                _context.Add(environmentLog);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["GreenhouseID"] = new SelectList(_context.Set<Greenhouse>(), "GreenhouseID", "GreenhouseID", environmentLog.GreenhouseID);
            return View(environmentLog);
        }

        // GET: EnvironmentLogs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var environmentLog = await _context.EnvironmentLog.FindAsync(id);
            if (environmentLog == null)
            {
                return NotFound();
            }
            ViewData["GreenhouseID"] = new SelectList(_context.Set<Greenhouse>(), "GreenhouseID", "GreenhouseID", environmentLog.GreenhouseID);
            return View(environmentLog);
        }

        // POST: EnvironmentLogs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("LogID,GreenhouseID,LogDate,TemperatureC,HumidityPercent,Notes")] EnvironmentLog environmentLog)
        {
            if (id != environmentLog.LogID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(environmentLog);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EnvironmentLogExists(environmentLog.LogID))
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
            ViewData["GreenhouseID"] = new SelectList(_context.Set<Greenhouse>(), "GreenhouseID", "GreenhouseID", environmentLog.GreenhouseID);
            return View(environmentLog);
        }

        // GET: EnvironmentLogs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var environmentLog = await _context.EnvironmentLog
                .Include(e => e.Greenhouse)
                .FirstOrDefaultAsync(m => m.LogID == id);
            if (environmentLog == null)
            {
                return NotFound();
            }

            return View(environmentLog);
        }

        // POST: EnvironmentLogs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var environmentLog = await _context.EnvironmentLog.FindAsync(id);
            if (environmentLog != null)
            {
                _context.EnvironmentLog.Remove(environmentLog);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EnvironmentLogExists(int id)
        {
            return _context.EnvironmentLog.Any(e => e.LogID == id);
        }
    }
}
