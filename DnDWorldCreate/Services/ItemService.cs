using DnDWorldCreate.Services.Interfaces;
using DnDWorldCreate.Data;
namespace DnDWorldCreate.Services
{
    public class ItemService
    {
        private readonly IRepository<Item> _itemRepository;
        public ItemService(IRepository<Item> itemRepository)
        {
            _itemRepository = itemRepository;
        }
        public async Task<Item> AddItemAsync(Item item)
        {
            if(item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }
            await _itemRepository.AddAsync(item);
            await _itemRepository.SaveChangesAsync();

            return item;
        }
        public async Task<Item> GetItemById(int id)
        {
            var item = await _itemRepository.GetByIdAsync(id) ?? throw new ArgumentException($"Cannot find item: {id}");
            return item;
;
        }
        public async Task<IEnumerable<Item>> GetAllItemsAsync()
        {
            return await _itemRepository.GetAllAsync();
        }
        public async Task<IReadOnlyList<Item>> GetAllItemsReadOnlyAsync()
        {
            var items = await _itemRepository.GetAllAsync();
            return items.ToList();
        }
        public async Task UpdateItemAsync(Item item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }
            _itemRepository.Update(item);
            await _itemRepository.SaveChangesAsync();
        }
        public async Task DeleteItemAsync(int id)
        {
            var item = await _itemRepository.GetByIdAsync(id);

            if(item == null)
            {
                throw new InvalidOperationException($"Item with ID {id} not found.");
            }

            _itemRepository.Remove(item);
            await _itemRepository.SaveChangesAsync();
        }
        public async Task<int> SaveChangesAsync()
        {
            return await _itemRepository.SaveChangesAsync();
        }
        
    }
}
