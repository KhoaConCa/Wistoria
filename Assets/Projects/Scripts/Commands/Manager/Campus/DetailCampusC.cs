using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEditor.PackageManager.Requests;
using CampusDataManager;
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
        if (campus == null)
        {
            Debug.LogWarning("Campus data is null. Cannot display details.");
            return;
        }

        SetComponentBasedOn();

        campusNameField.placeholder.GetComponent<TextMeshProUGUI>().text = campus.CampusName;
        campusRoomField.placeholder.GetComponent<TextMeshProUGUI>().text = campus.CampusRoom;
    }

    #endregion

    #region -- Methods -- 

    void Start()
    {
        AddComponentHandler();

        GetDataModify();

        _saveButton?.onClick.AddListener(OnClickSaveButton);
    }

    private void AddComponentHandler()
    {
        if (_updateHandler == null)
        {
            _updateHandler = gameObject.AddComponent<DetailCampusH>();
        }
        else
        {
            Debug.Log("The DetailCampusH component already exists");
        }
    }

    /// <summary>
    /// Get transform of father component
    /// </summary>
    private void SetComponentBasedOn()
    {
        if (this.gameObject != null && this.gameObject.activeSelf)
        {
            campusNameField = GameObject.FindWithTag("ValueCampus").GetComponent<TMP_InputField>();
            campusRoomField = GameObject.FindWithTag("ValueRoom").GetComponent<TMP_InputField>();

            if (campusNameField == null || campusRoomField == null)
                Debug.Log("hello");

            //SetDataBaseOn();
        }
        else
        {
            Debug.LogError("DetailCampus not found in hierarchy.");
        }
    }

 /*   /// <summary>
    /// Set data for detail GUI
    /// </summary>
    private void SetDataBaseOn()
    {
        try
        {
            if (campusNameField != null || campusRoomField != null)
            {
                campusNameField.placeholder.GetComponent<TextMeshProUGUI>().text = CampusManager.currentCampusName;
                campusRoomField.placeholder.GetComponent<TextMeshProUGUI>().text = CampusManager.currentCampusRoom;
            }

        }
        catch (Exception e)
        {
            Debug.LogError($"Error in: {e.Message}");
        }
    }*/

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
        _campusData._id = CampusManager.currentCampusID;
        _campusData.CampusName = CampusManager.currentCampusName;
        _campusData.Room = CampusManager.currentCampusRoom;
        _campusData.__v = CampusManager.__v;
    }

    private void SetDataModify()
    {
        CampusManager.currentCampusName = campusNameField.textComponent.text;
        CampusManager.currentCampusRoom = campusRoomField.textComponent.text;
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

    [SerializeField] private TMP_InputField campusNameField;
    [SerializeField] private TMP_InputField campusRoomField;

    #endregion
}
