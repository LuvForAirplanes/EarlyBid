using EarlyBid.Server.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EarlyBid.Server.Services
{
    public class Repository<TEntity, TId> where TEntity : class, new()
    {
        public ApplicationDbContext context;

        public bool IsSortDescending { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="entity">Entity to be used at the top of the select list.</param>
        public Repository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public virtual async Task<TEntity> CreateAsync(TEntity entity, bool saveNow = true)
        {
            await context.Set<TEntity>().AddAsync(entity);
            if (saveNow)
                await context.SaveChangesAsync();
            return entity;
        }

        public virtual async Task DeleteAsync(TId id, bool saveNow = true)
        {
            var entity = await GetByIdAsync(id);
            context.Set<TEntity>().Remove(entity);
            if (saveNow)
                await context.SaveChangesAsync();
        }

        public virtual async Task<bool> ExistsAsync(TId id)
        {
            var x = await GetByIdAsync(id, true);
            return x != null;
        }

        public virtual IQueryable<TEntity> Get(bool disableTracking = true)
        {
            if (disableTracking)
                return context.Set<TEntity>().AsNoTracking();
            else
                return context.Set<TEntity>();
        }

        public virtual async Task<TEntity> GetByIdAsync(TId id, bool disableTracking = true)
        {
            var e = await context.Set<TEntity>().FindAsync(id);

            if (disableTracking && e != null)
                context.Entry(e).State = EntityState.Detached;

            return e;
        }

        public virtual async Task<TEntity> UpdateAsync(TEntity entity, TId id, bool saveNow = true)
        {
            context.Set<TEntity>().Update(entity);
            if (saveNow)
                await context.SaveChangesAsync();
            return entity;
        }

        public virtual async Task<List<TEntity>> ListAsync()
        {
            return await Get()
                .ToListAsync();

        }

        public virtual async Task SaveChangesAsync()
        {
            await context.SaveChangesAsync();
        }
    }
}
