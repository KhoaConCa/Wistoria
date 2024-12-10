using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Utilities;

public class SpawnStoreStudentV : MonoBehaviour, IStoreViewSpawner
{
    #region -- Implements --

    /// <summary>
    /// Using addressable to create prefab
    /// </summary>
    /// <param name="store">Data of store</param>
    public void CreateCard(StoreD store)
    {
        MainHandler.ClearSpawnedPrefabs();

        MainHandler.SpawnPrefabByLabel(_storePrefab, _objectContain, (spawnedPrefab) =>
        {
            if (spawnedPrefab != null)
            {
                IStoreCardData cardData = GetComponentCard(spawnedPrefab);
                cardData.Initialize(store);

                FindPositionComponentCard();
                UpdateData(cardData);
            }
            else
            {
                Debug.LogError("Failed to spawn prefab!");
            }
        });
    }

    #endregion

    #region -- Methods --

    void Awake()
    {
        AddComponentDefault();
        GetComponentDefault();
    }

    #region -- Add Component --
    private void AddComponentDefault()
    {
        try
        {
            if (_setDataStoreView == null)
                _setDataStoreView = gameObject.AddComponent<SetDataStoreV>();
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
        }
    }
    #endregion

    #region -- Get Component --
    private void GetComponentDefault()
    {
        try
        {
            if (_storePrefab == null)
                _storePrefab = new AssetLabelReference { labelString = "Package" };

            if (_objectContain == null)
                _objectContain = GameObject.FindWithTag("ObjectContain");
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
        }
    }
    #endregion

    #region -- Set Up Card Prefab --
    private IStoreCardData GetComponentCard(GameObject cardPrefab)
    {
        return cardPrefab.GetComponent<IStoreCardData>();
    }

    private void FindPositionComponentCard()
    {
        try
        {
            Transform positionPaper = MainHandler.FindChildObjectsByTag(MainHandler.LastSpawnedPrefab.transform, _tagPaper);
            Transform positionPrice = MainHandler.FindChildObjectsByTag(MainHandler.LastSpawnedPrefab.transform, _tagPrice);

            if (positionPaper != null && positionPrice != null)
                _setDataStoreView.AddComponentFromPrefab(positionPaper, positionPrice);
            else
                Debug.LogError("UI components not found in prefab!");
        }
        catch (Exception e)
        {
            Debug.LogError(e.Message);
        }
    }

    /// <summary>
    /// Set data for prefab
    /// </summary>
    /// <param name="name">Store name</param>
    /// <param name="room">Store room</param>
    private void UpdateData(IStoreCardData store)
    {
        _setDataStoreView.SetStoreData(store);
    }
    #endregion

    #endregion

    #region -- Fields --

    private IStoreDataSetter _setDataStoreView;

    private GameObject _objectContain;

    [SerializeField] private AssetLabelReference _storePrefab;
    private readonly string _tagPaper = "ValuePaper";
    private readonly string _tagPrice = "ValuePrice";

    private string _storeID;

    #endregion
}
