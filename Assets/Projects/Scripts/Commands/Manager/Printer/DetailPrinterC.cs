using UnityEngine;
using TMPro;
using UnityEngine.UI;
using PrinterDataManager;
using System;

public class DetailPrinterC : MonoBehaviour, IPrinterDetailCommand
{
    #region -- Implements -- 

    /// <summary>
    /// Display Detail Printer to the prefab
    /// </summary>
    /// <param name="printer">Data of printer was clicked</param>
    public void DisplayPrinterDetails(IPrinterCardData printer)
    {
        try
        {
            if (printer == null)
            {
                Debug.LogWarning("Printer data is null. Cannot display details.");
                return;
            }

            _printerNameField.placeholder.GetComponent<TextMeshProUGUI>().text = printer.PrinterName;
            _printerTypeField.placeholder.GetComponent<TextMeshProUGUI>().text = printer.PrinterType;
            _descriptionField.placeholder.GetComponent<TextMeshProUGUI>().text = printer.Description;
            _paperField.placeholder.GetComponent<TextMeshProUGUI>().text = printer.Paper.ToString();
            _inkField.placeholder.GetComponent<TextMeshProUGUI>().text = printer.Ink.ToString(); ;
            
            _locateAt.captionText.GetComponent<TextMeshProUGUI>().text = printer.LocateAt.CampusName + " - " + printer.LocateAt.Room;
            _status.captionText.GetComponent<TextMeshProUGUI>().text = printer.Status.ToString();
        }
        catch (Exception e)
        {
            Debug.LogError(e.Message);
        }
    }

    #endregion

    #region -- Methods -- 

    void Start()
    {
        AddComponentHandler();

        _saveButton?.onClick.AddListener(OnClickSaveButton);
    }

    private void AddComponentHandler()
    {
        if (_updateHandler == null)
            _updateHandler = gameObject.AddComponent<DetailPrinterH>();
        else
            Debug.Log("The DetailPrinterH component already exists");
    }

    private void OnClickSaveButton()
    {
        SetDataModify();
        GetDataModify();
        ClearDataModify();

        StartCoroutine(_updateHandler.UpdatePrinterData(_printerData, OnSuccess, OnFailed));
    }

    /// <summary>
    /// Handlers the response from the server when update successfully
    /// </summary>
    /// <param name="printer">Printer data updated</param>
    public void OnSuccess(PrinterD printer)
    {
        Debug.Log($"Updated Printer: {printer.PrinterName}, Room: {printer.LocateAt}");
    }

    /// <summary>
    /// Handlers the response from the server when update failed
    /// </summary>
    public void OnFailed(PrinterD printer)
    {
        Debug.Log($"Can not update Printer! Try again!");
    }

    /// <summary>
    /// Set new printer data
    /// </summary>
    private void GetDataModify()
    {
        _printerData._id = PrinterManager.currentPrinterID;
        _printerData.PrinterName = PrinterManager.currentPrinterName;
        _printerData.LocateAt = PrinterManager.currentLocateAt;
        _printerData.__v = PrinterManager.__v;
    }

    private void SetDataModify()
    {
        PrinterManager.currentPrinterName = _printerNameField.textComponent.text;
        //PrinterManager.currentLocateAt = _locateAt.itemText.text;
    }

    private void ClearDataModify()
    {
        _printerNameField.textComponent.text = null;
        _locateAt.itemText.text = null;
    }

    #endregion

    #region -- Fields -- 

    private PrinterD _printerData = new PrinterD();

    private IPrinterCardData _cardData;
    private IDataPrinterTransferHandler _detailHandler;
    private IDetailPrinterUpdateHandler _updateHandler;

    [SerializeField] private Button _saveButton;
    [SerializeField] private Button _deleteButton;

    [SerializeField] private TMP_InputField _printerNameField;
    [SerializeField] private TMP_InputField _printerTypeField;
    [SerializeField] private TMP_InputField _descriptionField;
    [SerializeField] private TMP_InputField _paperField;
    [SerializeField] private TMP_InputField _inkField;

    [SerializeField] private TMP_Dropdown _locateAt;
    [SerializeField] private TMP_Dropdown _status;

    #endregion
}
