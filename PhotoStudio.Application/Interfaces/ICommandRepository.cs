using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PhotoStudio.Application.Interfaces
{
    public interface ICommandRepository<TEntity> : IRepository
    {
        Task<TEntity> AddAsync(TEntity entity);

        Task<TEntity> UpdateAsync(TEntity entity);

        Task DeleteAsync(TEntity entity);

        IQueryable<TEntity> GetAll();

        IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);
    }
}
