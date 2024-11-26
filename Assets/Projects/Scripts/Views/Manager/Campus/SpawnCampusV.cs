using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;
using Utilities;

public class SpawnCampusV : MonoBehaviour, ICampusViewSpawner
{
    #region -- Implements --

    /// <summary>
    /// Using addressable to create prefab
    /// </summary>
    /// <param name="campus">Data of campus</param>
    public void CreateCard(CampusD campus)
    {
        MainHandler.ClearSpawnedPrefabs();

        MainHandler.SpawnPrefabByLabel(_campusPrefab, _objectContain, (spawnedPrefab) =>
        {
            if (spawnedPrefab != null)
            {
                ICampusCardData cardData = GetComponentCard(spawnedPrefab);
                cardData.Initialize(campus);

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
            if (_setDataCampusView == null)
                _setDataCampusView = gameObject.AddComponent<SetDataCampusV>();
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
            if (_campusPrefab == null)
                _campusPrefab = new AssetLabelReference { labelString = "CampusManager" };

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
    private ICampusCardData GetComponentCard(GameObject cardPrefab)
    {
        return cardPrefab.GetComponent<ICampusCardData>();
    }

    private void FindPositionComponentCard()
    {
        try
        {
            Transform positionCampus = MainHandler.FindChildObjectsByTag(MainHandler.LastSpawnedPrefab.transform, _tagCampus);
            Transform positionRoom = MainHandler.FindChildObjectsByTag(MainHandler.LastSpawnedPrefab.transform, _tagRoom);

            if (positionCampus != null && positionRoom != null)
                _setDataCampusView.AddComponentFromPrefab(positionCampus, positionRoom);
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
    /// <param name="name">Campus name</param>
    /// <param name="room">Campus room</param>
    private void UpdateData(ICampusCardData campus)
    {
        _setDataCampusView.SetCampusData(campus);
    }
    #endregion

    #endregion

    #region -- Fields --

    private ICampusDataSetter _setDataCampusView;

    private GameObject _objectContain;

    [SerializeField] private AssetLabelReference _campusPrefab;
    private readonly string _tagCampus = "ValueCampus";
    private readonly string _tagRoom = "ValueRoom";

    private string _campusID;

    #endregion
}
