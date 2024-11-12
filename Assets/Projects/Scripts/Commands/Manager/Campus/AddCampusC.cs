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

        addButton.onClick.AddListener(ClickAddButton);
        backButton.onClick.AddListener(ClickBackButton);
    }

    private void AddComponentAddHandler()
    {
        if (_addHandler == null)
        {
            _addHandler = gameObject.AddComponent<AddCampusH>();
        }
        else
        {
            Debug.Log("The AddCampusH component already exists");
        }
    }

    private void GetTransformUI()
    {
        if (_transformUI == null)
        {
            _transformUI = gameObject.GetComponent<UITransformV>();
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
        _transformUI.SetActiveCampusUI(_searchCampus);
    }

    private void SetNewCampusData()
    {
        _newCampus.CampusName = campusNameField.text;
        _newCampus.Room = campusRoomField.text;
    }

    #endregion

    #region -- Fields --

    private CampusD _newCampus = new CampusD();

    public Button addButton;
    public Button backButton;

    [SerializeField] private GameObject _searchCampus;

    public TMP_Text campusNameField;
    public TMP_Text campusRoomField;

    private ITransformUI _transformUI;
    private IAddCampusHandler _addHandler;

    #endregion
}
