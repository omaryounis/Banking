
using BankingPortal.Domain.Entities;
using BankingPortal.Domain.Interfaces;
using BankingPortal.EntityFrameWorkCore; 
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BankingPortal.EntityFrameWorkCore.Repositories
{

    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly BankDbContext _context;

        public GenericRepository(BankDbContext context)
        {
            _context = context;
        }
        private IQueryable<T> ApplyIncludes(IQueryable<T> query, params Expression<Func<T, object>>[] includes)
        {
            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }
            return query;
        }

        public async Task AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
        }

        public async Task AddRangeAsync(IEnumerable<T> entities)
        {
            await _context.Set<T>().AddRangeAsync(entities);
        }

        public IQueryable<T> Find(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _context.Set<T>().Where(predicate).AsNoTracking();
            return ApplyIncludes(query, includes);
        }

        public async Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _context.Set<T>().Where(predicate);
            query = ApplyIncludes(query, includes);
            return await query.FirstOrDefaultAsync();
        }

        public async Task<T> FindAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes)
        {

            IQueryable<T> query = _context.Set<T>();
            query = ApplyIncludes(query, includes);
            return await query.FirstOrDefaultAsync(predicate);

        }

        public IQueryable<T> GetAllWithoutTracking(params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _context.Set<T>().AsNoTracking();
            return ApplyIncludes(query, includes);
        }
        public IQueryable<T> GetAll(params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _context.Set<T>();
            return ApplyIncludes(query, includes);
        }


        public async Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _context.Set<T>();
            query = ApplyIncludes(query, includes);
            return await query.ToListAsync();
        }
        public async Task<IEnumerable<T>> GetAllWithoutTrackingAsync(params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _context.Set<T>().AsNoTracking();
            query = ApplyIncludes(query, includes);
            return await query.ToListAsync();
        }
        public async Task<T> GetByIdAsync(int id, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _context.Set<T>();
            query = ApplyIncludes(query, includes);
            return await query.FirstOrDefaultAsync(e => EF.Property<int>(e, "Id") == id);
        }


        public async Task RemoveAsync(T entity)
        {
            _context.Set<T>().Remove(entity);
            await Task.CompletedTask;
        }

        public async Task RemoveRangeAsync(IEnumerable<T> entities)
        {
            _context.Set<T>().RemoveRange(entities);
            await Task.CompletedTask;
        }

        public async Task UpdateAsync(T entity)
        {
            _context.Set<T>().Update(entity);
            await Task.CompletedTask;

        }
        // Pagination, sorting, and filtering method
        public Task GetPagedAsync(int pageIndex, int pageSize, Expression<Func<T, bool>>? filter, Func<IQueryable<Client>, IOrderedQueryable<T>>? orderBy, Func<object, object> includes, Func<object, object> value)
        {
            throw new NotImplementedException();
        }
        public async Task<(IEnumerable<T> Items, int TotalCount)> GetPagedAsync(
                int pageIndex,
                int pageSize,
                Expression<Func<T, bool>>? filter = null,
                Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
                params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _context.Set<T>();

            if (filter != null)
            {
                query = query.Where(filter);
            }
            query = ApplyIncludes(query, includes);

            var totalCount = await query.CountAsync();


            if (orderBy != null)
            {
                query = orderBy(query);
            }

            var items = await query
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .AsNoTracking()
                .ToListAsync();

            return (items, totalCount);
        }
        public async Task<IQueryable<T>> IncludeAsync( Expression<Func<T, bool>> filter,params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _context.Set<T>();

            foreach (var include in includes)
            {
                query = query.Include(include).Where(filter);
            }

            return await Task.FromResult(query);
        }

       
    }
}
