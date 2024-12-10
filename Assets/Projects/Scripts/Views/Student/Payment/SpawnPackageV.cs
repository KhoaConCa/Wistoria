using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;
using Utilities;

public class SpawnPackageV : MonoBehaviour , ISpawnPackageView
{
    #region -- Implements --

    /// <summary>
    /// Using addressable to create prefab
    /// </summary>
    /// <param name="package">Data of package</param>
    public void CreateCard(PackageD package)
    {
        _objectContain = GameObject.FindWithTag("ObjectContain");
        MainHandler.SpawnPrefabByLabel(_packagePrefab, _objectContain, (spawnedPrefab) =>
        {
            try
            {
                if (spawnedPrefab != null)
                {
                    var packageCardData = spawnedPrefab.GetComponent<PackageCardData>();
                    if (packageCardData != null)
                    {
                        packageCardData.Initialize(package.Paper, package.Price);
                    }
                    else
                    {
                        Debug.LogError("PackageCardData component is missing on the prefab. Please ensure the component is attached.");
                    }

                    FindComponentUI();
                    UpdateData(package.Paper, package.Price);
                }
                else
                {
                    Debug.LogError("Failed to spawn prefab!");
                }
            }
            catch (Exception e)
            {
                Debug.LogError(e.Message);
            }
        });
    }


    #endregion

    #region -- Methods --

    void Awake()
    {
        GetObject();

        AddComponentSetData();
    }

    private void FindComponentUI()
    {
        Transform positionPaper = MainHandler.FindChildObjectsByTag(MainHandler.LastSpawnedPrefab.transform, _packagePaper);
        Transform positionPrice = MainHandler.FindChildObjectsByTag(MainHandler.LastSpawnedPrefab.transform, _packagePrice);

        if (positionPaper != null && positionPrice != null)
        {
            _setDataPackageView.AddComponentFromPrefab(positionPrice, positionPaper);
        }
        else
        {
            Debug.LogError("UI components not found in prefab!");
        }
    }

    private void GetObject()
    {
        try
        {
            if (_packagePrefab == null)
                _packagePrefab = new AssetLabelReference { labelString = "Package" };
        }
        catch (Exception e)
        {
            Debug.LogError(e.Message);
        }
    }

    private void AddComponentSetData()
    {
        if (_setDataPackageView == null)
        {
            _setDataPackageView = gameObject.AddComponent<SetDataPackageV>();
        }
        else
        {
            Debug.Log("The SetDataCampusV component already exists");
        }
    }


    private void UpdateData(string paper, string price)
    {
        _setDataPackageView.SetPackagePaper(paper);
        _setDataPackageView.SetPackagePrice(price);
    }

    #endregion

    #region -- Fields --

    private ISetDataPackageView _setDataPackageView;

    [SerializeField] private AssetLabelReference _packagePrefab;

    private GameObject _objectContain;

    private readonly string _packagePaper = "PackageName";
    private readonly string _packagePrice = "PackageValue"; 

    #endregion
}
