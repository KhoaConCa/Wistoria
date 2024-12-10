using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DetailPrinterV : MonoBehaviour
{
    #region -- Methods --

    private void OnEnable()
    {
        DisableInteractableField();
    }

    #region - Interactable Fields -
    public void EnableInteractableField()
    {
        _nameInputField.interactable = true;
        _typeInputField.interactable = true;
        _descriptionInputField.interactable = true;

        _locationDropDown.interactable = true;
        _statusDropDown.interactable = true;
    }

    public void DisableInteractableField()
    {
        _nameInputField.interactable = false;
        _typeInputField.interactable = false;
        _descriptionInputField.interactable = false;

        _locationDropDown.interactable = false;
        _statusDropDown.interactable = false;
    }

    #endregion

    #region - Modify Button Event -
    public void EnableModifyButton(GameObject buttonObject)
    {
        buttonObject.SetActive(true);
    }

    public void DisableModifyButton(GameObject buttonObject)
    {
        buttonObject.SetActive(false);
    }
    #endregion

    #endregion

    #region -- Fields --

    [SerializeField] private TMP_InputField _nameInputField;
    [SerializeField] private TMP_InputField _typeInputField;
    [SerializeField] private TMP_InputField _descriptionInputField;

    [SerializeField] private TMP_Dropdown _locationDropDown;
    [SerializeField] private TMP_Dropdown _statusDropDown;

    #endregion
}
