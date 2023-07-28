using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Models.TrailModels;

namespace Services.TrailServices
{
    public interface ITrailService
    {
        Task<bool> AddTrailAsync(TrailCreate trail);
        Task<IEnumerable<TrailListItem?>> GetAllTrailsAsync();
        Task<TrailDetail?> GetTrailByIdAsync(int? id);
        Task<IEnumerable<TrailListItem?>> GetTrailsByRegionIdAsync(int id);
        Task<bool> UpdateTrailAsync(TrailUpdate update);
        Task<bool> DeleteTrailByIdAsync(int id);
    }
}