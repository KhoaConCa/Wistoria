using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DetailCampusV : MonoBehaviour
{
    #region -- Implements --

    #endregion

    #region -- Methods --

    void Start()
    {
        GetComponentTransform();

        _backButton.onClick.AddListener(SwitchUIBack);
        _backButton.onClick.AddListener(DisableInputField);
        _backButton.onClick.AddListener(EnableModifyButton);
        _backButton.onClick.AddListener(DisableSaveButton);

        _modifyButton.onClick.AddListener(EnableInputField);
        _modifyButton.onClick.AddListener(DisableModifyButton);
        _modifyButton.onClick.AddListener(EnableSaveButton);
    }

    private void GetComponentTransform()
    {
        if (_transformUI == null)
        {
            _transformUI = GameObject.FindWithTag(_parentTag).GetComponent<UITransformV>();
        }
        else
        {
            Debug.Log("The UITransformV component already exists");
        }
    }

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

    private void SwitchUIBack()
    {
        _transformUI.SetActiveObjectUI(_targetTag);
    }

    private void EnableModifyButton()
    {
        _edit.SetActive(true);
    }

    private void DisableModifyButton()
    {
        _edit.SetActive(false);
    }

    private void EnableSaveButton()
    {
        _save.SetActive(true);
    }

    private void DisableSaveButton()
    {
        _save.SetActive(false);
    }

    #endregion

    #region -- Fields --

    private ITransformUI _transformUI;

    [TagSelector] [SerializeField] private string _targetTag;
    [TagSelector] [SerializeField] private string _parentTag;

    [SerializeField] private GameObject _edit;
    [SerializeField] private GameObject _save;

    [SerializeField] private Button _backButton;
    [SerializeField] private Button _modifyButton;
    [SerializeField] private Button _saveButton;
    [SerializeField] private Button _deleteButton;

    public TMP_InputField campusNameInput;
    public TMP_InputField campusRoomInput;

    

        #endregion
}
