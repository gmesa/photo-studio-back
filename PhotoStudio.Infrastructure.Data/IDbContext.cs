using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage;
using PhotoStudio.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoStudio.Infrastructure.Data
{
    public interface IDbContext : IUnitOfWork
    {
        Database DataBase { get; }

        /// <summary>
        ///     Database Set.
        /// </summary>
        /// <typeparam name="T">Database Entity to retrieve</typeparam>
        /// <returns>Database set of database entity.</returns>
        DbSet<T> DbSet<T>() where T : class;

        EntityEntry Attach<TEntity>(TEntity entity) where TEntity: class;


    }
}
