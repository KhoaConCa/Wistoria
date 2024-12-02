using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ILogInHandler
{
    IEnumerator SearchStudentById(string id, Action<string> onSuccess, Action<string> onFailed);
    IEnumerator SearchManagerById(string id, Action<string> onSuccess, Action<string> onFailed);
}
