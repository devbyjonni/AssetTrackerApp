using AssetTrackerApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AssetTrackerApp.Data
{
    /// The AssetRepository class is responsible for storing and retrieving asset data.
    /// It acts as an in-memory data layer, providing basic operations such as adding and
    /// retrieving assets, as well as sorted views based on asset type or office location.
    public class AssetRepository
    {
        private readonly List<Asset> _assets = new();

        // Add a new asset to the tracker
        public void AddAsset(Asset asset)
        {
            _assets.Add(asset);
        }

        // Return all assets
        public List<Asset> GetAllAssets()
        {
            return _assets;
        }

        // Return assets sorted by type (Computer > Smartphone), then by purchase date
        public List<Asset> GetSortedAssetsByTypeAndDate()
        {
            return _assets
                .OrderBy(a => a.Type)         // Alphabetical: Computer before Smartphone
                .ThenBy(a => a.PurchaseDate) // Oldest first
                .ToList();
        }

        // Return assets sorted by Office name, then by purchase date
        public List<Asset> GetSortedAssetsByOfficeAndDate()
        {
            return _assets
                .OrderBy(a => a.Office.Name)
                .ThenBy(a => a.PurchaseDate)
                .ToList();
        }
    }
}
