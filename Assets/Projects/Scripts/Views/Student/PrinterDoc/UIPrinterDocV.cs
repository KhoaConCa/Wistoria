using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;
using Utilities;

public class UIPrinterDocV : MonoBehaviour, IPrinterDocView
{
    #region -- Implements --

    public QueueD GetQueue() => _printerDocCard.Queue;
    public DocumentDStudent GetDocument() => _printerDocCard.Document;

    public void SetPrinterDocCard(PrinterDocDStudent data)
    {
        _printerDocCard.Initialize(data);
    }

    public void TopUpPaper(int paperNeed)
    {
        string message = $"Số của bạn hiện tại không đủ! " +
            $"Vui lòng thanh toán thêm {paperNeed} giấy để tiếp tục!";

        MainView.OnNotice(message, null, GoToTopUp);
    }

    public void SwitchHistory()
    {
        _historyButton.onClick.Invoke();
    }

    #endregion

    #region -- Methods --

    private void Start()
    {
        GetComponent();
    }

    #region -- Get Component --
    private void GetComponent()
    {
        if (_printerDocC == null)
            _printerDocC = gameObject.GetComponent<PrinterDocC>();

        if (_historyButton == null)
        {
            GameObject hisButton = GameObject.FindWithTag("HisNavi");
            _historyButton = hisButton.GetComponent<Button>();
        }

        if (_storeButton == null)
        {
            GameObject storeButton = GameObject.FindWithTag("StoreButton");
            _storeButton = storeButton.GetComponent<Button>();
        }
    }
    #endregion

    #region -- Get Addressable --
    private void GetPrefabByLabel(string label)
    {
        _prefab = new AssetLabelReference { labelString = label };
    }
    #endregion

    #region -- Go To Top Up --
    private void GoToTopUp()
    {
        Button button = _storeButton.GetComponent<Button>();
        button.onClick.Invoke();
    }
    #endregion

    #region -- Create Printer Doc Event --
    public void OnCreatePrinterDoc()
    {
        PrinterDocDStudent printerDocD = GetPrinterDocData();

        if (string.IsNullOrEmpty(printerDocD.Printer._id))
        {
            MainView.OnFailed("Vui lòng chọn máy in!");
            return;
        }

        _printerDocC.CreatePrinterDoc(printerDocD);
    }
    #endregion

    #region -- Get Printer Doc Data --
    private PrinterDocDStudent GetPrinterDocData()
    {
        PrinterDocDStudent newData = new PrinterDocDStudent();
        newData.Initialize(_printerDocCard);

        if (newData.PrinterRaw == null)
            newData.PrinterRaw = newData.Printer;

        return newData;
    }
    #endregion

    #endregion

    #region -- Fields --

    private IPrinterDocCommand _printerDocC;

    private AssetLabelReference _prefab;

    private Button _historyButton;
    private Button _storeButton;

    [SerializeField] private PrinterDocCard _printerDocCard;

    #endregion
}
