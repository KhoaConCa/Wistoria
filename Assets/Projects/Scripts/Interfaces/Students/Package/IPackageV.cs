using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPackageView
{
    /// <summary>
    /// Sets package data to display in the view.
    /// </summary>
    /// <param name="package">The package data to display.</param>
    void SetPackageData(PackageD package);
}
public interface ISpawnPackageView
{
    /// <summary>
    /// Creates a card in the view for a specific package.
    /// </summary>
    /// <param name="package">The package data to display on the card.</param>
    void CreateCard(PackageD package);
}

#region -- Interface for Setting Package Data in View --
/// <summary>
/// Interface for setting specific package data in a view component.
/// </summary>
#endregion
public interface ISetDataPackageView
{
    /// <summary>
    /// Sets the package price display.
    /// </summary>
    /// <param name="price">The price to display for the package.</param>
    void SetPackagePrice(string price);

    /// <summary>
    /// Sets the paper type display for the package.
    /// </summary>
    /// <param name="paper">The paper type to display.</param>
    void SetPackagePaper(string paper);

    /// <summary>
    /// Adds components from a prefab at specified locations for price and paper type.
    /// </summary>
    /// <param name="priceLocation">The location for the price component.</param>
    /// <param name="paperLocation">The location for the paper component.</param>
    void AddComponentFromPrefab(Transform priceLocation, Transform paperLocation);
}