using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.AddressableAssets;

/// <summary>
/// Singleton class that manages the loading and releasing of addressable assets.
/// This class ensures that assets are only loaded once and are reused to optimize performance
/// </summary>
public class Addressable : Singleton<Addressable>
{
    #region -- Methods --

    /// <summary>
    /// Creates or retrieves an asset using the Addressables system.
    /// If the asset is already loaded, it is retrieved from the cache; otherwise, it is loaded asynchronously
    /// </summary>
    /// <typeparam name="T">The type of the asset to load</typeparam>
    /// <param name="key">The unique key used to identify the asset in the Addressables system</param>
    /// <param name="onComplete">Action to be invoked when the asset is successfully loaded</param>
    /// <param name="onFailed">Optional action to be invoked if the asset fails to load</param>
    public void CreateAsset<T>(string key, Action<T> onComplete, Action onFailed = null)
    {
        if (_assetDictionary.ContainsKey(key))
        {
            onComplete?.Invoke((T)(_assetDictionary[key].Result));
        }
        else
        {
            StartCoroutine(LoadAsset(key, onComplete, onFailed));
        }
    }

    /// <summary>
    /// Loads an asset asynchronously using the Addressables system.
    /// This coroutine handles the loading process and stores the asset in the cache if successful
    /// </summary>
    /// <typeparam name="T">The type of the asset to load</typeparam>
    /// <param name="key">The unique key used to identify the asset</param>
    /// <param name="onComplete">Action to be invoked when the asset is successfully loaded</param>
    /// <param name="onFailed">Optional action to be invoked if the asset fails to load</param>
    /// <returns>An IEnumerator to support coroutine-based loading</returns>
    private IEnumerator LoadAsset<T>(string key, Action<T> onComplete, Action onFailed = null)
    {
        var opHandle = Addressables.LoadAssetAsync<T>(key);
        yield return opHandle;

        if (opHandle.Status == AsyncOperationStatus.Succeeded)
        {
            onComplete?.Invoke(opHandle.Result);

            if (_assetDictionary.ContainsKey(key))
            {
                RemoveAsset(key);
            }

            _assetDictionary[key] = opHandle;
        }
    }

    /// <summary>
    /// Removes an asset from the cache and releases its memory.
    /// This should be called when the asset is no longer needed to avoid memory leaks
    /// </summary>
    /// <param name="key">The unique key identifying the asset to be removed</param>
    public void RemoveAsset(string key)
    {
        Addressables.Release(_assetDictionary[key]);
        _assetDictionary.Remove(key);
    }

    #endregion

    #region -- Fields --

    /// <summary>
    /// A dictionary that stores loaded assets with their corresponding keys.
    /// This allows quick retrieval of assets without reloading them from Addressables
    /// </summary>
    private readonly Dictionary<string, AsyncOperationHandle> _assetDictionary = new();

    #endregion
}
