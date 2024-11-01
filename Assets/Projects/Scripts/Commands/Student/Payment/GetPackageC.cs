using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;
using TMPro;

public class GetPackageC : MonoBehaviour , IGetPackageCommand
{
    void Start()
    {
        AddComponentPackageHandler();
        AddComponentPackageView();
        StartCoroutine(_packageHandler.GetAllPackage(OnPackageFound));
    }

    public void OnPackageFound(PackageD package)
    {
        if (package != null)
        {
            Debug.Log($"Found paper: {package.Paper}, price: {package.Price}");
            _spawnPackageView.CreateCard(package);
        }
        else
        {
            Debug.Log("Package not found.");
        }
    }

    #region -- Add Components --

    void AddComponentPackageView()
    {
        if (_spawnPackageView == null)
        {
            _spawnPackageView = gameObject.AddComponent<SpawnPackageV>();
        }
        else
        {
            Debug.Log("Đã tồn tại component SpawnPackageV");
        }
    }

    void AddComponentPackageHandler()
    {
        if (_packageHandler == null)
        {
            _packageHandler = gameObject.AddComponent<GetPackageH>();
        }
        else
        {
            Debug.Log("Đã tồn tại component GetPackageH");
        }
    }

    #endregion

    #region -- Fields --

    private IGetPackageHandler _packageHandler;
    private ISpawnPackageView _spawnPackageView;

    #endregion
}
