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
    public class GreenhousesController : Controller
    {
        private readonly GreenhousesContext _context;

        public GreenhousesController(GreenhousesContext context)
        {
            _context = context;
        }

        // GET: Greenhouses
        public async Task<IActionResult> Index()
        {
            return View(await _context.Greenhouse.ToListAsync());
        }

        // GET: Greenhouses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var greenhouse = await _context.Greenhouse
                .FirstOrDefaultAsync(m => m.GreenhouseID == id);
            if (greenhouse == null)
            {
                return NotFound();
            }

            return View(greenhouse);
        }

        // GET: Greenhouses/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Greenhouses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("GreenhouseID,Name,Location,SizeSqFt,TemperatureControlled,HumidityControlled")] Greenhouse greenhouse)
        {
            if (ModelState.IsValid)
            {
                _context.Add(greenhouse);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(greenhouse);
        }

        // GET: Greenhouses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var greenhouse = await _context.Greenhouse.FindAsync(id);
            if (greenhouse == null)
            {
                return NotFound();
            }
            return View(greenhouse);
        }

        // POST: Greenhouses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("GreenhouseID,Name,Location,SizeSqFt,TemperatureControlled,HumidityControlled")] Greenhouse greenhouse)
        {
            if (id != greenhouse.GreenhouseID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(greenhouse);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GreenhouseExists(greenhouse.GreenhouseID))
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
            return View(greenhouse);
        }

        // GET: Greenhouses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var greenhouse = await _context.Greenhouse
                .FirstOrDefaultAsync(m => m.GreenhouseID == id);
            if (greenhouse == null)
            {
                return NotFound();
            }

            return View(greenhouse);
        }

        // POST: Greenhouses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var greenhouse = await _context.Greenhouse.FindAsync(id);
            if (greenhouse != null)
            {
                _context.Greenhouse.Remove(greenhouse);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GreenhouseExists(int id)
        {
            return _context.Greenhouse.Any(e => e.GreenhouseID == id);
        }
    }
}
