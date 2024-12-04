using UnityEngine;
using Utilities;

public class GetPackageC : MonoBehaviour, IGetPackageCommand
{
    #region -- Implements --

    /// <summary>
    /// Adds the SpawnPackageV component if it has not already been added.
    /// </summary>
    public void AddComponentPackageView()
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
    public void AddComponentPackageHandler()
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

    #region -- Methods --

    /// <summary>
    /// Unity's Start method.
    /// Adds required components and initiates the coroutine to get all packages.
    /// </summary>
    private void Awake()
    {
        AddComponentPackageHandler();
        AddComponentPackageView();
    }

    private void OnEnable()
    {
        StartCoroutine(_packageHandler.GetAllPackage(OnPackageFound,
            OnFetchSuccess,
            OnFetchFailed));
    }

    private void OnDisable()
    {
        MainHandler.ClearSpawnedPrefabs();
    }

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
    /// <summary>
    /// Callback executed when fetching printers succeeds.
    /// </summary>
    /// <param name="message">Success message.</param>
    private void OnFetchSuccess(string message)
    {
        Debug.Log($"Fetch successful: {message}");
    }

    /// <summary>
    /// Callback executed when fetching printers fails.
    /// </summary>
    /// <param name="error">Error message.</param>
    private void OnFetchFailed(string error)
    {
        Debug.LogError($"Fetch failed: {error}");
    }

    #endregion

    #region -- Fields --

    private IGetPackageHandler _packageHandler;
    private ISpawnPackageView _spawnPackageView;

    #endregion
}
