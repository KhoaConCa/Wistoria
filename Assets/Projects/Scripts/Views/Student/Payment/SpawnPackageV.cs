using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
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
        MainHandler.ClearSpawnedPrefabs();

        MainHandler.LoadAndSpawnPrefab(_address, _path, (spawnedPrefab) =>
        {
            if (spawnedPrefab != null)
            {
                Debug.Log("Prefab spawned successfully.");

                // Add button component for ModifyCampusC
/*                _modifyCampusCommand.SetupButton(spawnedPrefab);
*/
                FindComponentUI(_packagePaper, _packagePrice);
                UpdateData(package.Paper, package.Price);
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
/*        AddComponentModifyCampus();
*/        AddComponentSetData();
    }

    private void FindComponentUI(string paper, string price)
    {
        Transform positionPaper = MainHandler.LastSpawnedPrefab?.transform.Find(paper);
        Transform positionPrice = MainHandler.LastSpawnedPrefab?.transform.Find(price);

        if (positionPaper != null && positionPrice != null)
        {
            _setDataPackageView.AddComponentFromPrefab(positionPrice, positionPaper);
        }
        else
        {
            Debug.LogError("UI components not found in prefab!");
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

/*    private void AddComponentModifyCampus()
    {
        if (_modifyCampusCommand == null)
        {
            _modifyCampusCommand = gameObject.AddComponent<ModifyCampusC>();
        }
        else
        {
            Debug.Log("The ModifyCampusC component already exists");
        }
    }*/

    private void UpdateData(string paper, string price)
    {
        _setDataPackageView.SetPackagePaper(paper);
        _setDataPackageView.SetPackagePrice(price);
    }

    #endregion

    #region -- Fields --

    private ISetDataPackageView _setDataPackageView;

    private readonly string _address = "Assets/Addons/MyGUI/MyPrefab/Button/Package.prefab";
    private readonly string _path = "/GUI/PGUI/PMiddle/PPayment/PPurchasePaper/PLayout/PItemList";
    private readonly string _packagePaper = "PInfo/PAddress/PValue/VPages";
    private readonly string _packagePrice = "PInfo/PLabel/LName"; 

    #endregion
}
