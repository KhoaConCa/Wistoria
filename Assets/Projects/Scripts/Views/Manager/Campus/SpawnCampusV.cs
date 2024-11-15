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
                ICampusCardData _campuscardData = spawnedPrefab.GetComponent<CampusCardData>();
                _campuscardData.Initialize(campus._id, campus.CampusName, campus.Room);

                FindComponentUI();
                UpdateData(campus);
            }
            else
            {
                Debug.LogError("Failed to spawn prefab!");
            }
        });
    }

    #endregion

    #region -- Methods --

    void Start()
    {
        AddComponentDefault();
        GetComponentDefault();
    }

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

    private void GetComponentDefault()
    {
        try
        {
            if (_campusPrefab == null)
                _campusPrefab = new AssetLabelReference { labelString = "Campus" };

            if (_objectContain == null)
                _objectContain = GameObject.FindWithTag("ObjectContain");
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
        }
    }

    /// <summary>
    /// Find root to set component for prefab
    /// </summary>
    /// <param name="name">Address campus name</param>
    /// <param name="room">Address campus room</param>
    private void FindComponentUI(string name, string room)
    {
        try
        {
            Transform positionCampus = MainHandler.LastSpawnedPrefab?.transform.Find(name);
            Transform positionRoom = MainHandler.LastSpawnedPrefab?.transform.Find(room);

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

    private void FindComponentUI()
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
    private void UpdateData(CampusD campus)
    {
        _setDataCampusView.SetCampusName(campus.CampusName);
        _setDataCampusView.SetCampusRoom(campus.Room);
    }

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
