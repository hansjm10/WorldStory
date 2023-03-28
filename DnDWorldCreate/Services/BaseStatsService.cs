using System.Collections.Generic;
using System.Threading.Tasks;
using DnDWorldCreate.Data;
using DnDWorldCreate.Data.Entitys;
using DnDWorldCreate.Data.Stats;
using DnDWorldCreate.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DnDWorldCreate.Services
{
    public class BaseStatsService : IRepository<BaseStats>
    {
        private readonly DnDWorldContext _context;

        public BaseStatsService(DnDWorldContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<BaseStats>> GetAllAsync()
        {
            return await _context.BaseStats.ToListAsync();
        }

        public async Task<BaseStats?> GetByIdAsync(int id)
        {
            return await _context.BaseStats.FindAsync(id);
        }

        public async Task<BaseStats> AddAsync(BaseStats entity)
        {
            await _context.BaseStats.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<BaseStats> UpdateAsync(BaseStats entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var baseStats = await _context.BaseStats.FindAsync(id);
            if (baseStats == null) return false;

            _context.BaseStats.Remove(baseStats);
            await _context.SaveChangesAsync();
            return true;
        }

        public void Update(BaseStats entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Remove(BaseStats entity)
        {
            _context.BaseStats.Remove(entity);
        }
    }
}