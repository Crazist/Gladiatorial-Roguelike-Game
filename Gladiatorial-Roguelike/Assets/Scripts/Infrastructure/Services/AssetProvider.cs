using System.Collections.Generic;
using UnityEngine;

namespace Infrastructure.Services
{
    public class AssetProvider
    {
        public T InstantiateAsset<T>(string path, Transform parent = null) where T : Object
        {
            T asset = LoadAsset<T>(path);

            if (asset == null)
            {
                Debug.LogError($"Failed to instantiate asset at path {path} because it could not be loaded.");
                return null;
            }

            T instantiatedAsset = Object.Instantiate(asset, parent);
            return instantiatedAsset;
        }

        public T[] LoadAllAssets<T>(string path) where T : Object
        {
            T[] assets = Resources.LoadAll<T>(path);
            if (assets == null || assets.Length == 0)
            {
                Debug.LogError($"Assets at path {path} could not be loaded.");
                return null;
            }

            return assets;
        }

        public T LoadAsset<T>(string path) where T : Object
        {
            T asset = Resources.Load<T>(path);

            if (asset == null)
            {
                Debug.LogError($"Asset at path {path} could not be loaded.");
                return null;
            }

            return asset;
        }
    }
}