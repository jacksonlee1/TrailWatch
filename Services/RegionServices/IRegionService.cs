using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Models.RegionModels;

namespace Services.RegionServices
{
    public interface IRegionService
    {
        Task<bool> AddRegionAsync(RegionCreate trail);
        Task<IEnumerable<RegionListItem?>> GetAllRegionsAsync();
        Task<RegionDetail?> GetRegionByIdAsync(int id);
        Task<bool> UpdateRegionAsync(RegionUpdate update);
        Task<bool> DeleteRegionByIdAsync(int id);
    }
}