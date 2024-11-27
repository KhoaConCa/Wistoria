using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Globalization;
using System;


public class SetDataStoreV : MonoBehaviour, IStoreDataSetter
{
    #region -- Implements --

    /// <summary>
    /// Add componet to from prefab selected
    /// </summary>
    /// <param name="nameLocation">Location of Text Name Field</param>
    /// <param name="roomLocation">Location of Text Room Field</param>
    public void AddComponentFromPrefab(Transform paperLocation, Transform priceLocation)
    {
        _storePaper = paperLocation.GetComponent<TextMeshProUGUI>();
        _storePrice = priceLocation.GetComponent<TextMeshProUGUI>();
    }

    public void SetStoreData(IStoreCardData store)
    {
        CultureInfo vietnamCulture = new CultureInfo("vi-VN");

        _storePaper.text = store.Paper.ToString("N0", vietnamCulture);
        _storePrice.text = store.Price.ToString("N0", vietnamCulture) + "đ";
    }

    #endregion

    #region -- Fields --

    [SerializeField] private TextMeshProUGUI _storePaper;
    [SerializeField] private TextMeshProUGUI _storePrice;

    #endregion
}
