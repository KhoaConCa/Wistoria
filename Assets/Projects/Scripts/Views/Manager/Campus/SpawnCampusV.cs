using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class SpawnCampusV : MonoBehaviour, ISpawnCampusView
{
    #region -- Implements --

    public void Initialization()
    {
        GetCardParent();
        AddComponentSetData();
    }

    public void CreateCard(CampusD campus)
    {
        var handle = Addressables.LoadAssetAsync<GameObject>("Assets/Addons/MyGUI/MyPrefab/PrefabTest/Card.prefab");
        handle.Completed += (AsyncOperationHandle<GameObject> task) =>
        {
            if (task.Status == AsyncOperationStatus.Succeeded)
            {
                // Tạo ra một Card clone và gán vào SearchCard parent
                GameObject spawnedCard = Instantiate(task.Result, searchCardParent.transform);

                // Reset vị trí và scale của card để nó hiển thị đúng trong layout
                spawnedCard.transform.localPosition = Vector3.zero;
                spawnedCard.transform.localScale = Vector3.one;

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
        Initialization();
    }

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

    #endregion
}
