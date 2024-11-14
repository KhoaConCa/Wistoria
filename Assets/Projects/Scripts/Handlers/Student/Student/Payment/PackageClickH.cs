using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PackageClickH : MonoBehaviour, IPackageClickH
{
    /// <summary>
    /// Handles the package click event.
    /// </summary>
    public void ClickPackage()
    {
        if (_packageData == null)
        {
            Debug.LogWarning("Package data is null. Cannot proceed with ClickPackage.");
            return;
        }

        Debug.Log($"Package clicked! Paper: {_packageData.Paper}, Price: {_packageData.Price}");

        PaymentD payment = new PaymentD
        {
            Paper = _packageData.Paper,
            Person = "671860901e0844975517030e", // Replace with the current student's ID
            Status = "Finished"
        };

        // Start the upload coroutine for payment
        StartCoroutine(_paymentProcessor.UploadPaymentToMongoDB(payment, () =>
        {
            // If payment succeeds, update the student's paper count
            int paperCount = int.Parse(_packageData.Paper);
            StartCoroutine(_studentUpdater.FetchAndIncrementPaper(payment.Person, paperCount));
        }));
    }

    /// <summary>
    /// Sets up the button event listener.
    /// </summary>
    public void SetUpButton()
    {
        clickPackage = gameObject.GetComponent<Button>();
        _packageData = gameObject.GetComponent<PackageCardData>();
    }

    private void Start()
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
            Debug.Log("The PackageData component already exists.");
        }
    }

    private void InitializeDependencies()
    {
        _paymentProcessor = gameObject.AddComponent<PaymentProcessor>();
        _studentUpdater = gameObject.AddComponent<StudentUpdater>();
    }

    public PackageD package;
    public Button clickPackage;

    private IPackageData _packageData;
    private IPaymentProcessor _paymentProcessor;
    private IStudentUpdater _studentUpdater;
}
