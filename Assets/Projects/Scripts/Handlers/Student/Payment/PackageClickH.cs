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

        _amount = int.Parse(_packageData.Paper);

        StartCoroutine(_momoHandler.CreateMOMOPayment(momo, onSuccess =>
        {
            _orderId = onSuccess.OrderID;
            Debug.Log(_orderId);
            _isWaitingForCallback = true;
            Application.OpenURL(onSuccess.PayURL);
            
        }, MainView.OnFailed));

        Debug.Log($"Package clicked! Paper: {_packageData.Paper}, Price: {_packageData.Price}");

        _amount = int.Parse(_packageData.Paper);
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
        InitializeDependencies();

        _clickPackage.onClick.AddListener(ClickPackage);

        SetUpButton();

        StartCoroutine(_studentPaper.GetStudentPaper(MainUser.STUDENT_ID, onSuccess =>
        {
            _currentPaper = onSuccess;
        }, MainView.OnFailed));
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

    #region -- Initialize --
    private void InitializeDependencies()
    {
        _paymentProcessor = gameObject.AddComponent<PaymentProcessor>();
        _momoHandler = gameObject.AddComponent<MomoH>();
        _studentPaper = gameObject.AddComponent<StudentUpdater>();
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
                StartCoroutine(_studentPaper.GetStudentPaper(MainUser.STUDENT_ID, onSuccess =>
                {
                    _currentPaper = onSuccess;
                }, MainView.OnFailed));

                if (resaultCode == 0)
                {
                    int newPaper = _amount + _currentPaper;
                    StartCoroutine(_studentPaper.UpdateStudentPaper(MainUser.STUDENT_ID, newPaper, MainView.OnSuccess, MainView.OnFailed));

                    StartCoroutine(_momoHandler.DeleteCallback(_orderId, MainView.OnSuccess, MainView.OnFailed));
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

    #endregion

    #region -- Fields --

    [SerializeField] private Button _clickPackage;

    private IPackageData _packageData;
    private IPaymentProcessor _paymentProcessor;

    private IMomoHandler _momoHandler;
    private IStudentPaper _studentPaper;

    private bool _isWaitingForCallback = false;
    private string _orderId;
    private int _currentPaper;
    private int _amount;

    #endregion
}
