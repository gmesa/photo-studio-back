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
    public class QueryRepository<TEntity> : IQueryRepository<TEntity> where TEntity : class
    {
        public IUnitOfWork UnitOfWork { get; set; }

        protected IDbContext Context
        {
            get { return (IDbContext)UnitOfWork; }
        }

        public QueryRepository(PhotoStudioContext context)
        {
            UnitOfWork = context;

        }

        private DbSet<TEntity> _dbSet;
        public DbSet<TEntity> DbSet { get { return _dbSet ?? (_dbSet = Context.DbSet<TEntity>()); } }


        public IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return DbSet.Where(predicate);
        }

        public IQueryable<TEntity> GetAll()
        {
            return DbSet;
        }

        public virtual IQueryable<TEntity> OrderBy(Expression<Func<TEntity, object>> property)
        {
            return  GetAll().OrderBy(property);
        }
    }
}
