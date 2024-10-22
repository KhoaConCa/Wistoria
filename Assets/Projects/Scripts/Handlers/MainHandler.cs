using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainHandler : MonoBehaviour
{
    /// <summary>
    /// Rebuild FromJson function to be able with array JSON
    /// </summary>
    /// <typeparam name="T">Any Data file</typeparam>
    /// <param name="json">JSON as string</param>
    /// <returns>Array of data</returns>
    public static T[] FromJson<T>(string json)
    {
        // Wrap the JSON array in a root object called "array"
        string newJson = $"{{\"array\":{json}}}";
        Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(newJson);
        return wrapper.array;
    }

    [Serializable]
    private class Wrapper<T>
    {
        public T[] array;
    }
}

