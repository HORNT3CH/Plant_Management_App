using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Plant_Management_App.Models;

namespace Plant_Management_App
{
    public class InventoriesContext : DbContext
    {
        public InventoriesContext (DbContextOptions<InventoriesContext> options)
            : base(options)
        {
        }

        public DbSet<Plant_Management_App.Models.Inventory> Inventory { get; set; } = default!;
    }
}
