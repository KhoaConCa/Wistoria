// CoroutineHandler.cs
using UnityEngine;
using System.Collections;

public class CoroutineHandler : MonoBehaviour
{
    private static CoroutineHandler _instance;

    public static void StartStaticCoroutine(IEnumerator coroutine)
    {
        if (_instance == null)
        {
            var obj = new GameObject("CoroutineHandler");
            _instance = obj.AddComponent<CoroutineHandler>();
            DontDestroyOnLoad(obj);
        }

        _instance.StartCoroutine(coroutine);
    }
}
