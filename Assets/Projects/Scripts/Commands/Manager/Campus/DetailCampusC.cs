using UnityEngine;
using TMPro;
using UnityEngine.UI;   
using System;
using Utilities;
using System.Collections.Generic;

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
    }

    void OnEnable()
    {
        if (_campusNameDropDown != null && _campusNameDropDown != null)
        {
            StartCoroutine(_updateHandler.GetUniqueName(SetDataCampusName, MainView.OnSuccess, MainView.OnFailed));
            StartCoroutine(_updateHandler.GetUniqueRoom(SetDataCampusRoom, MainView.OnSuccess, MainView.OnFailed));
        }
    }

    void OnDisable()
    {
        if (_campusNameDropDown != null && _campusRoomDropDown != null)
            ClearDataModify();
    }

    #region -- Add Component --
    private void AddComponentHandler()
    {
        if (_updateHandler == null)
            _updateHandler = gameObject.AddComponent<DetailCampusH>();
        else
            Debug.Log("The DetailCampusH component already exists");
    }
    #endregion

    #region -- Set Data --
    private void SetDataCampusName(List<string> campusName)
    {
        _campusNameDropDown.ClearOptions();

        _campusNameDropDown.AddOptions(campusName);
        int indexSelected = campusName.IndexOf(_cardData.Name);
        _campusNameDropDown.value = indexSelected;
    }

    private void SetDataCampusRoom(List<string> campusRoom)
    {
        _campusRoomDropDown.ClearOptions();

        _campusRoomDropDown.AddOptions(campusRoom);
        int indexSelected = campusRoom.IndexOf(_cardData.Room);
        _campusRoomDropDown.value = indexSelected;
    }
    #endregion

    #region -- Main Event --
    public void OnClickSaveButton()
    {
        SetDataModify();
        GetDataModify();

        StartCoroutine(_updateHandler.UpdateCampusData(_campusData, MainView.OnSuccess, MainView.OnFailed));
    }

    /// <summary>
    /// Set new campus data
    /// </summary>
    private void GetDataModify()
    {
        _campusData.Initialize(_cardData);
    }

    private void SetDataModify()
    {
        _cardData.Name = _campusNameDropDown.captionText.text;
        _cardData.Room = _campusRoomDropDown.captionText.text;
    }

    private void ClearDataModify()
    {
        _campusNameDropDown.ClearOptions();
        _campusRoomDropDown.ClearOptions();
    }
    #endregion

    #endregion

    #region -- Fields -- 

    private ICampusCardData _cardData;
    private IDetailCampusUpdateHandler _updateHandler;

    private CampusD _campusData = new CampusD();

    [SerializeField] private Button _saveButton;

    [SerializeField] private TMP_Dropdown _campusNameDropDown;
    [SerializeField] private TMP_Dropdown _campusRoomDropDown;

    #endregion
}
