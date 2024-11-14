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

        PaymentD payment = new PaymentD
        {
            Paper = _packageData.Paper,
            Person = "671860901e0844975517030e", // Replace with the current student's ID
            Status = "Finished"
        };

        // Start the upload coroutine
        StartCoroutine(_paymentProcessor.UploadPaymentToMongoDB(payment));
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
        InitializeDependencies();

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
            Debug.Log("The PackageData component already exiests");
        }
    }

    private void InitializeDependencies()
    {
        // Add the PaymentProcessor component to handle uploads
        _paymentProcessor = gameObject.AddComponent<PaymentProcessor>();
    }

    /// <summary>
    /// Assigns the package data and updates the UI elements.
    /// </summary>
    /// <param name="package">The package data to assign.</param>

    public PackageD package;
    public Button clickPackage;

    private IPackageData _packageData;
    private IPaymentProcessor _paymentProcessor;

}
