using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace owp_web.Models
{
    public class OwpContext : DbContext
    {
        public OwpContext (DbContextOptions<OwpContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<owp_web.Models.WorkItem> WorkItem { get; set; }
    }
}
