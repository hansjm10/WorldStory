using DnDWorldCreate.Data;
using DnDWorldCreate.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DnDWorldCreate.Services
{
    public class TownService
    {
        private readonly ITownRepository _townRepository;
        public TownService(ITownRepository townRepository)
        {
            _townRepository = townRepository;
        }
        public async Task<Town> AddTownAsync(Town town)
        {
            if (town == null)
            {
                throw new ArgumentNullException(nameof(town));
            }

            await _townRepository.AddAsync(town);
            await _townRepository.SaveChangesAsync();

            return town;
        }
        public async Task<Town> GetTownByIdAsync(int id)
        {
            return await _townRepository.GetByIdAsync(id);
        }
        public async Task<IEnumerable<Town>> GetAllTownsAsync()
        {
            return await _townRepository.GetAllAsync();
        }
        public async Task UpdateTownAsync(Town town)
        {
            if (town == null)
            {
                throw new ArgumentNullException(nameof(town));
            }

            _townRepository.Update(town);
            await _townRepository.SaveChangesAsync();
        }
        public async Task DeleteTownAsync(int id)
        {
            var town = await _townRepository.GetByIdAsync(id);

            if (town == null)
            {
                throw new InvalidOperationException($"Town with ID {id} not found.");
            }

            _townRepository.Remove(town);
            await _townRepository.SaveChangesAsync();
        }
        public async Task<int> SaveChangesAsync()
        {
            return await _townRepository.SaveChangesAsync();
        }
        public async Task<IEnumerable<Town>> GetTownsByRegionIdAsync(int regionId)
        {
            return await _townRepository.GetTownsByRegionIdAsync(regionId);
        }

    }
}
