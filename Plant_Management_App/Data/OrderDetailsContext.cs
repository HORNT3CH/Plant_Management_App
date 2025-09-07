using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Plant_Management_App.Models;

namespace Plant_Management_App
{
    public class OrderDetailsContext : DbContext
    {
        public OrderDetailsContext (DbContextOptions<OrderDetailsContext> options)
            : base(options)
        {
        }

        public DbSet<Plant_Management_App.Models.OrderDetail> OrderDetail { get; set; } = default!;
    }
}
