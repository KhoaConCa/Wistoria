using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AddCampusV : MonoBehaviour
{
    #region -- Methods --

    #region - Switch Option Event -
    public void SwitchAddNewCampus(Toggle toggleButton)
    {
        if (toggleButton.isOn)
        {
            _campusNameField.gameObject.SetActive(true);
            _campusNameField.text = "";

            _campusNameDropDown.gameObject.SetActive(false);
        }

    }

    public void SwitchAddNewRoom(Toggle toggleButton)
    {
        if (toggleButton.isOn)
        {
            _campusNameField.gameObject.SetActive(false);
            _campusNameDropDown.gameObject.SetActive(true);

            if (_campusNameDropDown.options.Count > 0)
                _campusNameDropDown.value = 0;
        }

    }
    #endregion

    #endregion

    #region -- Fields --

    [SerializeField] private TMP_InputField _campusNameField;
    [SerializeField] private TMP_InputField _campusRoomField;

    [SerializeField] private TMP_Dropdown _campusNameDropDown;

    #endregion
}
