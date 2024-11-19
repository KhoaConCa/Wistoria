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

        StartCoroutine(_addHandler.AddNewCampus(_newCampus, OnAddSuccess));
    }

    #endregion

    #region -- Methods --

    void Start()
    {
        AddComponentAddHandler();
        GetTransformUI();
    }

    void OnDisable()
    {
        
    }

    private void AddComponentAddHandler()
    {
        if (_addHandler == null)
            _addHandler = gameObject.AddComponent<AddCampusH>();
        else
            Debug.Log("The AddCampusH component already exists");
    }

    private void GetTransformUI()
    {
        if (_transformUI == null)
        {
            _transformUI = GameObject.FindWithTag("MainUI").GetComponent<UITransformV>();
        }
        else
        {
            Debug.Log("The UITransformV component already exiests");
        }
    }

    private void OnAddSuccess(CampusD campus)
    {
        Debug.Log($"Create a campus: {campus.CampusName}, room: {campus.Room}");
    }

    private void ClickBackButton()
    {
        _transformUI.SetActiveObjectUI(_tagName);
    }

    private void SetEventButton()
    {


        //_addButton.onClick.AddListener(ClickAddButton);
        //_backButton.onClick.AddListener(ClickBackButton);
    }

    private void SetNewCampusData()
    {
        //_newCampus.CampusName = _campusNameField.text;
        //_newCampus.Room = _campusRoomField.text;
    }

    #endregion

    #region -- Fields --

    private ITransformUI _transformUI;
    private IAddCampusHandler _addHandler;

    private CampusD _newCampus = new CampusD();

    [SerializeField] private Button _addButton;
    [SerializeField] private Button _backButton;

    [SerializeField] private TMP_InputField _campusNameField;
    [SerializeField] private TMP_InputField _campusRoomField;

    [SerializeField] private TMP_Dropdown _campusNameDropDown;

    [SerializeField] private string _tagName;

    #endregion
}
