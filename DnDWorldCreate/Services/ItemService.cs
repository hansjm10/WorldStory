using DnDWorldCreate.Services.Interfaces;
using DnDWorldCreate.Data.Entitys;
using DnDWorldCreate.Data;
using Microsoft.EntityFrameworkCore;

namespace DnDWorldCreate.Services
{
    public class ItemService : BaseService<Item>
    {
        private readonly IRepository<Item> _itemRepository;
        private readonly IDbContextFactory<DnDWorldContext> _contextFactory;
        public ItemService(IRepository<Item> itemRepository, IDbContextFactory<DnDWorldContext> contextFactory) : base(itemRepository, contextFactory)
        {
            _itemRepository = itemRepository;
            _contextFactory = contextFactory;
        }
    }
}