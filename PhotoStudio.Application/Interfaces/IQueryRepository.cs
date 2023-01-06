using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PhotoStudio.Application.Interfaces
{
    public interface IQueryRepository<TEntity> : IRepository
    {
        IQueryable<TEntity> GetAll();

        IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);

        IQueryable<TEntity> OrderBy(Expression<Func<TEntity, object>> property);

    }
}
