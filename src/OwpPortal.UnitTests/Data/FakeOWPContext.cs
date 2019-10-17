using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using owp_web.Data;
using owp_web.Models;

namespace OwpPortal.UnitTests.Data
{
    public class FakeOwpContext : DbContext, IOwpContext
    {
        public DbSet<owp_web.Models.WorkItem> WorkItem { get; set; }

        public IQueryable<WorkItemListViewModel> WorkItemList => throw new NotImplementedException();

        Task<EntityEntry> IDbContext.AddAsync(object entity, CancellationToken cancellationToken)
        {
            return null;
        }

        Task<EntityEntry<TEntity>> IDbContext.AddAsync<TEntity>(TEntity entity, CancellationToken cancellationToken)
        {
            return null;
        }

        Task<TEntity> IDbContext.FindAsync<TEntity>(params object[] keyValues)
        {
            return null;
        }

        Task<object> IDbContext.FindAsync(Type entityType, object[] keyValues, CancellationToken cancellationToken)
        {
            return null;
        }

        Task<TEntity> IDbContext.FindAsync<TEntity>(object[] keyValues, CancellationToken cancellationToken)
        {
            return null;
        }

        Task<object> IDbContext.FindAsync(Type entityType, params object[] keyValues)
        {
            return null;
        }
    }
}
