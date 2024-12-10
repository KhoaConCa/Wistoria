using System;
using UnityEngine;
using UnityEngine.UI;
using Utilities;

public class PackageClickH : MonoBehaviour, IPackageClickH
{
    #region -- Implements --

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

        CreateMOMOPayment(momo);

        _amount = int.Parse(_packageData.Paper);
        _paperPaid = _packageData.Paper;

        GetPaperPackage(_paperPaid);
    }

    /// <summary>
    /// Sets up the button event listener.
    /// </summary>
    public void SetUpButton()
    {
        _clickPackage = gameObject.GetComponent<Button>();
        _packageData = gameObject.GetComponent<PackageCardData>();
    }

    #endregion

    #region -- Methods --

    private void Start()
    {
        GetComponentData();
        GetPackageH();
        InitializeDependencies();

        _clickPackage.onClick.AddListener(ClickPackage);

        SetUpButton();
        GetPaperStudent();
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

    private void GetPackageH()
    {
        if (_packagePrice == null)
        {
            GameObject store = GameObject.FindWithTag("StudentStore");
            _packagePrice = store.gameObject.GetComponent<GetPackageH>();
        }
        else
        {
            Debug.Log("The GetPackageH component already exists.");
        }
    }

    #region -- Initialize --
    private void InitializeDependencies()
    {
        _paymentProcessor = gameObject.AddComponent<PaymentProcessor>();
        _momoHandler = gameObject.AddComponent<MomoH>();
        _studentPaper = gameObject.AddComponent<StudentUpdater>();
        _paymentHandler = gameObject.AddComponent<CreatePaymentH>();
    }
    #endregion

    #region -- Focus On Application --
    private void OnApplicationFocus(bool hasFocus)
    {
        if (hasFocus && _isWaitingForCallback)
        {
            StartCoroutine(_momoHandler.GetCallback(_orderId, onSuccess =>
            {
                int resaultCode = onSuccess;
                MainView.OnSuccess(resaultCode.ToString());

                if (resaultCode == 0)
                {
                    int newPaper = _amount + _currentPaper;
                    ProcessDone(newPaper, _orderId);
                }
                else
                {
                    MainView.OnFailed("Thanh toán không thành công");
                }

            }, onFailed =>
            {
                MainView.OnFailed("Bạn chưa thanh toán");
            }));
            
            _isWaitingForCallback = false;
        }
    }
    #endregion

    private void OnPackageFound(PackageJsonD package)
    {
        PaymentJsonD payment = new PaymentJsonD();
        payment.PersonRaw = MainUser.STUDENT_ID;
        payment.Paper = package;
        payment.Status = "Success";
        payment.updateDate = DateTime.Now;

        StartCoroutine(_paymentHandler.UploadJson(payment, MainView.OnSuccess, MainView.OnFailed));
    }

    private void CreateMOMOPayment(MomoD momo)
    {
        StartCoroutine(_momoHandler.CreateMOMOPayment(momo, onSuccess =>
        {
            _orderId = onSuccess.OrderID;
            Debug.Log(_orderId);
            _isWaitingForCallback = true;
            Application.OpenURL(onSuccess.PayURL);

        }, MainView.OnFailed));
    }

    private void GetPaperPackage(string paperPaid)
    {
        StartCoroutine(_packagePrice.GetPackageByPaper(paperPaid, OnPackageFound, MainView.OnSuccess, MainView.OnFailed));
    }

    private void GetPaperStudent()
    {
        StartCoroutine(_studentPaper.GetStudentPaper(MainUser.STUDENT_ID, onSuccess =>
        {
            _currentPaper = onSuccess;
        }, MainView.OnFailed));
    }

    private void ProcessDone(int paper, string id)
    {
        StartCoroutine(_studentPaper.UpdateStudentPaper(MainUser.STUDENT_ID, paper, MainView.OnSuccess, MainView.OnFailed));

        StartCoroutine(_momoHandler.DeleteCallback(id, MainView.OnSuccess, MainView.OnFailed));
    }

    #endregion

    #region -- Fields --

    [SerializeField] private Button _clickPackage;

    private IPackageData _packageData;
    private IPaymentProcessor _paymentProcessor;
    private IMomoHandler _momoHandler;
    private IStudentPaper _studentPaper;
    private IGetPackageByPaper _packagePrice;
    private ICreatePaymentHandler _paymentHandler;

    private bool _isWaitingForCallback = false;
    private string _orderId;
    private int _currentPaper;
    private int _amount;
    private string _paperPaid;
    private string _packageId;

    #endregion
}
