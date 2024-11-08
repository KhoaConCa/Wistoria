using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Utilities;

public class CampusUIManager : MonoBehaviour, ICampusViewSpawner
{
    #region -- Implements --

    /// <summary>
    /// Using addressable to create prefab
    /// </summary>
    /// <param name="campus">Data of campus</param>
    public void CreateCard(CampusD campus)
    {
        _campusID = campus._id;

        MainHandler.ClearSpawnedPrefabs();

        MainHandler.SpawnPrefabByLabel(_campusPrefab, _path, (spawnedPrefab) =>
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
        _campusPrefab = new AssetLabelReference { labelString = "Campus" };

        AddComponentSetData();
    }

    /// <summary>
    /// Find root to set component for prefab
    /// </summary>
    /// <param name="name">Address campus name</param>
    /// <param name="room">Address campus room</param>
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

    /// <summary>
    /// Set data for prefab
    /// </summary>
    /// <param name="name">Campus name</param>
    /// <param name="room">Campus room</param>
    private void UpdateData(string name, string room)
    {
        _setDataCampusView.SetCampusName(name);
        _setDataCampusView.SetCampusRoom(room);
    }

    public void ShowCampusDetails()
    {
        if (!string.IsNullOrEmpty(_campusID))
        {
            CampusD campusDetails = CampusDataManager.Instance.GetCampusData(_campusID);

            if (campusDetails != null)
            {
                Debug.Log($"Campus Details: {campusDetails.CampusName}, Room: {campusDetails.Room}");
            }
            else
            {
                Debug.LogWarning("Campus details not found.");
            }
        }
    }


    #endregion

    #region -- Fields --

    private ICampusDataSetter _setDataCampusView;

    [SerializeField] private AssetLabelReference _campusPrefab;

    private readonly string _path = "/GUI/Monitor/Campus/SearchCampus/Body/SearchCard/Panel";
    private readonly string _campusName = "Campus/Value";
    private readonly string _campusRoom = "Room/Value";

    private string _campusID;

    #endregion
}
