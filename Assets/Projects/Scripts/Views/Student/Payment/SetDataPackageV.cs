using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using System.Globalization;


public class SetDataPackageV : MonoBehaviour, ISetDataPackageView
{
    #region -- Implements --

    public void SetPackagePrice(string price)
    {
        int priceInt = int.Parse(price);

        CultureInfo vietnamCulture = new CultureInfo("vi-VN");
        packagePrice.text = priceInt.ToString("N0", vietnamCulture) + " đ";
    }

    public void SetPackagePaper(string paper)
    {
        packagePaper.text = $"Gói {paper} giấy";
    }

    #endregion

    #region -- Methods --

    /// <summary>
    /// 
    /// </summary>
    /// <param name="priceLocation"></param>
    /// <param name="paperLocation"></param>
    public void AddComponentFromPrefab(Transform priceLocation, Transform paperLocation)
    {
        packagePrice = priceLocation.GetComponent<TextMeshProUGUI>();
        packagePaper = paperLocation.GetComponent<TextMeshProUGUI>();
    }

    #endregion

    #region -- Fields --

    public TextMeshProUGUI packagePrice;
    public TextMeshProUGUI packagePaper;

    #endregion
}
