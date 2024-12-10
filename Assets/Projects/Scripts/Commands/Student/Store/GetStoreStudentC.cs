using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Utilities;

public class GetStoreStudentC : MonoBehaviour, IGetStoreCommand
{
    #region -- Implements --

    /// <summary>
    /// Handles the response from the server when store information is found.
    /// Logs the details of the store if it exists
    /// </summary>
    /// <param name="store">The store object returned from the server</param>
    public void OnStoreFound(StoreD store)
    {
        if (store != null)
        {
            _spawnStoreView.CreateCard(store);
            if (!storeDs.Keys.Contains(store.Id))
                storeDs[store.Id] = store;
        }
        else
            Debug.Log("Store not found.");
    }

    #endregion

    #region -- Methods --

    private void Awake()
    {
        AddComponentStoreHandler();
        AddComponetStoreView();
    }

    private void OnEnable()
    {
        try
        {
            MainHandler.ClearSpawnedPrefabs();
            if (MainHandler.PrefabList.Count <= 0)
                StartCoroutine(_storeHandler.GetAllStore(OnStoreFound, MainView.OnSuccess, MainView.OnFailed));
        }
        catch (Exception e)
        {
            Debug.LogWarning(e.Message);
        }
    }

    #region -- Add Components --
    private void AddComponetStoreView()
    {
        if (_spawnStoreView == null)
            _spawnStoreView = gameObject.AddComponent<SpawnStoreStudentV>();
        else
            Debug.Log("The SpawnStoreStudentV component already exists");
    }

    private void AddComponentStoreHandler()
    {
        if (_storeHandler == null)
            _storeHandler = gameObject.AddComponent<GetStoreStudentH>();
        else
            Debug.Log("The GetStoreStudentH component already exists");
    }
    #endregion

    #endregion

    #region -- Fields --

    private IGetStoreHandler _storeHandler;
    private IStoreViewSpawner _spawnStoreView;

    private Dictionary<string, StoreD> storeDs = new Dictionary<string, StoreD>();

    #endregion
}
