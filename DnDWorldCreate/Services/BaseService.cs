using DnDWorldCreate.Data;
using DnDWorldCreate.Data.Entitys;
using DnDWorldCreate.Pages;
using DnDWorldCreate.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace DnDWorldCreate.Services
{
    public class BaseService<T> : IBaseService<T> where T : BaseEntity
    {
        private readonly IRepository<T> _repository;
        private readonly IDbContextFactory<DnDWorldContext> _contextFactory;

        public BaseService(IRepository<T> repository, IDbContextFactory<DnDWorldContext> contextFactory)
        {
            _repository = repository;
            _contextFactory = contextFactory;
        }

        public async Task<T> AddAsync(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            using var context = _contextFactory.CreateDbContext();

            await _repository.AddAsync(entity, context);
            await _repository.SaveChangesAsync(context);

            return entity;
        }

        public async Task DeleteAsync(int id)
        {
            using var context = _contextFactory.CreateDbContext();

            var item = await _repository.GetByIdAsync(id, context);

            if (item == null)
            {
                throw new InvalidOperationException($"{typeof(T).Name} with ID {id} not found.");
            }

            _repository.Remove(item, context);
            await _repository.SaveChangesAsync(context);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            using var context = _contextFactory.CreateDbContext();

            return await _repository.GetAllAsync(context);
        }

        public async Task<IReadOnlyList<T>> GetAllReadOnlyAsync()
        {
            using var context = _contextFactory.CreateDbContext();

            var items = await _repository.GetAllAsync(context);
            return items.ToList();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            using var context = _contextFactory.CreateDbContext();

            return await _repository.GetByIdAsync(id, context) ?? throw new ArgumentException($"Cannot find {typeof(T).Name}: {id}");
        }

        public async Task<int> SaveChangesAsync()
        {
            using var context = _contextFactory.CreateDbContext();

            return await _repository.SaveChangesAsync(context);
        }

        public async Task UpdateAsync(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            using var context = _contextFactory.CreateDbContext();

            _repository.Update(entity, context);
            await _repository.SaveChangesAsync(context);
        }
    }
}
