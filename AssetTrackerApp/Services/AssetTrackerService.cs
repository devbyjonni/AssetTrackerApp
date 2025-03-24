using AssetTrackerApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AssetTrackerApp.Services
{
    public class AssetTrackerService
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
