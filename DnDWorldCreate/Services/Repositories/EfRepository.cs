using DnDWorldCreate.Data;
using DnDWorldCreate.Data.Entitys;
using DnDWorldCreate.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System.Linq.Expressions;

namespace DnDWorldCreate.Services.Repositories
{
    public class EfRepository<T> : IRepository<T> where T : BaseEntity
    {
        public EfRepository(IDbContextFactory<DnDWorldContext> context) { }

        public async Task<IEnumerable<T>> GetAllAsync(DnDWorldContext context)
        {
            return await context.Set<T>().ToListAsync();
        }

        public async Task<T> AddAsync(T entity, DnDWorldContext context)
        {
            await context.Set<T>().AddAsync(entity);
            await context.SaveChangesAsync();
            return entity;
        }

        public async Task<T> UpdateAsync(T entity, DnDWorldContext context)
        {
            context.Entry(entity).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> DeleteAsync(int id, DnDWorldContext context)
        {
            var entity = await GetByIdAsync(id, context);
            if (entity == null) return false;

            context.Set<T>().Remove(entity);
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<T?> GetByIdAsync(int id, DnDWorldContext context)
        {
            return await context.Set<T>().FindAsync(id);
        }

        public async Task<int> SaveChangesAsync(DnDWorldContext context)
        {
            return await context.SaveChangesAsync();
        }

        public void Remove(T entity, DnDWorldContext context)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            context.Set<T>().Remove(entity);
        }

        public void Update(T entity, DnDWorldContext context)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            context.Entry(entity).State = EntityState.Modified;
        }
        public async Task UpdateWithRelatedAsync(T entity,DnDWorldContext context,  params Expression<Func<T, object>>[] navigationProperties)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            context.Entry(entity).State = EntityState.Modified;

            foreach (var navigationProperty in navigationProperties)
            {
                var relatedEntity = navigationProperty.Compile()(entity);
                if (relatedEntity != null)
                {
                    var relatedEntry = context.Entry(relatedEntity);
                    if (relatedEntry.State == EntityState.Detached)
                    {
                        var databaseEntity = await relatedEntry.GetDatabaseValuesAsync();
                        if (databaseEntity != null)
                        {
                            relatedEntry.CurrentValues.SetValues(relatedEntity);
                        }
                    }
                }
            }

            await context.SaveChangesAsync();
        }
        public async Task<IEnumerable<T>> GetAllIncludingAsync(DnDWorldContext context, Func<IQueryable<T>,IQueryable<T>> includedProperties)
        {
            IQueryable<T> query = context.Set<T>();

            if (includedProperties != null)
            {
                query = includedProperties(query);
            }

            return await query.ToListAsync();
        }
    }
}
