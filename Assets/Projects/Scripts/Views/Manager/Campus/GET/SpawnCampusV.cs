using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;
using Utilities;

public class SpawnCampusV : MonoBehaviour, ISpawnCampusView
{
    #region -- Implements --

    /// <summary>
    /// Using addressable to create prefab
    /// </summary>
    /// <param name="campus">Data of campus</param>
    public void CreateCard(CampusD campus)
    {
        MainHandler.ClearSpawnedPrefabs();

        MainHandler.LoadAndSpawnPrefab(_address, _path, (spawnedPrefab) =>
        {
            if (spawnedPrefab != null)
            {
                Debug.Log("Prefab spawned successfully.");

                // Add button component for ModifyCampusC
                _modifyCampusCommand.SetupButton(spawnedPrefab);

                FindComponentUI(_campusName, _campusRoom);
                UpdateData(campus.CampusName, campus.Room);
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
        AddComponentModifyCampus();
        AddComponentSetData();
    }

    private void FindComponentUI(string name, string room)
    {
        Transform positionName = MainHandler.LastSpawnedPrefab?.transform.Find(name);
        Transform positionRoom = MainHandler.LastSpawnedPrefab?.transform.Find(room);

        if (positionName != null && positionRoom != null)
        {
            _setDataCampusView.AddComponentFromPrefab(positionName, positionRoom);
        }
        else
        {
            Debug.LogError("UI components not found in prefab!");
        }
    }

    private void AddComponentSetData()
    {
        if (_setDataCampusView == null)
        {
            _setDataCampusView = gameObject.AddComponent<SetDataCampusV>();
        }
        else
        {
            Debug.Log("The SetDataCampusV component already exists");
        }
    }

    private void AddComponentModifyCampus()
    {
        if (_modifyCampusCommand == null)
        {
            _modifyCampusCommand = gameObject.AddComponent<ModifyCampusC>();
        }
        else
        {
            Debug.Log("The ModifyCampusC component already exists");
        }
    }

    private void UpdateData(string name, string room)
    {
        _setDataCampusView.SetCampusName(name);
        _setDataCampusView.SetCampusRoom(room);
    }

    #endregion

    #region -- Fields --

    private ISetDataCampusView _setDataCampusView;
    private IModifyCampusCommand _modifyCampusCommand;

    private readonly string _address = "Assets/Addons/MyGUI/MyPrefab/PrefabTest/Card.prefab";
    private readonly string _path = "/GUI/Monitor/Campus/SearchCampus/Body/SearchCard/Panel";
    private readonly string _campusName = "Campus/Value";
    private readonly string _campusRoom = "Room/Value";

    #endregion
}
