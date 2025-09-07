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
    public class PlantBatchesController : Controller
    {
        private readonly PlantBatchContext _context;

        public PlantBatchesController(PlantBatchContext context)
        {
            _context = context;
        }

        // GET: PlantBatches
        public async Task<IActionResult> Index()
        {
            var plantBatchContext = _context.PlantBatch.Include(p => p.Greenhouse).Include(p => p.Plant);
            return View(await plantBatchContext.ToListAsync());
        }

        // GET: PlantBatches/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var plantBatch = await _context.PlantBatch
                .Include(p => p.Greenhouse)
                .Include(p => p.Plant)
                .FirstOrDefaultAsync(m => m.BatchID == id);
            if (plantBatch == null)
            {
                return NotFound();
            }

            return View(plantBatch);
        }

        // GET: PlantBatches/Create
        public IActionResult Create()
        {
            ViewBag.PlantID = new SelectList(_context.Plant.OrderBy(p => p.CommonName), "PlantID", "CommonName");
            ViewBag.GreenhouseID = new SelectList(_context.Greenhouse.OrderBy(g => g.Name), "GreenhouseID", "Name");
            return View();
        }

        // POST: PlantBatches/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BatchID,PlantID,GreenhouseID,SeedDate,TransplantDate,QuantityPlanted,ExpectedHarvestDate,Notes")] PlantBatch plantBatch)
        {
            if (ModelState.IsValid)
            {
                _context.Add(plantBatch);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
                        
            ViewData["GreenhouseID"] = new SelectList(_context.Set<Greenhouse>(), "GreenhouseID", "GreenhouseID", plantBatch.GreenhouseID);
            ViewData["PlantID"] = new SelectList(_context.Set<Plant>(), "PlantID", "PlantID", plantBatch.PlantID);
            return View(plantBatch);
        }

        // GET: PlantBatches/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var plantBatch = await _context.PlantBatch.FindAsync(id);
            if (plantBatch == null)
            {
                return NotFound();
            }
            ViewBag.PlantID = new SelectList(_context.Plant.OrderBy(p => p.CommonName), "PlantID", "CommonName");
            ViewBag.GreenhouseID = new SelectList(_context.Greenhouse.OrderBy(g => g.Name), "GreenhouseID", "Name");            
            return View(plantBatch);
        }

        // POST: PlantBatches/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BatchID,PlantID,GreenhouseID,SeedDate,TransplantDate,QuantityPlanted,ExpectedHarvestDate,Notes")] PlantBatch plantBatch)
        {
            if (id != plantBatch.BatchID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(plantBatch);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PlantBatchExists(plantBatch.BatchID))
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
            ViewData["GreenhouseID"] = new SelectList(_context.Set<Greenhouse>(), "GreenhouseID", "GreenhouseID", plantBatch.GreenhouseID);
            ViewData["PlantID"] = new SelectList(_context.Set<Plant>(), "PlantID", "PlantID", plantBatch.PlantID);
            return View(plantBatch);
        }

        // GET: PlantBatches/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var plantBatch = await _context.PlantBatch
                .Include(p => p.Greenhouse)
                .Include(p => p.Plant)
                .FirstOrDefaultAsync(m => m.BatchID == id);
            if (plantBatch == null)
            {
                return NotFound();
            }

            return View(plantBatch);
        }

        // POST: PlantBatches/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var plantBatch = await _context.PlantBatch.FindAsync(id);
            if (plantBatch != null)
            {
                _context.PlantBatch.Remove(plantBatch);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PlantBatchExists(int id)
        {
            return _context.PlantBatch.Any(e => e.BatchID == id);
        }
    }
}
