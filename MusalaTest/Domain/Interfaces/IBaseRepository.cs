using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace MusalaTest.Domain.Interfaces
{
    public interface IBaseRepository<TEntity>
    {
        IQueryable<TEntity> GetQuery();

        Task<ICollection<TEntity>> GetAll(CancellationToken cancellationToken);

        IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> predicate);

        Task<TEntity> Find(Expression<Func<TEntity, bool>> predicate,
            CancellationToken cancellationToken = default);

        Task<TEntity> FindReadOnly(Expression<Func<TEntity, bool>> predicate,
            CancellationToken cancellationToken = default);

        Task<TAny> PerformToFirstOrDefaultAsync<TAny>(IQueryable<TAny> query, Expression<Func<TAny, bool>> predicate,
            CancellationToken cancellationToken);

        Task<TAny> PerformToFirstOrDefaultAsync<TAny>(IQueryable<TAny> query, CancellationToken cancellationToken);

        Task<List<TAny>> PerformToListAsync<TAny>(IQueryable<TAny> query, CancellationToken cancellationToken);

        Task<List<TAny>> PerformToListAsync<TAny>(Expression<Func<TAny, bool>> predicate,
            CancellationToken cancellationToken = default);

        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate,
            CancellationToken cancellationToken = default);

        Task<bool> AllAsync(Expression<Func<TEntity, bool>> predicate,
            CancellationToken cancellationToken = default);

        Task<decimal> SumAsync(IQueryable<TEntity> query, Expression<Func<TEntity, decimal>> sum,
            CancellationToken cancellationToken = default);

        Task<int> CountAsync(IQueryable<TEntity> query,
            CancellationToken cancellationToken = default);

        Task<TEntity> CreateOne(TEntity obj,
            CancellationToken cancellationToken = default);

        Task CreateRange(IEnumerable<TEntity> objs,
            CancellationToken cancellationToken = default);

        TEntity UpdateOneOnly(TEntity obj);

        void UpdateRangeOnly(IEnumerable<TEntity> objs);

        Task<TEntity> UpdateOneAndSave(TEntity obj,
            CancellationToken cancellationToken = default);

        Task<bool> UpdateRangeAndSave(IEnumerable<TEntity> objs,
            CancellationToken cancellationToken = default);

        void DeleteOneOnly(Expression<Func<TEntity, bool>> predicate);

        Task<int> DeleteOneAndSave(Expression<Func<TEntity, bool>> predicate,
            CancellationToken cancellationToken = default);

        void DeleteOneOnly(TEntity obj);

        Task<int> DeleteOneAndSave(TEntity obj,
            CancellationToken cancellationToken = default);

        void DeleteRangeOnly(IEnumerable<TEntity> objs);

        Task<int> DeleteRangeAndSave(IEnumerable<TEntity> objs,
            CancellationToken cancellationToken = default);

        Task<int> SaveAllAsync(CancellationToken cancellationToken);
    }
}