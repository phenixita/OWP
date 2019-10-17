using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using owp_web.Data;

namespace owp_web.Models
{
    public class OwpContext : DbContext, IOwpContext
    {
        public OwpContext (DbContextOptions<OwpContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        public IQueryable<WorkItemListViewModel> WorkItemList { get
            {
                return WorkItem.Select(wi => new WorkItemListViewModel() { Address = wi.Address, AssignedTo = wi.AssignedTo, AssignmentId = wi.AssignmentId, CreatedOn = wi.CreatedOn, Description = wi.Description, LastChangedOn = wi.LastChangedOn, Latitude = wi.Latitude, Longitude = wi.Longitude, Status = wi.Status, StatusName = wi.StatusName, TypeName = wi.TypeName, WorkItemId = wi.WorkItemId, WorkItemPriority = wi.WorkItemPriority, WorkItemType = wi.WorkItemType });
            } 
        }
        
        public DbSet<owp_web.Models.WorkItem> WorkItem { get; set; }

        Task<EntityEntry> IDbContext.AddAsync(object entity, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        Task<EntityEntry<TEntity>> IDbContext.AddAsync<TEntity>(TEntity entity, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        Task<TEntity> IDbContext.FindAsync<TEntity>(params object[] keyValues)
        {
            throw new NotImplementedException();
        }

        Task<object> IDbContext.FindAsync(Type entityType, object[] keyValues, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        Task<TEntity> IDbContext.FindAsync<TEntity>(object[] keyValues, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        Task<object> IDbContext.FindAsync(Type entityType, params object[] keyValues)
        {
            throw new NotImplementedException();
        }
    }
}
