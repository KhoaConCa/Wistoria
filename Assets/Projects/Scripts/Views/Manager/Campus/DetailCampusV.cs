using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DetailCampusV : MonoBehaviour
{
    #region -- Methods --

    void Start()
    {
        GetComponentDefault();

        AddEventEditButton();
        AddEventBackButton();
    }

    private void GetComponentDefault()
    {
        if (_transformUI == null)
            _transformUI = GameObject.FindWithTag(_parentTag).GetComponent<UITransformV>();
        else
            Debug.Log("The UITransformV component already exists");
    }

    private Button GetComponentButton(GameObject button)
    {
        return button.GetComponentInChildren<Button>();
    }

    #region - Input fields -
    private void EnableInputField()
    {
        campusNameInput.interactable = true;
        campusRoomInput.interactable = true;
    }

    private void DisableInputField()
    {
        campusNameInput.interactable = false;
        campusRoomInput.interactable = false;
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
        Button buttonComponent = _edit.GetComponentInChildren<Button>();

        buttonComponent.onClick.AddListener(EnableInputField);
        buttonComponent.onClick.AddListener(DisableModifyButton);
        buttonComponent.onClick.AddListener(EnableSaveButton);
    }

    private void AddEventBackButton()
    {
        Button buttonComponent = _back.GetComponentInChildren<Button>();

        buttonComponent.onClick.AddListener(SwitchUIBack);
        buttonComponent.onClick.AddListener(DisableInputField);
        buttonComponent.onClick.AddListener(EnableModifyButton);
        buttonComponent.onClick.AddListener(DisableSaveButton);
    }
    #endregion

    #endregion

    #region -- Fields --

    private ITransformUI _transformUI;

    [TagSelector] [SerializeField] private string _targetTag;
    [TagSelector] [SerializeField] private string _parentTag;

    [SerializeField] private GameObject _edit;
    [SerializeField] private GameObject _save;
    [SerializeField] private GameObject _delete;
    [SerializeField] private GameObject _back;

    public TMP_InputField campusNameInput;
    public TMP_InputField campusRoomInput;

    #endregion
}
