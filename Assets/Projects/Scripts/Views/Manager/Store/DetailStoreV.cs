using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DetailStoreV : MonoBehaviour
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
        _paperInputField.GetComponent<TMP_InputField>().interactable = true;
        _priceInputField.GetComponent<TMP_InputField>().interactable = true;
    }

    public void DisableInteractableField()
    {
        _paperInputField.GetComponent<TMP_InputField>().interactable = false;
        _priceInputField.GetComponent<TMP_InputField>().interactable = false;
    }

    public void ResetInteractableField()
    {
        _priceInputField.GetComponent<TMP_InputField>().text = "";
        _paperInputField.GetComponent<TMP_InputField>().text = "";

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

    [SerializeField] private GameObject _paperInputField;
    [SerializeField] private GameObject _priceInputField;

    #endregion
}
