using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DetailPrinterV : MonoBehaviour
{
    #region -- Methods --

    void Start()
    {
        GetComponentDefault();

        AddEventEditButton();
        AddEventBackButton();

        DisableInputField();
    }

    private void GetComponentDefault()
    {
        if (_transformUI == null)
            _transformUI = GameObject.FindWithTag(_parentTag).GetComponent<UITransformV>();
        else
            Debug.Log("The UITransformV component already exists");
    }

    #region - Input fields -
    private void EnableInputField()
    {
        _printerNameField.interactable = true;
        _printerTypeField.interactable = true;
        _descriptionField.interactable = true;
        _paperField.interactable = true;
        _inkField.interactable = true;

        _locateAt.interactable = true;
        _status.interactable = true;
    }

    private void DisableInputField()
    {
        _printerNameField.interactable = false;
        _printerTypeField.interactable = false;
        _descriptionField.interactable = false;
        _paperField.interactable = false;
        _inkField.interactable = false;

        _locateAt.interactable = false;
        _status.interactable = false;
    }
    #endregion

    #region - Button back -
    private void SwitchUIBack()
    {
        _transformUI.SetActiveObjectUI(_targetTag);
    }
    #endregion

    #region - Button edit -
    private void EnableModifyButton()
    {
        _edit.SetActive(true);
    }

    private void DisableModifyButton()
    {
        _edit.SetActive(false);
    }
    #endregion

    #region - Button save -
    private void EnableSaveButton()
    {
        _save.SetActive(true);
    }

    private void DisableSaveButton()
    {
        _save.SetActive(false);
    }
    #endregion

    #region - Add event button -
    private void AddEventEditButton()
    {
        Button buttonComponent = GetComponentButton(ref _edit);

        buttonComponent.onClick.AddListener(EnableInputField);
        buttonComponent.onClick.AddListener(DisableModifyButton);
        buttonComponent.onClick.AddListener(EnableSaveButton);
    }

    private void AddEventBackButton()
    {
        Button buttonComponent = GetComponentButton(ref _back);

        buttonComponent.onClick.AddListener(SwitchUIBack);
        buttonComponent.onClick.AddListener(DisableInputField);
        buttonComponent.onClick.AddListener(EnableModifyButton);
        buttonComponent.onClick.AddListener(DisableSaveButton);
    }

    private Button GetComponentButton(ref GameObject objectButton)
    {
        return objectButton.GetComponent<Button>();
    }
    #endregion

    #endregion

    #region -- Fields --

    private ITransformUI _transformUI;

    [TagSelector][SerializeField] private string _targetTag;
    [TagSelector][SerializeField] private string _parentTag;

    [SerializeField] private GameObject _edit;
    [SerializeField] private GameObject _save;
    [SerializeField] private GameObject _delete;
    [SerializeField] private GameObject _back;

    [SerializeField] private TMP_InputField _printerNameField;
    [SerializeField] private TMP_InputField _printerTypeField;
    [SerializeField] private TMP_InputField _descriptionField;
    [SerializeField] private TMP_InputField _paperField;
    [SerializeField] private TMP_InputField _inkField;

    [SerializeField] private TMP_Dropdown _locateAt;
    [SerializeField] private TMP_Dropdown _status;

    #endregion
}
