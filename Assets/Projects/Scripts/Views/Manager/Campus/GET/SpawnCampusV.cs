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
        ClearSpawnedPrefabs();

        var handle = Addressables.LoadAssetAsync<GameObject>("Assets/Addons/MyGUI/MyPrefab/PrefabTest/Card.prefab");
        handle.Completed += (AsyncOperationHandle<GameObject> task) =>
        {
            if (task.Status == AsyncOperationStatus.Succeeded)
            {
                // Create Card clone (Prefab) and stick it in Search Card Parent
                GameObject spawnedCard = Instantiate(task.Result, searchCardParent.transform);

                // Reset position and scale of card to display in correct layout
                spawnedCard.transform.localPosition = Vector3.zero;
                spawnedCard.transform.localScale = Vector3.one;

                // Add to list
                _spawnedPrefabs.Add(spawnedCard);

                // Find UI components inside spawned prefab
                Transform positionName = spawnedCard.transform.Find("Campus/Value");
                Transform positionRoom = spawnedCard.transform.Find("Room/Value");
                _setDataCampus.AddComponentFromPrefab(positionName, positionRoom);

                UpdateData(campus.CampusName, campus.Room);

                Debug.Log("Card prefab đã được spawn thành công.");
            }
            else
            {
                Debug.LogError("Không thể load prefab!");
            }
        };
    }

    #endregion

    #region -- Methods --

    void Start()
    {
        GetCardParent();
        AddComponentSetData();
    }

    /// <summary>
    /// Delete all prefab has been spawned
    /// </summary>
    private void ClearSpawnedPrefabs()
    {
        foreach (var prefab in _spawnedPrefabs)
        {
            if (prefab != null)
            {
                Destroy(prefab);
            }
        }

        _spawnedPrefabs.Clear();
    }

    /// <summary>
    /// Find Transform of SearchCampus
    /// </summary>
    private void GetCardParent()
    {
        GameObject parent = GameObject.Find("/GUI/Monitor/Campus/SearchCampus/Body/SearchCard/Panel");

        if (parent != null)
        {
            searchCardParent = parent;
        }
        else
        {
            Debug.Log("Không tìm thấy Search Card!");
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

    private void UpdateData(string name, string room)
    {
        _setDataCampus.SetCampusName(name);
        _setDataCampus.SetCampusRoom(room);
    }

    #endregion

    #region -- Fields --

    public GameObject searchCardParent;

    private ISetDataCampusView _setDataCampus;

    private List<GameObject> _spawnedPrefabs = new List<GameObject>();

    #endregion
}
