using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using MusalaTest.Domain.Interfaces;

namespace MusalaTest.Domain.Shared
{
    public class BaseRepository<TDEntity, TPEntity, TContext> : IBaseRepository<TDEntity>
        where TDEntity : class
        where TPEntity : class
        where TContext : DbContext
    {
        protected readonly TContext Context;
        protected readonly DbSet<TPEntity> Table;
        protected readonly IMapper Mapper;

        public BaseRepository(TContext dbContext, IMapper mapper)
        {
            Context = dbContext;
            Table = dbContext.Set<TPEntity>();

            Mapper = mapper;
        }

        public IQueryable<TDEntity> GetQuery()
        {
            return Table.ProjectTo<TDEntity>(Mapper.ConfigurationProvider);
        }

        public virtual async Task<ICollection<TDEntity>> GetAll(CancellationToken cancellationToken)
        {
            var ans = await Table.ToListAsync(cancellationToken);

            return ans.Select(e => Mapper.Map<TPEntity, TDEntity>(e)).ToList(); // can be use ConvertAll
        }

        public virtual IQueryable<TDEntity> Where(Expression<Func<TDEntity, bool>> predicate)
        {
            var newPredicate = Mapper.Map<Expression<Func<TPEntity, bool>>>(predicate);

            var temp = Table.Where(newPredicate);

            return temp.ProjectTo<TDEntity>(Mapper.ConfigurationProvider);
        }

        public virtual async Task<TDEntity> Find(Expression<Func<TDEntity, bool>> predicate,
            CancellationToken cancellationToken = default)
        {
            var newPredicate = Mapper.Map<Expression<Func<TPEntity, bool>>>(predicate);

            var ans = await Table.FirstOrDefaultAsync(newPredicate, cancellationToken);

            return Mapper.Map<TPEntity, TDEntity>(ans);
        }

        public virtual async Task<TDEntity> FindReadOnly(Expression<Func<TDEntity, bool>> predicate,
            CancellationToken cancellationToken = default)
        {
            var newPredicate = Mapper.Map<Expression<Func<TPEntity, bool>>>(predicate);
            var ans = await Table.AsNoTracking().FirstOrDefaultAsync(newPredicate, cancellationToken);

            return Mapper.Map<TPEntity, TDEntity>(ans);
        }

        public async Task<TAny> PerformToFirstOrDefaultAsync<TAny>(IQueryable<TAny> query,
            Expression<Func<TAny, bool>> predicate, CancellationToken cancellationToken)
        {
            var newQuery = query.ProjectTo<TPEntity>(Mapper.ConfigurationProvider);
            var newPredicate = Mapper.Map<Expression<Func<TPEntity, bool>>>(predicate);

            var ans = await newQuery.FirstOrDefaultAsync(newPredicate, cancellationToken: cancellationToken);

            return Mapper.Map<TPEntity, TAny>(ans);
        }

        public async Task<TAny> PerformToFirstOrDefaultAsync<TAny>(IQueryable<TAny> query,
            CancellationToken cancellationToken)
        {
            var newQuery = query.ProjectTo<TPEntity>(Mapper.ConfigurationProvider);

            var ans = await newQuery.FirstOrDefaultAsync(cancellationToken: cancellationToken);

            return Mapper.Map<TPEntity, TAny>(ans);
        }

        public async Task<List<TAny>> PerformToListAsync<TAny>(IQueryable<TAny> query,
            CancellationToken cancellationToken)
        {
            var newQuery = query.ProjectTo<TPEntity>(Mapper.ConfigurationProvider);

            var ans = await newQuery.ToListAsync(cancellationToken: cancellationToken);

            return ans.Select(e => Mapper.Map<TPEntity, TAny>(e)).ToList();
        }

        public virtual async Task<List<TAny>> PerformToListAsync<TAny>(Expression<Func<TAny, bool>> predicate,
            CancellationToken cancellationToken = default)
        {
            var newPredicate = Mapper.Map<Expression<Func<TPEntity, bool>>>(predicate);

            var temp = await Table
                .Where(newPredicate).ToListAsync(cancellationToken);

            return temp.Select(e => Mapper.Map<TPEntity, TAny>(e)).ToList();
        }


        public Task<bool> AnyAsync(Expression<Func<TDEntity, bool>> predicate,
            CancellationToken cancellationToken = default)
        {
            var newPredicate = Mapper.Map<Expression<Func<TPEntity, bool>>>(predicate);
            return Table.AnyAsync(newPredicate, cancellationToken);
        }

        public Task<bool> AllAsync(Expression<Func<TDEntity, bool>> predicate,
            CancellationToken cancellationToken = default)
        {
            var newPredicate = Mapper.Map<Expression<Func<TPEntity, bool>>>(predicate);
            return Table.AllAsync(newPredicate, cancellationToken);
        }

        public Task<decimal> SumAsync(IQueryable<TDEntity> query, Expression<Func<TDEntity, decimal>> sum,
            CancellationToken cancellationToken = default)
        {
            var newQuery = query.ProjectTo<TPEntity>(Mapper.ConfigurationProvider);
            var newSum = Mapper.Map<Expression<Func<TPEntity, decimal>>>(sum);

            return newQuery.SumAsync(newSum, cancellationToken);
        }

        public Task<int> CountAsync(IQueryable<TDEntity> query, CancellationToken cancellationToken = default)
        {
            var newQuery = query.ProjectTo<TPEntity>(Mapper.ConfigurationProvider);

            return newQuery.CountAsync(cancellationToken);
        }

        public virtual async Task<TDEntity> CreateOne(TDEntity obj, CancellationToken cancellationToken = default)
        {
            var newObj = Mapper.Map<TDEntity, TPEntity>(obj);

            try
            {
                var ans = await Table.AddAsync(newObj, cancellationToken);

                await SaveAllAsync(cancellationToken);

                return Mapper.Map<TPEntity, TDEntity>(ans.Entity);
            }
            catch (DbUpdateException ex)
            {
                throw new Exception("Unable to save data. A register with the same data already exist", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("A general exception occurred saving data", ex);
            }
        }

        public virtual async Task CreateRange(IEnumerable<TDEntity> objs, CancellationToken cancellationToken = default)
        {
            var newObjs = objs.Select(e => Mapper.Map<TDEntity, TPEntity>(e)).ToList();

            try
            {
                await Table.AddRangeAsync(newObjs, cancellationToken);
                await SaveAllAsync(cancellationToken);
            }
            catch (DbUpdateException ex)
            {
                throw new Exception("Unable to save data. A register with the same data already exist", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("A general exception occurred saving data", ex);
            }
        }

        public virtual TDEntity UpdateOneOnly(TDEntity obj)
        {
            var newObj = Mapper.Map<TDEntity, TPEntity>(obj);

            Table.Attach(newObj);

            return obj;
        }

        public virtual void UpdateRangeOnly(IEnumerable<TDEntity> objs)
        {
            var newObjs = objs.Select(e => Mapper.Map<TDEntity, TPEntity>(e)).ToList();

            Table.AttachRange(newObjs);
        }

        public virtual async Task<TDEntity> UpdateOneAndSave(TDEntity obj,
            CancellationToken cancellationToken = default)
        {
            // foreach (var entityEntry in Context.ChangeTracker.Entries()) {
            //     Console.WriteLine(
            //         $"Found {entityEntry.Metadata.Name} entity with ID {entityEntry.Property("Id").CurrentValue}");
            // }

            var newObj = Mapper.Map<TDEntity, TPEntity>(obj);

            try
            {
                Table.Attach(newObj);

                Context.Entry(newObj).State = EntityState.Modified;

                await SaveAllAsync(cancellationToken);

                return obj;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                var data = ex.Entries.Single();
                var entity = await data.GetDatabaseValuesAsync(cancellationToken);
                var msg = "Unable to update data. ";
                if (entity == null)
                {
                    msg += "The entity being updated is already deleted by another user";
                }
                else
                {
                    await data.ReloadAsync(cancellationToken);
                    msg += "The entity being updated has already been updated by another user";
                }

                throw new Exception(msg, ex);
            }
            catch (DbUpdateException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception("A general exception occurred editing data", ex);
            }
        }

        public virtual async Task<bool> UpdateRangeAndSave(IEnumerable<TDEntity> objs,
            CancellationToken cancellationToken = default)
        {
            var newObjs = objs.Select(e => Mapper.Map<TDEntity, TPEntity>(e)).ToList();

            try
            {
                Table.AttachRange(newObjs);

                await SaveAllAsync(cancellationToken);

                return true;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                var data = ex.Entries.Single();
                var entity = await data.GetDatabaseValuesAsync(cancellationToken);
                var msg = "Unable to update data. ";
                if (entity == null)
                {
                    msg += "The entity being updated is already deleted by another user";
                }
                else
                {
                    await data.ReloadAsync(cancellationToken);
                    msg += "The entity being updated has already been updated by another user";
                }

                throw new Exception(msg, ex);
            }
            catch (DbUpdateException ex)
            {
                throw new Exception("Unable to save data. A register with the same data already exist", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("A general exception occurred editing data", ex);
            }
        }

        public virtual void DeleteOneOnly(Expression<Func<TDEntity, bool>> predicate)
        {
            var newPredicate = Mapper.Map<Expression<Func<TPEntity, bool>>>(predicate);
            try
            {
                var elements = Table.Where(newPredicate);

                foreach (var obj in elements)
                {
                    Table.Remove(obj);
                }
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new Exception("Concurrency exception occurred deleting data", ex);
            }
            catch (DbUpdateException ex)
            {
                throw new Exception(
                    "A database exception occurred deleting data. The register that was attempted to eliminate is still associated with another one",
                    ex);
            }
            catch (Exception ex)
            {
                throw new Exception("A general exception occurred deleting data", ex);
            }
        }

        public virtual async Task<int> DeleteOneAndSave(Expression<Func<TDEntity, bool>> predicate,
            CancellationToken cancellationToken = default)
        {
            var newPredicate = Mapper.Map<Expression<Func<TPEntity, bool>>>(predicate);

            try
            {
                var elements = Table.Where(newPredicate);

                foreach (var obj in elements)
                {
                    Table.Remove(obj);
                }

                return await SaveAllAsync(cancellationToken);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new Exception("Concurrency exception occurred deleting data", ex);
            }
            catch (DbUpdateException ex)
            {
                throw new Exception(
                    "A database exception occurred deleting data. The register that was attempted to eliminate is still associated with another one",
                    ex);
            }
            catch (Exception ex)
            {
                throw new Exception("A general exception occurred deleting data", ex);
            }
        }

        public void DeleteOneOnly(TDEntity obj)
        {
            Table.Remove(Mapper.Map<TDEntity, TPEntity>(obj));
        }

        public virtual async Task<int> DeleteOneAndSave(TDEntity obj, CancellationToken cancellationToken = default)
        {
            var newObj = Mapper.Map<TDEntity, TPEntity>(obj);
            try
            {
                Table.Remove(newObj);
                return await SaveAllAsync(cancellationToken);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new Exception("Concurrency exception occurred deleting data", ex);
            }
            catch (DbUpdateException ex)
            {
                throw new Exception(
                    "A database exception occurred deleting data. The register that was attempted to eliminate is still associated with another one",
                    ex);
            }
            catch (Exception ex)
            {
                throw new Exception("A general exception occurred deleting data", ex);
            }
        }

        public void DeleteRangeOnly(IEnumerable<TDEntity> objs)
        {
            var newObjs = objs.Select(e => Mapper.Map<TDEntity, TPEntity>(e)).ToList();

            Table.RemoveRange(newObjs);
        }

        public virtual async Task<int> DeleteRangeAndSave(IEnumerable<TDEntity> objs,
            CancellationToken cancellationToken = default)
        {
            var newObjs = objs.Select(e => Mapper.Map<TDEntity, TPEntity>(e)).ToList();

            try
            {
                try
                {
                    Table.AttachRange(newObjs);
                }
                catch (Exception e)
                {
                    foreach (var i in newObjs)
                        Context.Entry(i).State = EntityState.Deleted;
                }

                this.Table.RemoveRange(newObjs);
                return await SaveAllAsync(cancellationToken);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new Exception("Concurrency exception occurred deleting data", ex);
            }
            catch (DbUpdateException ex)
            {
                throw new Exception(
                    "A database exception occurred deleting data. The register that was attempted to eliminate is still associated with another one",
                    ex);
            }
            catch (Exception ex)
            {
                throw new Exception("A general exception occurred deleting data", ex);
            }
        }

        public Task<int> SaveAllAsync(CancellationToken cancellationToken)
        {
            return Context.SaveChangesAsync(cancellationToken);
        }
    }
}