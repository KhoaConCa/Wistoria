using UnityEngine;

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
    public string Paper { get; set; }
    public string Price { get; set; }
    #endregion

    #endregion
}