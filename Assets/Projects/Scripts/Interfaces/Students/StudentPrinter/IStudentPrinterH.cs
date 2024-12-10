using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGetStudentPrinterHandler
{
    /// <summary>
    /// Coroutine to get all packages and execute a callback for each package found.
    /// </summary>
    /// <param name="onPackageFound">Callback executed for each package found.</param>
    /// <returns>IEnumerator for coroutine.</returns>
    //IEnumerator GetAllStudentPrinter(Action<StudentPrinterD> onStudentPrinterFound, Action<string> onSuccess, Action<string> onFaild);

    IEnumerator GetAllQueue(Action<QueueD> onQueueFound, Action<string> onSuccess, Action<string> onFailed);
}
