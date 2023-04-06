using DnDWorldCreate.Services.Interfaces;
using DnDWorldCreate.Data.Entitys;
using DnDWorldCreate.Data;
using Microsoft.EntityFrameworkCore;

namespace DnDWorldCreate.Services
{
    public class ItemService
    {
        private readonly IRepository<Item> _itemRepository;
        private readonly IDbContextFactory<DnDWorldContext> _contextFactory;
        public ItemService(IRepository<Item> itemRepository, IDbContextFactory<DnDWorldContext> contextFactory)
        {
            _itemRepository = itemRepository;
            _contextFactory = contextFactory;
        }
        public async Task<Item> AddItemAsync(Item item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }
            using var context = _contextFactory.CreateDbContext();

            await _itemRepository.AddAsync(item, context);
            await _itemRepository.SaveChangesAsync(context);

            return item;
        }
        public async Task<Item> GetItemById(int id)
        {
            using var context = _contextFactory.CreateDbContext();

            return await _itemRepository.GetByIdAsync(id, context) ?? throw new ArgumentException($"Cannot find item: {id}");
        }
        public async Task<IEnumerable<Item>> GetAllItemsAsync()
        {
            using var context = _contextFactory.CreateDbContext();

            return await _itemRepository.GetAllAsync(context);
        }
        public async Task<IReadOnlyList<Item>> GetAllItemsReadOnlyAsync()
        {
            using var context = _contextFactory.CreateDbContext();

            var items = await _itemRepository.GetAllAsync(context);
            return items.ToList();
        }
        public async Task UpdateItemAsync(Item item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }
            using var context = _contextFactory.CreateDbContext();

            _itemRepository.Update(item, context);
            await _itemRepository.SaveChangesAsync(context);
        }
        public async Task DeleteItemAsync(int id)
        {
            using var context = _contextFactory.CreateDbContext();

            var item = await _itemRepository.GetByIdAsync(id,context);

            if (item == null)
            {
                throw new InvalidOperationException($"Item with ID {id} not found.");
            }

            _itemRepository.Remove(item, context);
            await _itemRepository.SaveChangesAsync(context);
        }
        public async Task<int> SaveChangesAsync()
        {
            using var context = _contextFactory.CreateDbContext();

            return await _itemRepository.SaveChangesAsync(context);
        }
    }
}