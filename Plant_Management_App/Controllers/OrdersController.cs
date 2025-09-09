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
    public class OrdersController : Controller
    {
        private readonly OrdersContext _context;

        public OrdersController(OrdersContext context)
        {
            _context = context;
        }

        // GET: Orders
        public async Task<IActionResult> Index()
        {
            var orders = await _context.Order
                .Include(o => o.Customer)
                .Include(o => o.OrderDetails)
                .ToListAsync();

            // Calculate total for each order
            foreach (var order in orders)
            {
                order.TotalAmount = order.OrderDetails.Sum(od => od.Quantity * od.UnitPrice);
            }

            await _context.SaveChangesAsync(); // Optional if you want to persist changes

            return View(orders);
        }


        // GET: Orders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Order
                .Include(o => o.Customer)
                .Include(o => o.OrderDetails)
                    .ThenInclude(od => od.Inventory)
                        .ThenInclude(i => i.Plant)
                .FirstOrDefaultAsync(m => m.OrderID == id);

            if (order == null)
            {
                return NotFound();
            }

            // Dynamically calculate the total (optional if you store it elsewhere)
            order.TotalAmount = order.OrderDetails.Sum(od => od.Quantity * od.UnitPrice);
            await _context.SaveChangesAsync(); // Optional: saves the new total to DB

            return View(order);
        }


        // GET: Orders/Create
        public IActionResult Create(int? ordersId)
        {
            ViewBag.CustomerID = new SelectList(
            _context.Customer
                .OrderBy(p => p.FirstName)
                .Select(p => new
                {
                    p.CustomerID,
                    FullName = p.FirstName + " " + p.LastName
                }),
            "CustomerID",
            "FullName"
        );
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OrderID,CustomerID,OrderDate")] Order order)
        {
            if (ModelState.IsValid)
            {
                _context.Add(order);
                await _context.SaveChangesAsync();
                // Redirect user to OrderDetails/Create with this OrderID
                return RedirectToAction("Create", "OrderDetails", new { orderId = order.OrderID });
            }
            ViewData["CustomerID"] = new SelectList(_context.Customer, "CustomerID", "CustomerID", order.CustomerID);
            return View(order);
        }

        public async Task<IActionResult> Print(int id)
        {
            var order = await _context.Order
                .Include(o => o.Customer)
                .Include(o => o.OrderDetails)
                    .ThenInclude(d => d.Inventory)
                        .ThenInclude(i => i.Plant)
                .FirstOrDefaultAsync(o => o.OrderID == id);

            if (order == null)
                return NotFound();


            return View("Print", order!); // or just return View(order);
        }

        // GET: Orders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Order.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            ViewBag.CustomerID = new SelectList(
                _context.Customer
                    .OrderBy(c => c.FirstName)
                    .Select(c => new
                    {
                        c.CustomerID,
                        FullName = c.FirstName + " " + c.LastName
                    }),
                "CustomerID",
                "FullName"
            );            
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OrderID,CustomerID,OrderDate")] Order order)
        {
            if (id != order.OrderID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(order);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(order.OrderID))
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
            ViewData["CustomerID"] = new SelectList(_context.Set<Customer>(), "CustomerID", "CustomerID", order.CustomerID);
            return View(order);
        }

        // GET: Orders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Order
                .Include(o => o.Customer)
                .FirstOrDefaultAsync(m => m.OrderID == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var order = await _context.Order.FindAsync(id);
            if (order != null)
            {
                _context.Order.Remove(order);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderExists(int id)
        {
            return _context.Order.Any(e => e.OrderID == id);
        }
    }
}
