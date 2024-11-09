using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#region -- Interface for Package View --
/// <summary>
/// Interface for setting package data in a view.
/// </summary>
#endregion
public interface IPackageView
{
    /// <summary>
    /// Sets package data to display in the view.
    /// </summary>
    /// <param name="package">The package data to display.</param>
    void SetPackageData(PackageD package);
}

#region -- Interface for Package Retrieval Handler --
/// <summary>
/// Interface for handling package retrieval operations.
/// </summary>
#endregion
public interface IGetPackageHandler
{
    /// <summary>
    /// Coroutine to get all packages and execute a callback for each package found.
    /// </summary>
    /// <param name="onPackageFound">Callback executed for each package found.</param>
    /// <returns>IEnumerator for coroutine.</returns>
    IEnumerator GetAllPackage(Action<PackageD> onPackageFound);

    /// <summary>
    /// Coroutine to get a specific package by paper type and execute a callback when found.
    /// </summary>
    /// <param name="packagePaper">The paper type of the package to find.</param>
    /// <param name="onPackageFound">Callback executed when the package is found.</param>
    /// <returns>IEnumerator for coroutine.</returns>
    IEnumerator GetPackage(string packagePaper, Action<PackageD> onPackageFound);
}

#region -- Interface for Package Command --
/// <summary>
/// Interface for commands related to package operations.
/// Placeholder for defining package-related commands.
/// </summary>
#endregion
public interface IGetPackageCommand
{
    // Define package-related commands here if needed
}

#region -- Interface for Package View Spawner --
/// <summary>
/// Interface for creating package view cards in the UI.
/// </summary>
#endregion
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
