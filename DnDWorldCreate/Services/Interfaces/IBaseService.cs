using DnDWorldCreate.Data.Entitys;

namespace DnDWorldCreate.Services.Interfaces
{
    public interface IBaseService<T> where T : BaseEntity
    {
        public Task<T> AddAsync(T entity);
        public Task<T> GetByIdAsync(int id);
        public Task<IEnumerable<T>> GetAllAsync();
        public Task<IReadOnlyList<T>> GetAllReadOnlyAsync();
        public Task UpdateAsync(T entity);
        public Task DeleteAsync(int id);
        public Task<int> SaveChangesAsync();
    }
}
