using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;
using Utilities;
using static SimpleFileBrowser.FileBrowser;

public class GetStoreC : MonoBehaviour, IGetStoreCommand
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
            if (MainHandler.PrefabList.Count <= 1)
                StartCoroutine(_storeHandler.GetAllStore(OnStoreFound, MainView.OnSuccess, MainView.OnFailed));

            ResetDataDropDown();
            SetDataDropDown();
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
            _spawnStoreView = gameObject.AddComponent<SpawnStoreV>();
        else
            Debug.Log("The SpawnStoreV component already exists");
    }

    private void AddComponentStoreHandler()
    {
        if (_storeHandler == null)
            _storeHandler = gameObject.AddComponent<GetStoreH>();
        else
            Debug.Log("The GetStoreH component already exists");
    }
    #endregion

    #region - Reset Data -
    private void ResetDataDropDown()
    {
        _priceDropDown.ClearOptions();
    }
    #endregion

    #region - Set Data -
    private void SetDataDropDown()
    {
        _priceDropDown.AddOptions(ConvertEnumToList());
    }

    private List<string> ConvertEnumToList()
    {
        List<string> descriptions = Enum.GetValues(typeof(PriceFilter))
                               .Cast<PriceFilter>()
                               .Select(e => EnumProperties.GetEnumDescription(e))
                               .ToList();
                               
        descriptions.Insert(0, "- Chọn bộ lọc -");

        return descriptions;
    }
    #endregion

    #region - Filter Data -
    public void FilterData()
    {
        MainHandler.ClearSpawnedPrefabs();

        int valueSelected = _priceDropDown.value;

        if (valueSelected == 0)
        {
            storeDs.Clear();
            StartCoroutine(_storeHandler.GetAllStore(OnStoreFound, MainView.OnSuccess, MainView.OnFailed));
            return;
        }    

        if (valueSelected == (int)PriceFilter.Descending)
            storeDs = storeDs.OrderByDescending(value => value.Value.Price)
                             .ToDictionary(value => value.Value.Id, value => value.Value);
        else
            storeDs = storeDs.OrderBy(value => value.Value.Price)
                             .ToDictionary(value => value.Value.Id, value => value.Value);

        ReCreateData();
    }

    private void ReCreateData()
    {
        foreach (var item in storeDs.Values)
        {
            Debug.Log(item.Price);
            OnStoreFound(item);
        }
    }
    #endregion

    #endregion

    #region -- Fields --

    private IGetStoreHandler _storeHandler;
    private IStoreViewSpawner _spawnStoreView;

    private Dictionary<string, StoreD> storeDs = new Dictionary<string, StoreD>();

    [SerializeField] private TMP_Dropdown _priceDropDown;

    #endregion
}