using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class SpawnPackageV : MonoBehaviour , ISpawnPaymentView
{
    #region -- Implements --

    public void CreateCard(PackageD package)
    {
        ClearSpawnedPrefabs();

        var handle = Addressables.LoadAssetAsync<GameObject>("Assets/Addons/MyGUI/MyPrefab/Button/Package.prefab");
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
                Transform positionPrice = spawnedCard.transform.Find("PLabel/LName");
                Transform positionPaper = spawnedCard.transform.Find("PValue/VPages");
                _setDataPackage.AddComponentFromPrefab(positionPrice, positionPaper);

                UpdateData(package.Price, package.Paper);

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
        GameObject parent = GameObject.Find("/GUI/PMiddle/PPayment/PPurchasePaper/PItemList");

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
        if (_setDataPackage == null)
        {
            _setDataPackage = gameObject.AddComponent<SetDataPackageV>();
        }
        else
        {
            Debug.Log("Đã tồn tại component GetCampusH");
        }
    }


    private void UpdateData(string price, string paper)
    {
        _setDataPackage.SetPackagePrice(price);
        _setDataPackage.SetPackagePaper(paper);
    }

    #endregion


    #region -- Fields --

    public GameObject searchCardParent;

    private ISetDataPackageView _setDataPackage;

    private List<GameObject> _spawnedPrefabs = new List<GameObject>();

    #endregion
}
