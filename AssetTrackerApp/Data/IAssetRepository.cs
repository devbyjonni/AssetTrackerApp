using AssetTrackerApp.Models;
using System.Collections.Generic;

namespace AssetTrackerApp.Data
{
    /// <summary>
    /// Interface for asset repository implementations.
    /// Provides methods for storing and retrieving assets.
    /// </summary>
    public interface IAssetRepository
    {
        void AddAsset(Asset asset);
        List<Asset> GetAllAssets();
        List<Asset> GetSortedAssetsByTypeAndDate();
        List<Asset> GetSortedAssetsByOfficeAndDate();
    }
}
