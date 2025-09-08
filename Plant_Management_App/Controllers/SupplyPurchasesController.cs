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
    public class SupplyPurchasesController : Controller
    {
        private readonly SupplyPurchasesContext _context;

        public SupplyPurchasesController(SupplyPurchasesContext context)
        {
            _context = context;
        }

        // GET: SupplyPurchases
        public async Task<IActionResult> Index()
        {
            var supplyPurchasesContext = _context.SupplyPurchase.Include(s => s.Supply);
            return View(await supplyPurchasesContext.ToListAsync());
        }

        // GET: SupplyPurchases/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var supplyPurchase = await _context.SupplyPurchase
                .Include(s => s.Supply)
                .FirstOrDefaultAsync(m => m.PurchaseID == id);
            if (supplyPurchase == null)
            {
                return NotFound();
            }

            return View(supplyPurchase);
        }

        // GET: SupplyPurchases/Create
        public IActionResult Create()
        {            
            ViewBag.SupplyID = new SelectList(_context.Supply.OrderBy(s => s.Name), "SupplyID", "Name");
            return View();
        }

        // POST: SupplyPurchases/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PurchaseID,SupplyID,PurchaseDate,Quantity,TotalCost")] SupplyPurchase supplyPurchase)
        {
            if (ModelState.IsValid)
            {
                _context.Add(supplyPurchase);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["SupplyID"] = new SelectList(_context.Set<Supply>(), "SupplyID", "SupplyID", supplyPurchase.SupplyID);
            return View(supplyPurchase);
        }

        // GET: SupplyPurchases/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var supplyPurchase = await _context.SupplyPurchase.FindAsync(id);
            if (supplyPurchase == null)
            {
                return NotFound();
            }
            ViewData["SupplyID"] = new SelectList(_context.Set<Supply>(), "SupplyID", "SupplyID", supplyPurchase.SupplyID);
            return View(supplyPurchase);
        }

        // POST: SupplyPurchases/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PurchaseID,SupplyID,PurchaseDate,Quantity,TotalCost")] SupplyPurchase supplyPurchase)
        {
            if (id != supplyPurchase.PurchaseID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(supplyPurchase);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SupplyPurchaseExists(supplyPurchase.PurchaseID))
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
            ViewData["SupplyID"] = new SelectList(_context.Set<Supply>(), "SupplyID", "SupplyID", supplyPurchase.SupplyID);
            return View(supplyPurchase);
        }

        // GET: SupplyPurchases/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var supplyPurchase = await _context.SupplyPurchase
                .Include(s => s.Supply)
                .FirstOrDefaultAsync(m => m.PurchaseID == id);
            if (supplyPurchase == null)
            {
                return NotFound();
            }

            return View(supplyPurchase);
        }

        // POST: SupplyPurchases/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var supplyPurchase = await _context.SupplyPurchase.FindAsync(id);
            if (supplyPurchase != null)
            {
                _context.SupplyPurchase.Remove(supplyPurchase);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SupplyPurchaseExists(int id)
        {
            return _context.SupplyPurchase.Any(e => e.PurchaseID == id);
        }
    }
}
