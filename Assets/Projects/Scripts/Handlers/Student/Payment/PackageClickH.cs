using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Utilities;
using System.Collections;

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

        _packageData = this.gameObject.GetComponent<PackageCardData>();

        MomoD momo = new MomoD();
        momo.Amount = int.Parse(_packageData.Price);

        _amount = int.Parse(_packageData.Paper);

        StartCoroutine(_momoHandler.CreateMOMOPayment(momo, onSuccess =>
        {
            _orderId = onSuccess.OrderID;
            _isWaitingForCallback = true;
            Application.OpenURL(onSuccess.PayURL);
            
        }, MainView.OnFailed));

        Debug.Log($"Package clicked! Paper: {_packageData.Paper}, Price: {_packageData.Price}");
        Debug.Log(_currentPaper);
/*        PaymentD payment = new PaymentD
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
        }));*/
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

        StartCoroutine(_studentPaper.GetStudentPaper(MainUser.STUDENT_ID, onSuccess =>
        {
            _currentPaper = onSuccess;
/*            _newPaper = int.Parse(_packageData.Paper) + _currentPaper;*/
        }, MainView.OnFailed));

        //StartCoroutine(_studentPaper.UpdateStudentPaper(MainUser.STUDENT_ID, _newPaper, MainView.OnSuccess, MainView.OnFailed));
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
        _momoHandler = gameObject.AddComponent<MomoH>();
        _studentPaper = gameObject.AddComponent<StudentUpdater>();
    }

    private void OnApplicationFocus(bool hasFocus)
    {
        if (hasFocus && _isWaitingForCallback)
        {
            StartCoroutine(_momoHandler.GetCallback(_orderId, onSuccess =>
            {
                _resaultCode = onSuccess;
                StartCoroutine(_studentPaper.GetStudentPaper(MainUser.STUDENT_ID, onSuccess =>
                {
                    _currentPaper = onSuccess;
                }, MainView.OnFailed));

                if (_resaultCode == 0)
                {
                    _newPaper = int.Parse(_packageData.Paper) + _currentPaper;
                    StartCoroutine(_studentPaper.UpdateStudentPaper(MainUser.STUDENT_ID, _newPaper, MainView.OnSuccess, MainView.OnFailed));
                }
            }, MainView.OnFailed));
            
            _isWaitingForCallback = false;
            StartCoroutine(_momoHandler.DeleteCallback(_orderId, MainView.OnSuccess, MainView.OnFailed));
        }
    }

    public PackageD package;
    public Button clickPackage;

    private IPackageData _packageData;
    private IPaymentProcessor _paymentProcessor;
    private IStudentUpdater _studentUpdater;
    private IMomoHandler _momoHandler;
    private IStudentPaper _studentPaper;

    private bool _isWaitingForCallback = false;
    private string _orderId;
    private int _resaultCode;
    private int _currentPaper;
    private int _newPaper;
    private int _amount;
}
