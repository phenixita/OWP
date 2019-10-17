using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using owp_web.Models;

namespace owp_web.Data
{
    public interface IOwpContext : IDbContext
    {
        DbSet<WorkItem> WorkItem { get; set; }

        IQueryable<WorkItemListViewModel>WorkItemList{get;}
    }
}
