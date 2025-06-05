using AssetTrackerApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AssetTrackerApp.Data
{
    /// <summary>
    /// In-memory repository for storing and retrieving assets.
    /// Provides basic operations such as adding assets and retrieving sorted views
    /// by type or office location.
    /// </summary>
    public class AssetRepository : IAssetRepository
    {
        private readonly List<Asset> _assets = new();

        /// <summary>
        /// Adds a new asset to the repository.
        /// </summary>
        /// <param name="asset">The asset to add.</param>
        public void AddAsset(Asset asset)
        {
            _assets.Add(asset);
        }

        /// <summary>
        /// Returns all stored assets.
        /// </summary>
        public List<Asset> GetAllAssets()
        {
            return new List<Asset>(_assets);
        }

        /// <summary>
        /// Returns assets sorted by type (Computer before Smartphone),
        /// then by purchase date (oldest first).
        /// </summary>
        public List<Asset> GetSortedAssetsByTypeAndDate()
        {
            return _assets
                .OrderBy(a => a.Type)
                .ThenBy(a => a.PurchaseDate)
                .ToList();
        }

        /// <summary>
        /// Returns assets sorted by office name,
        /// then by purchase date (oldest first).
        /// </summary>
        public List<Asset> GetSortedAssetsByOfficeAndDate()
        {
            return _assets
                .OrderBy(a => a.Office.Name)
                .ThenBy(a => a.PurchaseDate)
                .ToList();
        }
    }
}
