using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DFS.Models;

namespace DFS.Data
{
    public class DFSContext : DbContext
    {
        public DFSContext (DbContextOptions<DFSContext> options)
            : base(options)
        {
        }
        public DbSet<DFS.Models.File> File { get; set; } 
    }
}
