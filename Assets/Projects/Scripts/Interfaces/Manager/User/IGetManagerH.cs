using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGetManagerH
{ 
    IEnumerator GetManagerByID(string id, Action<ManagerD> onSuccess, Action<string> onFailed);
}
