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
        SpawnCardPrefab();
    }

    public void CreateCards(List<CampusD> campuses)
    {

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

    public void SpawnCardPrefab()
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
                campusName = spawnedCard.transform.Find("Campus/Value").GetComponent<TextMeshProUGUI>();
                campusRoom = spawnedCard.transform.Find("Room/Value").GetComponent<TextMeshProUGUI>();
                Debug.Log("Card prefab đã được spawn thành công.");
            }
            else
            {
                Debug.LogError("Không thể load prefab!");
            }
        };
    }

    #endregion

    #region -- Fields --

    public GameObject searchCardParent;

    public TextMeshProUGUI campusName;
    public TextMeshProUGUI campusRoom;

    #endregion
}
