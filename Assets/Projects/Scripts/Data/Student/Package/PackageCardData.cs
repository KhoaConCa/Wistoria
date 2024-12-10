using UnityEngine;

public class PackageCardData : MonoBehaviour, IPackageData
{
    #region -- Implements --

    public void Initialize(string paper, string price)
    {
        Paper = paper;
        Price = price;
    }

    #region -- Properties --
    public string Paper { get; set; }
    public string Price { get; set; }
    #endregion

    #endregion
}