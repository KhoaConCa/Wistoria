using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AddCampusV : MonoBehaviour
{
    #region -- Methods --

    private void OnEnable()
    {
        GetTransformUI();
    }

    private void OnDisable()
    {
        ResetAsDefault();
    }

    private void GetTransformUI()
    {
        if (_transformUI == null)
            _transformUI = GameObject.FindWithTag("MainUI").GetComponent<UITransformV>();
        else
            Debug.Log("The UITransformV component already exiests");
    }

    public void SwitchAddNewCampus()
    {
        if (_addNewCampus.isOn)
        {
            _campusNameField.gameObject.SetActive(true);
            _campusNameDropDown.gameObject.SetActive(false);
        }

    }

    public void SwitchAddNewRoom()
    {
        if (_addNewRoom.isOn)
        {
            _campusNameField.gameObject.SetActive(false);
            _campusNameDropDown.gameObject.SetActive(true);
        }

    }

    public void ResetAsDefault()
    {
        _campusNameDropDown.ClearOptions();

        _campusNameField.text = "";
        _campusRoomField.text = "";
    }

    #endregion

    #region -- Fields --

    private ITransformUI _transformUI;

    [SerializeField] private Button _addButton;
    [SerializeField] private Button _backButton;

    [SerializeField] private Toggle _addNewRoom;
    [SerializeField] private Toggle _addNewCampus;

    [SerializeField] private TMP_InputField _campusNameField;
    [SerializeField] private TMP_InputField _campusRoomField;

    [SerializeField] private TMP_Dropdown _campusNameDropDown;

    [SerializeField] private string _tagName;

    #endregion
}
