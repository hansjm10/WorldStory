using DnDWorldCreate.Data.Entitys;
using Microsoft.EntityFrameworkCore;

namespace DnDWorldCreate.Services.Interfaces
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T?> GetByIdAsync(int id);
        Task<T> AddAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task<bool> DeleteAsync(int id);
        void Update(T entity);
        Task<int> SaveChangesAsync();
        void Remove(T entity);
    }
}
