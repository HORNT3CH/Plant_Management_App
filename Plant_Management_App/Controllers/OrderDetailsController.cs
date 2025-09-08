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
    public class OrderDetailsController : Controller
    {
        private readonly OrderDetailsContext _context;

        public OrderDetailsController(OrderDetailsContext context)
        {
            _context = context;
        }

        // GET: OrderDetails
        public async Task<IActionResult> Index()
        {
            var orderDetailsContext = _context.OrderDetail.Include(o => o.Inventory).Include(o => o.Order);
            return View(await orderDetailsContext.ToListAsync());
        }

        // GET: OrderDetails/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderDetail = await _context.OrderDetail
                .Include(o => o.Inventory)
                .Include(o => o.Order)
                .FirstOrDefaultAsync(m => m.OrderDetailID == id);
            if (orderDetail == null)
            {
                return NotFound();
            }

            return View(orderDetail);
        }

        // GET: OrderDetails/Create
        public IActionResult Create(int? ordersId)
        {
            if (ordersId == null)
                return BadRequest("Order ID is required.");

            var model = new OrderDetail
            {
                OrderID = ordersId.Value
            };

            ViewData["InventoryID"] = new SelectList(
                _context.Inventory.Include(i => i.Plant)
                .Select(i => new
                {
                    i.InventoryID,
                    Description = i.Plant.CommonName + " - " + i.Location
                }),
                "InventoryID", "Description");

            ViewData["OrderID"] = new SelectList(_context.Order, "OrderID", "OrderID", ordersId);

            return View(model); // ✅ pass the model
        }


        // POST: OrderDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OrderDetailID,OrderID,InventoryID,Quantity,UnitPrice")] OrderDetail orderDetail)
        {
            var inventory = await _context.Inventory.FindAsync(orderDetail.InventoryID);

            if (inventory == null)
            {
                ModelState.AddModelError("", "Selected inventory item does not exist.");
            }
            else if (orderDetail.Quantity > inventory.QuantityAvailable)
            {
                ModelState.AddModelError("Quantity", $"Not enough stock. Only {inventory.QuantityAvailable} units available.");
            }

            if (ModelState.IsValid)
            {
                // Decrease stock
                inventory.QuantityAvailable -= orderDetail.Quantity;

                _context.Add(orderDetail);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            // Repopulate dropdowns on validation error
            ViewData["InventoryID"] = new SelectList(_context.Set<Inventory>(), "InventoryID", "InventoryID", orderDetail.InventoryID);
            ViewData["OrderID"] = new SelectList(_context.Set<Order>(), "OrderID", "OrderID", orderDetail.OrderID);
            return View(orderDetail);
        }

        [HttpGet]
        public async Task<IActionResult> GetUnitPrice(int inventoryId)
        {
            var price = await _context.Inventory
                .Where(i => i.InventoryID == inventoryId)
                .Select(i => i.UnitPrice)
                .FirstOrDefaultAsync();

            return Json(price);
        }

        // GET: OrderDetails/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderDetail = await _context.OrderDetail.FindAsync(id);
            if (orderDetail == null)
            {
                return NotFound();
            }
            ViewData["InventoryID"] = new SelectList(_context.Set<Inventory>(), "InventoryID", "InventoryID", orderDetail.InventoryID);
            ViewData["OrderID"] = new SelectList(_context.Set<Order>(), "OrderID", "OrderID", orderDetail.OrderID);
            return View(orderDetail);
        }

        // POST: OrderDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OrderDetailID,OrderID,InventoryID,Quantity,UnitPrice")] OrderDetail orderDetail)
        {
            if (id != orderDetail.OrderDetailID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(orderDetail);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderDetailExists(orderDetail.OrderDetailID))
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
            ViewData["InventoryID"] = new SelectList(_context.Set<Inventory>(), "InventoryID", "InventoryID", orderDetail.InventoryID);
            ViewData["OrderID"] = new SelectList(_context.Set<Order>(), "OrderID", "OrderID", orderDetail.OrderID);
            return View(orderDetail);
        }

        // GET: OrderDetails/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderDetail = await _context.OrderDetail
                .Include(o => o.Inventory)
                .Include(o => o.Order)
                .FirstOrDefaultAsync(m => m.OrderDetailID == id);
            if (orderDetail == null)
            {
                return NotFound();
            }

            return View(orderDetail);
        }

        // POST: OrderDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var orderDetail = await _context.OrderDetail.FindAsync(id);
            if (orderDetail != null)
            {
                _context.OrderDetail.Remove(orderDetail);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderDetailExists(int id)
        {
            return _context.OrderDetail.Any(e => e.OrderDetailID == id);
        }
    }
}
