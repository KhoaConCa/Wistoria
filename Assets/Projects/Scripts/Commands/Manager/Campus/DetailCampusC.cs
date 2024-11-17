using UnityEngine;
using TMPro;
using UnityEngine.UI;   
using System;

public class DetailCampusC : MonoBehaviour, ICampusDetailCommand
{
    #region -- Implements -- 

    /// <summary>
    /// Display Detail Campus to the prefab
    /// </summary>
    /// <param name="campus">Data of campus was clicked</param>
    public void DisplayCampusDetails(ICampusCardData campus)
    {
        try
        {
            if (campus == null)
            {
                Debug.LogWarning("Campus data is null. Cannot display details.");
                return;
            }

            _cardData = campus;
        }
        catch (Exception e)
        {
            Debug.LogError(e.Message);
        }
    }

    #endregion

    #region -- Methods -- 

    void Awake()
    {
        AddComponentHandler();

        _saveButton?.onClick.AddListener(OnClickSaveButton);
    }

    void OnEnable()
    {
        if (_campusNameField != null && _campusRoomField != null)
            SetData();
    }

    void OnDisable()
    {
        if (_campusNameField != null && _campusRoomField != null)
            ClearDataModify();
}

    private void SetData()
    {
        _campusNameField.placeholder.GetComponent<TextMeshProUGUI>().text = _cardData.CampusName;
        _campusRoomField.placeholder.GetComponent<TextMeshProUGUI>().text = _cardData.CampusRoom;
    }

    private void AddComponentHandler()
    {
        if (_updateHandler == null)
            _updateHandler = gameObject.AddComponent<DetailCampusH>();
        else
            Debug.Log("The DetailCampusH component already exists");
    }

    private void OnClickSaveButton()
    {
        SetDataModify();
        GetDataModify();

        StartCoroutine(_updateHandler.UpdateCampusData(_campusData, OnSuccess, OnFailed));
    }

    /// <summary>
    /// Handlers the response from the server when update successfully
    /// </summary>
    /// <param name="campus">Campus data updated</param>
    public void OnSuccess(CampusD campus)
    {
        Debug.Log($"Updated Campus: {campus.CampusName}, Room: {campus.Room}");
    }

    /// <summary>
    /// Handlers the response from the server when update failed
    /// </summary>
    public void OnFailed(CampusD campus)
    {
        Debug.Log($"Can not update Campus! Try again!");
    }

    /// <summary>
    /// Set new campus data
    /// </summary>
    private void GetDataModify()
    {
        _campusData._id = _cardData.CampusID;
        _campusData.CampusName = _cardData.CampusName;
        _campusData.Room = _cardData.CampusRoom;
        _campusData.__v = "0";
    }

    private void SetDataModify()
    {
        _cardData.CampusName = _campusNameField.textComponent.text;
        _cardData.CampusRoom = _campusRoomField.textComponent.text;
    }

    private void ClearDataModify()
    {
        _campusNameField.text = "";
        _campusRoomField.text = "";

        _campusNameField.placeholder.GetComponent<TextMeshProUGUI>().text = "";
        _campusRoomField.placeholder.GetComponent<TextMeshProUGUI>().text = "";
    }

    #endregion

    #region -- Fields -- 

    private CampusD _campusData = new CampusD();

    private ICampusCardData _cardData;
    private IDataCampusTransferHandler _detailHandler;
    private IDetailCampusUpdateHandler _updateHandler;

    [SerializeField] private Button _saveButton;
    [SerializeField] private Button _deleteButton;
    [SerializeField] private Button _backButton;

    [SerializeField] private TMP_InputField _campusNameField;
    [SerializeField] private TMP_InputField _campusRoomField;

    #endregion
}
