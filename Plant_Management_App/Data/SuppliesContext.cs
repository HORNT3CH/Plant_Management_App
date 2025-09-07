using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Plant_Management_App.Models;

namespace Plant_Management_App
{
    public class SuppliesContext : DbContext
    {
        public SuppliesContext (DbContextOptions<SuppliesContext> options)
            : base(options)
        {
        }

        public DbSet<Plant_Management_App.Models.Supply> Supply { get; set; } = default!;
    }
}
