using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DetailCampusV : MonoBehaviour
{
    #region -- Methods --

    private void OnEnable()
    {
        DisableInteractableField();
    }

    private void OnDisable()
    {
        ResetInteractableField();
    }

    #region - Interactable Fields -
    public void EnableInteractableField()
    {
        _campusRoomDropDown.GetComponent<TMP_Dropdown>().interactable = true;
        _campusNameDropDown.GetComponent<TMP_Dropdown>().interactable = true;
    }

    public void DisableInteractableField()
    {
        _campusRoomDropDown.GetComponent<TMP_Dropdown>().interactable = false;
        _campusNameDropDown.GetComponent<TMP_Dropdown>().interactable = false;
    }

    public void ResetInteractableField()
    {
        _campusNameDropDown.GetComponent<TMP_Dropdown>().ClearOptions();
        _campusRoomDropDown.GetComponent<TMP_Dropdown>().ClearOptions();

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

    [SerializeField] private GameObject _campusRoomDropDown;
    [SerializeField] private GameObject _campusNameDropDown;

    #endregion
}
