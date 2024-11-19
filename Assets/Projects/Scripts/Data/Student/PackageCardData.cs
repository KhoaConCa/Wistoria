using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PackageCardData : MonoBehaviour, IPackageData
{
    #region -- Implements --

    public void Initialize(string paper, string price)
    {
        Paper = paper;
        Price = price;

        Debug.Log($"PackageCardData initialized: Paper = {Paper}, Price = {Price}");
    }

    #region -- Properties --

    public string Paper { get; set; } // Implement the Paper property
    public string Price { get; set; } // Implement the Price property

    #endregion

    #endregion

    #region -- Methods --

    #endregion

    #region -- Fields --

    #endregion
}