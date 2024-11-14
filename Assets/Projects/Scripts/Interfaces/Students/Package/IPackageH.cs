using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
public interface IPackageClickH
{
    void ClickPackage();
    void SetUpButton();
}
