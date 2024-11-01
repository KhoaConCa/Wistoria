using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;


public class SetDataPackageV : MonoBehaviour, ISetDataPackageView
{
    #region -- Implements --

    public void SetPackagePrice(string price)
    {
        packagePrice.text = price;
    }

    public void SetPackagePaper(string paper)
    {
        packagePaper.text = paper;
    }

    #endregion

    #region -- Methods --

    /// <summary>
    /// 
    /// </summary>
    /// <param name="priceLocation"></param>
    /// <param name="packageLocation"></param>
    public void AddComponentFromPrefab(Transform priceLocation, Transform packageLocation)
    {
        packagePrice = priceLocation.GetComponent<TextMeshProUGUI>();
        packagePaper = packageLocation.GetComponent<TextMeshProUGUI>();
    }

    #endregion

    #region -- Fields --

    public TextMeshProUGUI packagePrice;
    public TextMeshProUGUI packagePaper;

    #endregion
}