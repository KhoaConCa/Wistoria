using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

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
        AddComponentSetData();
        AddComponentModifyCampus();
    }

    private void FindComponentUI(string name, string room)
    {
        Transform positionName = MainHandler.LastSpawnedPrefab?.transform.Find(name);
        Transform positionRoom = MainHandler.LastSpawnedPrefab?.transform.Find(room);

        if (positionName != null && positionRoom != null)
        {
            _setDataCampus.AddComponentFromPrefab(positionName, positionRoom);
        }
        else
        {
            Debug.LogError("UI components not found in prefab!");
        }
    }

    private void AddComponentSetData()
    {
        if (_setDataCampus == null)
        {
            _setDataCampus = gameObject.AddComponent<SetDataCampusV>();
        }
        else
        {
            Debug.Log("Đã tồn tại component GetCampusH");
        }
    }

    private void AddComponentModifyCampus()
    {
        if (_modifyCampus == null)
        {
            _modifyCampus = gameObject.AddComponent<ModifyCampusC>();
        }
        else
        {
            Debug.Log("Đã tồn tại component GetCampusH");
        }
    }

    private void UpdateData(string name, string room)
    {
        _setDataCampus.SetCampusName(name);
        _setDataCampus.SetCampusRoom(room);
    }

    #endregion

    #region -- Fields --

    private ISetDataCampusView _setDataCampus;
    private IModifyCampusCommand _modifyCampus;

/*    private List<GameObject> _spawnedPrefabs = new List<GameObject>();*/

    private readonly string _address = "Assets/Addons/MyGUI/MyPrefab/PrefabTest/Card.prefab";
    private readonly string _path = "/GUI/Monitor/Campus/SearchCampus/Body/SearchCard/Panel";
    private string _campusName = "Campus/Value";
    private string _campusRoom = "Room/Value";

    #endregion
}
