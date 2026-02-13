using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Storage;
using PruebaTecnica.Common.Core.Base;

namespace PruebaTecnica.Common.Application
{
    public interface IRepositoryBase<T> where T : EntityBase
    {
        Task<IDbContextTransaction> BeginTransactionAsync();
        Task<IReadOnlyList<T>> GetAllAsync();
        Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate);

        Task<T> GetEntityAsync(Expression<Func<T, bool>> predicate = null,
            List<Expression<Func<T, object>>> includes = null,
            bool disableTracking = true);
            
        Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate = null,
                                       Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
                                       List<Expression<Func<T, object>>> includes = null,
                                       bool disableTracking = true);

        Task<T> GetByIdAsync(Guid id);
        Task<T> GetByCodigoAsync(Expression<Func<T, bool>> predicate);
        Task<T> AddAsync(T entity);
        Task AddRangeAsync(List<T> entities);
        Task<T> UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        Task<int> GetCountAsync(Expression<Func<T, bool>> predicate);
        Task UpdateRangeAsync(List<T> entities);
        T Detach(T entity);
    }
}
