using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Plant_Management_App.Models;

namespace Plant_Management_App
{
    public class EnvironmentLogsContext : DbContext
    {
        public EnvironmentLogsContext (DbContextOptions<EnvironmentLogsContext> options)
            : base(options)
        {
        }

        public DbSet<Plant_Management_App.Models.EnvironmentLog> EnvironmentLog { get; set; } = default!;
        public DbSet<Plant_Management_App.Models.Greenhouse> Greenhouse { get; set; } = default!;
    }
}
