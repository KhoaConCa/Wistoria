using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

#region -- Class Description --
/// <summary>
/// Command class responsible for fetching and displaying package data.
/// Initializes required components, starts package retrieval, and handles found packages.
/// </summary>
#endregion
public class GetPackageC : MonoBehaviour, IGetPackageCommand
{
    #region -- Unity Methods --

    /// <summary>
    /// Unity's Start method.
    /// Adds required components and initiates the coroutine to get all packages.
    /// </summary>
    void Start()
    {
        AddComponentPackageHandler();
        AddComponentPackageView();
        StartCoroutine(_packageHandler.GetAllPackage(OnPackageFound));
    }

    #endregion

    #region -- Package Handling Methods --

    /// <summary>
    /// Callback executed when a package is found.
    /// Displays package information in the console and creates a package card in the view.
    /// </summary>
    /// <param name="package">The package data found by the handler.</param>
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

    #endregion

    #region -- Add Components --

    /// <summary>
    /// Adds the SpawnPackageV component if it has not already been added.
    /// </summary>
    void AddComponentPackageView()
    {
        if (_spawnPackageView == null)
        {
            _spawnPackageView = gameObject.AddComponent<SpawnPackageV>();
        }
        else
        {
            Debug.Log("SpawnPackageV component already exists.");
        }
    }

    /// <summary>
    /// Adds the GetPackageH component if it has not already been added.
    /// </summary>
    void AddComponentPackageHandler()
    {
        if (_packageHandler == null)
        {
            _packageHandler = gameObject.AddComponent<GetPackageH>();
        }
        else
        {
            Debug.Log("GetPackageH component already exists.");
        }
    }

    #endregion

    #region -- Fields --

    private IGetPackageHandler _packageHandler;
    private ISpawnPackageView _spawnPackageView;

    #endregion
}
