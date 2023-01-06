using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Storage;
using PhotoStudio.Infrastructure.Data.DBContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoStudio.Infrastructure.Data
{
    public class CommonDbContext : DbContext, IDbContext
    {

        public CommonDbContext(DbContextOptions<PhotoStudioContext> options) : base(options)
        { }
        Database IDbContext.DataBase => ((IDbContext)this).DataBase;

        public async Task<int> Commit()
        {
            try
            {
                return await SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                //Trace.TraceHelper.Error(string.Format("{0} - Entries: {1}", ex.Message, ex.Entries));
                //throw new ExcepcionDeNegocio("Concurrencia", ex);
                throw ex;
            }
            catch (DbUpdateException ex)
            {
                throw ex;
                //Trace.TraceHelper.Error(string.Format("{0} - Entries: {1}", ex.Message, ex.Entries));
                //throw new ExcepcionDeNegocio("Concurrencia", ex);
            }

        }

        public async Task<int> CommitAndRefreshChanges()
        {
            try
            {
                return await SaveChangesAsync(true);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                //Trace.TraceHelper.Error(string.Format("{0} - Entries: {1}", ex.Message, ex.Entries));
                //throw new ExcepcionDeNegocio("Concurrencia", ex);
                throw ex;
            }
            catch (DbUpdateException ex)
            {
                throw ex;
                //Trace.TraceHelper.Error(string.Format("{0} - Entries: {1}", ex.Message, ex.Entries));
                //throw new ExcepcionDeNegocio("Concurrencia", ex);
            }


        }

        public Task RollbackChanges()
        {
            ChangeTracker.Entries()
                .ToList()
                .ForEach(entry => entry.State = EntityState.Unchanged);

            return Task.CompletedTask;
        }

        public DbSet<T> DbSet<T>() where T : class
        {
            return Set<T>();
        }

        EntityEntry IDbContext.Attach<TEntity>(TEntity entity)
        {
            return Attach(entity);
        }



    }
}
