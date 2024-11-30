using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMomoHandler
{
    IEnumerator CreateMOMOPayment(MomoD momo, Action<MomoD> onSuccess, Action<string> onFaild);
    IEnumerator GetCallback(string orderId, Action<MomoD> onMOMOFound, Action<string> onSuccess, Action<string> onFaild);
    IEnumerator DeleteCallback(string orderId, Action<string> onSuccess, Action<string> onFaild);
}