using Microsoft.EntityFrameworkCore;
using PhotoStudio.Application.Interfaces;
using PhotoStudio.Domain.Interfaces;
using PhotoStudio.Infrastructure.Data.DBContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PhotoStudio.Infrastructure.Data
{
    public class CommandRepository<TEntity> : ICommandRepository<TEntity> where TEntity : class
    {
        public IUnitOfWork UnitOfWork { get; set; }

        public IDbContext Context { get => UnitOfWork as IDbContext; }

        public CommandRepository(PhotoStudioContext context)
        {
            UnitOfWork = context;
           
        }

        private DbSet<TEntity> _dbSet;
        public DbSet<TEntity> DbSet { get { return _dbSet ?? (_dbSet = Context.DbSet<TEntity>()); } }

        public async virtual Task<TEntity> AddAsync(TEntity entity)
        {
            await DbSet.AddAsync(entity);

            return entity;
        }

        public virtual Task DeleteAsync(TEntity entity)
        {
            DbSet.Remove(entity);
            return Task.CompletedTask;
        }

        public async virtual Task<TEntity> UpdateAsync(TEntity entity)
        {
            Context.Attach(entity).State = EntityState.Modified;

            return await Task.FromResult(entity);            

        }

        public IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return DbSet.Where(predicate);
        }

        public IQueryable<TEntity> GetAll()
        {
            return DbSet;
        }
    }
}
