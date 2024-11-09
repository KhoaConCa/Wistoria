using UnityEngine;
using System.Collections;

#region -- Class Description --
/// <summary>
/// Singleton handler for managing coroutines across scenes in a static context.
/// Ensures that a coroutine can be started from any part of the codebase without needing a specific MonoBehaviour instance.
/// </summary>
#endregion
public class CoroutineHandler : MonoBehaviour
{
    #region -- Public Static Methods --

    /// <summary>
    /// Starts a coroutine in a static context. Creates a new GameObject if none exists for handling the coroutine.
    /// </summary>
    /// <param name="coroutine">The IEnumerator coroutine to run.</param>
    public static void StartStaticCoroutine(IEnumerator coroutine)
    {
        if (_instance == null)
        {
            var obj = new GameObject("CoroutineHandler");
            _instance = obj.AddComponent<CoroutineHandler>();
            DontDestroyOnLoad(obj); // Ensures the GameObject is not destroyed when loading new scenes
        }

        _instance.StartCoroutine(coroutine);
    }

    #endregion

    #region -- Fields --

    private static CoroutineHandler _instance;

    #endregion
}
