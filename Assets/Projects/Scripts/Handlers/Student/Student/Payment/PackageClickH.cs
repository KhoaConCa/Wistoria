using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PackageClickH: MonoBehaviour , IPackageClickH
{
    /// <summary>
    /// Switch form, transfer data when the campus card was clicked
    /// </summary>
    public void ClickPackage()
    {
        if (package == null)
        {
            Debug.LogWarning("Package data is null. Cannot proceed with ClickCard.");
        }
        Debug.Log($"Package clicked! Paper: {_packageData.Paper}, Price: {_packageData.Price}");
    }

    /// <summary>
    /// Set event for prefab
    /// </summary>
    public void SetUpButton()
    {
        clickPackage = gameObject.GetComponent<Button>();
        _packageData = gameObject.GetComponent<PackageCardData>();
    }



    void Start()
    {
        GetComponentData();

        clickPackage.onClick.AddListener(ClickPackage);

        SetUpButton();
    }

    private void GetComponentData()
    {
        if (_packageData == null)
        {
            _packageData = gameObject.GetComponent<PackageCardData>();
        }
        else
        {
            Debug.Log("The ModifyCampusH component already exiests");
        }
    }

    /// <summary>
    /// Assigns the package data and updates the UI elements.
    /// </summary>
    /// <param name="package">The package data to assign.</param>

    public PackageD package;
    public Button clickPackage;

    private IPackageData _packageData;

}
