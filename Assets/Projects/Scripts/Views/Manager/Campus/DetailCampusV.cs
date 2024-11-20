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

    #region - Interactable fields -
    public void EnableInteractableField()
    {
        _campusRoomInput.GetComponent<TMP_InputField>().interactable = true;

        _campusNameDropDown.GetComponent<TMP_Dropdown>().interactable = true;
    }

    public void DisableInteractableField()
    {
        _campusRoomInput.GetComponent<TMP_InputField>().interactable = false;

        _campusNameDropDown.GetComponent<TMP_Dropdown>().interactable = false;
    }

    public void ResetInteractableField()
    {
        _campusNameDropDown.GetComponent<TMP_Dropdown>().ClearOptions();

        TMP_InputField inputFieldRoom = _campusRoomInput.GetComponent<TMP_InputField>();
        inputFieldRoom.text = "";
        inputFieldRoom.placeholder.GetComponent<TextMeshProUGUI>().text = "";
    }
    #endregion

    #region - Button edit -
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

    [SerializeField] private GameObject _campusRoomInput;
    [SerializeField] private GameObject _campusNameDropDown;

    #endregion
}
