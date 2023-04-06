using DnDWorldCreate.Data;
using DnDWorldCreate.Data.Entitys;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DnDWorldCreate.Services.Interfaces
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task<IEnumerable<T>> GetAllAsync(DnDWorldContext context);
        Task<T?> GetByIdAsync(int id, DnDWorldContext context);
        Task<T> AddAsync(T entity, DnDWorldContext context);
        Task<T> UpdateAsync(T entity, DnDWorldContext context);
        Task<bool> DeleteAsync(int id, DnDWorldContext context);
        void Update(T entity, DnDWorldContext context);
        Task<int> SaveChangesAsync(DnDWorldContext context);
        void Remove(T entity, DnDWorldContext context);
        Task UpdateWithRelatedAsync(T entity, DnDWorldContext context, params Expression<Func<T, object>>[] properties);
        Task<IEnumerable<T>> GetAllIncludingAsync(DnDWorldContext context, Func<IQueryable<T>, IQueryable<T>> includedProperties);
    }
}
