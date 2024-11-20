using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AddCampusC : MonoBehaviour, IAddCampusCommand
{
    #region -- Implements --

    /// <summary>
    /// Switch UI
    /// </summary>
    public void ClickAddButton()
    {
        SetNewCampusData();
        ClearData();

        StartCoroutine(_addHandler.AddNewCampus(_newCampus, OnAddSuccess));
    }

    #endregion

    #region -- Methods --

    private void Awake()
    {
        AddComponentAddHandler();
    }

    private void OnEnable()
    {
        if (_addHandler != null)
        {
            StartCoroutine(_addHandler.GetUniqueName(SetDataForDropDown));
            SetDataAsDefault();
        }
    }

    private void OnDisable()
    {
        ResetAsDefault();
    }

    #region - Add Component -
    private void AddComponentAddHandler()
    {
        if (_addHandler == null)
            _addHandler = gameObject.AddComponent<AddCampusH>();
        else
            Debug.Log("The AddCampusH component already exists");
    }
    #endregion

    #region - Set Data -
    private void SetDataAsDefault()
    {
        _newCampus = new CampusD();
    }

    private void SetDataForDropDown(List<string> campusName)
    {
        campusName.Insert(0, "- Chọn cơ sở -");

        _campusNameDropDown.ClearOptions();
        _campusNameDropDown.AddOptions(campusName);

        _campusNameDropDown.value = 0;
    }
    #endregion

    #region - Interactable Field -
    public void ResetAsDefault()
    {
        _addNewCampus.isOn = false;
        _addNewRoom.isOn = true;

        _campusNameDropDown.ClearOptions();

        _campusNameField.text = "";
        _campusRoomField.text = "";
    }
    #endregion

    #region - Add New Campus -
    private void SetNewCampusData()
    {
        if (_campusNameDropDown.gameObject.activeSelf)
            _newCampus.CampusName = _campusNameDropDown.captionText.text;
        else
            _newCampus.CampusName = _campusNameField.text;

        _newCampus.Room = _campusRoomField.text;
    }

    private void ClearData()
    {
        if (_campusNameDropDown.gameObject.activeSelf)
            StartCoroutine(_addHandler.GetUniqueName(SetDataForDropDown));
        else
            _campusNameField.text = "";

        _campusRoomField.text = "";
    }

    private void OnAddSuccess(CampusD campusD)
    {
        Debug.Log($"Add new campus {campusD.CampusName} - {campusD.Room} successfully!");
    }
    #endregion

    #endregion

    #region -- Fields --

    private IAddCampusHandler _addHandler;

    private CampusD _newCampus = new CampusD();

    [SerializeField] private Toggle _addNewRoom;
    [SerializeField] private Toggle _addNewCampus;

    [SerializeField] private TMP_InputField _campusNameField;
    [SerializeField] private TMP_InputField _campusRoomField;

    [SerializeField] private TMP_Dropdown _campusNameDropDown;

    #endregion
}
