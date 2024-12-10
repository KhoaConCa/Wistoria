using UnityEngine;
using TMPro;
using UnityEngine.UI;
using PrinterDataManager;
using System;
using System.Collections.Generic;
using System.Linq;
using Utilities;

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

            _cardData = printer;
        }
        catch (Exception e)
        {
            Debug.LogError(e.Message);
        }
    }

    #endregion

    #region -- Methods -- 

    private void Awake()
    {
        AddComponentHandler();
    }

    private void OnEnable()
    {
        SetData();
    }

    #region -- Set Data --
    private void SetData()
    {
        ResetInteractableField();
        StartCoroutine(_updateHandler.GetAllCampus(SetDataToEditField, onSuccess =>
        {
            MainView.OnDebugged(onSuccess);
        }, onFailed =>
        {
            MainView.OnReset(SetData, onFailed);
        }));
    }
    #endregion

    #region - Add Component -
    private void AddComponentHandler()
    {
        if (_updateHandler == null)
            _updateHandler = gameObject.AddComponent<DetailPrinterH>();
        else
            Debug.Log("The DetailPrinterH component already exists");
    }
    #endregion

    #region - Set Data As Default -
    private void SetDataToEditField(List<CampusD> campusD)
    {
        _printerNameField.text = _cardData.PrinterName;
        _printerTypeField.text = _cardData.PrinterType;
        _descriptionField.text = _cardData.Description;

        _campusD = campusD;
        int locationSelected = campusD.FindIndex(campus => campus.Id == _cardData.LocateAt.Id);
        _campusDs =  TransferData(campusD);
        _locateAtDropDown.ClearOptions();
        _locateAtDropDown.AddOptions(_campusDs.Values.ToList());
        _locateAtDropDown.value = locationSelected;

        List<string> statusData = EnumProperties.ConvertEnumToList<PrinterStatus>();
        var statusSelected = EnumProperties.GetEnumIdByName<PrinterStatus>(_cardData.Status);
        _statusDropDown.AddOptions(statusData);
        _statusDropDown.value = statusSelected;
    }

    private Dictionary<string, string> TransferData(List<CampusD> datas)
    {
        Dictionary<string, string> newData = new Dictionary<string, string>();

        foreach(var data in datas)
        {
            newData[data.Id] = data.Name + " - " + data.Room;
        }

        return newData;
    }
    #endregion

    #region - Reset Data Fields As Default -
    public void ResetInteractableField()
    {
        _printerNameField.text = "";
        _printerTypeField.text = "";
        _descriptionField.text = "";

        _locateAtDropDown.ClearOptions();
        _statusDropDown.ClearOptions();
    }
    #endregion

    #region - Save Event Button -
    public void OnClickSaveButton()
    {
        SetDataModify();
        StartCoroutine(_updateHandler.UpdatePrinterData(_printerD, MainView.OnSuccess, MainView.OnFailed));
    }

    /// <summary>
    /// Set new printer data
    /// </summary>
    private void SetDataModify()
    {
        _printerD._id = _cardData.PrinterID;
        _printerD.PrinterName = GetDataTextBox(_printerNameField.text, _cardData.PrinterName);
        _printerD.PrinterType = GetDataTextBox(_printerTypeField.text, _cardData.PrinterType);
        _printerD.Description = GetDataTextBox(_descriptionField.text, _cardData.Description);
        _printerD.Ink = _cardData.Ink;
        _printerD.Paper = _cardData.Paper;

        CampusD campus = GetDataDropDown();
        _printerD.LocateAtRaw = campus;
        _printerD.UpdateLocateAt(campus);
        _printerD.Status = EnumProperties
                           .GetEnumByDescription<PrinterStatus>(_statusDropDown.captionText.text)
                           .ToString();

        _printerD.__v = "0";

        _cardData.Initialize(_printerD);
    }

    private string GetDataTextBox(string newValue, string oldValue)
    {
        if (newValue != "")
            return newValue;
        else
            return oldValue;
    }

    private CampusD GetDataDropDown()
    {
        int valueSelected = _campusDs.Values.ToList().IndexOf(_locateAtDropDown.captionText.text);
        string idSelected = _campusDs.Keys.ElementAt(valueSelected);

        if (idSelected != _cardData.LocateAt.Id)
        {
            foreach (CampusD campus in _campusD)
            {
                if (campus.Id == idSelected) return campus;
            }
        }
        else
            return _cardData.LocateAt;

        return null;
    }
    #endregion

    #endregion

    #region -- Fields -- 

    private IPrinterCardData _cardData;
    private IDetailPrinterUpdateHandler _updateHandler;

    private Dictionary<string, string> _campusDs = new Dictionary<string, string>();

    private PrinterD _printerD = new PrinterD();
    private List<CampusD> _campusD = new List<CampusD>();

    [SerializeField] private Button _saveButton;

    [SerializeField] private TMP_InputField _printerNameField;
    [SerializeField] private TMP_InputField _printerTypeField;
    [SerializeField] private TMP_InputField _descriptionField;

    [SerializeField] private TMP_Dropdown _locateAtDropDown;
    [SerializeField] private TMP_Dropdown _statusDropDown;

    #endregion
}
