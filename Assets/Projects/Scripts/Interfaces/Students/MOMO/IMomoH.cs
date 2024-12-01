using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMomoHandler
{
    IEnumerator CreateMOMOPayment(MomoD momo, Action<MomoD> onSuccess, Action<string> onFailed);
    IEnumerator GetCallback(string orderId, Action<int> onSuccess, Action<string> onFailed);
    IEnumerator DeleteCallback(string orderId, Action<string> onSuccess, Action<string> onFailed);
}