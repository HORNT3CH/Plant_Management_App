using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Plant_Management_App.Models;

namespace Plant_Management_App
{
    public class PlantBatchContext : DbContext
    {
        public PlantBatchContext (DbContextOptions<PlantBatchContext> options)
            : base(options)
        {
        }

        public DbSet<Plant_Management_App.Models.PlantBatch> PlantBatch { get; set; } = default!;
        public DbSet<Greenhouse> Greenhouse { get; set; } = default!;
        public DbSet<Plant_Management_App.Models.Plant> Plant { get; set; } = default!;
    }
}
