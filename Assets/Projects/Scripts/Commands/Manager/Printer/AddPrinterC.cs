using Sirenix.Utilities;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utilities;

public class AddPrinterC : MonoBehaviour, IAddPrinterCommand
{
    #region -- Implements --

    /// <summary>
    /// Switch UI
    /// </summary>
    public void ClickAddButton()
    {
        SetNewData();

        StartCoroutine(_addHandler.AddNewPrinter(_printerD, result =>
        {
            MainView.OnSuccess(result.Message);

            StartCoroutine(_addHandler.AddNewQueue(result.Data[0], MainView.OnDebugged, MainView.OnFailed));

            SetUpDataDefault();

        }, MainView.OnFailed));
    }

    #endregion

    #region -- Methods --

    private void Awake()
    {
        AddComponentAddHandler();
    }

    private void OnEnable()
    {
        SetUpDataDefault();
    }

    #region - Add Component -
    private void AddComponentAddHandler()
    {
        if (_addHandler == null)
            _addHandler = gameObject.AddComponent<AddPrinterH>();
        else
            Debug.Log("The AddPrinterH component already exists");
    }
    #endregion

    #region - Set Data -

    private void SetUpDataDefault()
    {
        ResetAsDefault();
        StartCoroutine(_addHandler.GetAllCampus(SetDataDropDown, MainView.OnDebugged, message =>
        {
            MainView.OnReset(SetUpDataDefault, message);
        }));
    }

    public void ResetAsDefault()
    {
        _printerNameField.text = "";
        _printerTypeField.text = "";
        _descriptionField.text = "";

        _locateAtDropDown.ClearOptions();
        _statusDropDown.ClearOptions();
    }

    public void SetDataDropDown(List<CampusD> campusD)
    {
        _campusD = campusD;

        _campusDs = TransferData(campusD);
        _locateAtDropDown.ClearOptions();
        _locateAtDropDown.AddOptions(_campusDs.Values.ToList());

        List<string> statusData = EnumProperties.ConvertEnumToList<PrinterStatus>("- Chọn trạng thái -");
        _statusDropDown.ClearOptions();
        _statusDropDown.AddOptions(statusData);
    }

    private Dictionary<string, string> TransferData(List<CampusD> datas)
    {
        Dictionary<string, string> newData = new Dictionary<string, string>() 
        { 
            ["0"] = "- Chọn cơ sở -"
        };

        foreach (var data in datas)
        {
            newData[data.Id] = data.Name + " - " + data.Room;
        }

        return newData;
    }
    #endregion

    #region - Add Event Button -

    /// <summary>
    /// Set new printer data
    /// </summary>
    private void SetNewData()
    {
        _printerD.PrinterName = GetDataTextBox(_printerNameField.text);
        _printerD.PrinterType = GetDataTextBox(_printerTypeField.text);
        _printerD.Description = GetDataTextBox(_descriptionField.text);
        _printerD.Ink = 0;
        _printerD.Paper = 0;

        _printerD.LocateAtRaw = GetDataDropDown();
        _printerD.Status = _statusDropDown.captionText.text;
    }

    private string GetDataTextBox(string value)
    {
        if (value == "")
            return null;

        return value;
    }

    private string GetDataDropDown()
    {
        int valueSelected = _campusDs.Values.ToList().IndexOf(_locateAtDropDown.captionText.text);
        string idSelected = _campusDs.Keys.ElementAt(valueSelected);

        foreach (CampusD campus in _campusD)
        {
            if (campus.Id == idSelected) 
                return campus.Id;
        }

        return null;
    }
    #endregion

    #endregion

    #region -- Fields --

    private IAddPrinterHandler _addHandler;

    private Dictionary<string, string> _campusDs = new Dictionary<string, string>();

    private PrinterD _printerD = new PrinterD();
    private List<CampusD> _campusD = new List<CampusD>();

    [SerializeField] private TMP_InputField _printerNameField;
    [SerializeField] private TMP_InputField _printerTypeField;
    [SerializeField] private TMP_InputField _descriptionField;

    [SerializeField] private TMP_Dropdown _locateAtDropDown;
    [SerializeField] private TMP_Dropdown _statusDropDown;

    #endregion
}
