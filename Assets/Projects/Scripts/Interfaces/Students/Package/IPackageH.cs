using System;
using System.Collections;

public interface IGetPackageHandler
{
    /// <summary>
    /// Coroutine to get all packages and execute a callback for each package found.
    /// </summary>
    /// <param name="onPackageFound">Callback executed for each package found.</param>
    /// <returns>IEnumerator for coroutine.</returns>
    IEnumerator GetAllPackage(Action<PackageD> onPackageFound, Action<string> onSuccess, Action<string> onFaild);

}
public interface IPackageClickH
{
    void ClickPackage();
    void SetUpButton();
}
