using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utilities;

public class AddStoreC : MonoBehaviour, IAddStoreCommand
{
    #region -- Implements --

    /// <summary>
    /// Switch UI
    /// </summary>
    public void ClickAddButton()
    {
        SetNewStoreData();
        ClearData();

        StartCoroutine(_addHandler.AddNewStore(_newStore, MainView.OnSuccess, MainView.OnFaild));
    }

    #endregion

    #region -- Methods --

    private void Awake()
    {
        AddComponentAddHandler();
    }

    private void OnEnable()
    {
        SetDataAsDefault();
    }

    private void OnDisable()
    {
        ClearData();
    }

    #region - Add Component -
    private void AddComponentAddHandler()
    {
        if (_addHandler == null)
            _addHandler = gameObject.AddComponent<AddStoreH>();
        else
            Debug.Log("The AddStoreH component already exists");
    }
    #endregion

    #region - Set Data -
    private void SetDataAsDefault()
    {
        _newStore = new StoreD();
    }
    #endregion

    #region - Interactable Field -
    public void ClearData()
    {
        _paperInputField.text = "";
        _priceInputField.text = "";
    }
    #endregion

    #region - Add New Store -
    private void SetNewStoreData()
    {
        _newStore.Paper = int.Parse(_paperInputField.text);
        _newStore.Price = int.Parse(_priceInputField.text);
    }
    #endregion

    #endregion

    #region -- Fields --

    private IAddStoreHandler _addHandler;

    private StoreD _newStore;

    [SerializeField] private TMP_InputField _paperInputField;
    [SerializeField] private TMP_InputField _priceInputField;


    #endregion
}
