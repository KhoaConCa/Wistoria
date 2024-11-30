using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using Utilities;

public class DetailStoreC : MonoBehaviour, IStoreDetailCommand
{
    #region -- Implements -- 

    /// <summary>
    /// Display Detail Store to the prefab
    /// </summary>
    /// <param name="store">Data of store was clicked</param>
    public void DisplayStoreDetails(IStoreCardData store)
    {
        try
        {
            if (store == null)
            {
                Debug.LogWarning("Store data is null. Cannot display details.");
                return;
            }

            _cardData = store;
        }
        catch (Exception e)
        {
            Debug.LogError(e.Message);
        }
    }

    #endregion

    #region -- Methods -- 

    void Awake()
    {
        AddComponentHandler();
    }

    void OnEnable()
    {
        SetDataStore();
    }

    void OnDisable()
    {
        if (_paperInputField != null && _priceInputField != null)
            ClearDataModify();
    }

    #region -- Add Component --
    private void AddComponentHandler()
    {
        if (_updateHandler == null)
            _updateHandler = gameObject.AddComponent<DetailStoreH>();
        else
            Debug.Log("The DetailStoreH component already exists");
    }
    #endregion

    #region -- Set Data --
    private void SetDataStore()
    {
        _paperInputField.text = _cardData.Paper.ToString();
        _priceInputField.text = _cardData.Price.ToString();
    }
    #endregion

    #region -- Main Event --
    public void OnClickSaveButton()
    {
        SetDataModify();
        GetDataModify();

        StartCoroutine(_updateHandler.UpdateStoreData(_storeData, MainView.OnSuccess, MainView.OnFailed));
    }

    /// <summary>
    /// Set new store data
    /// </summary>
    private void GetDataModify()
    {
        _storeData.Initialize(_cardData);
    }

    private void SetDataModify()
    {
        _cardData.Paper = int.Parse(_paperInputField.text);
        _cardData.Price = int.Parse(_priceInputField.text);
    }

    private void ClearDataModify()
    {
        _paperInputField.text = "";
        _priceInputField.text = "";
    }
    #endregion

    #endregion

    #region -- Fields -- 

    private IStoreCardData _cardData;
    private IDetailStoreUpdateHandler _updateHandler;

    private StoreD _storeData = new StoreD();

    [SerializeField] private TMP_InputField _paperInputField;
    [SerializeField] private TMP_InputField _priceInputField;

    #endregion
}
