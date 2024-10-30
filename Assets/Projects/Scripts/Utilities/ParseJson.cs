using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.AddressableAssets;

public class ParseJson
{
    /// <summary>
    /// Parse JSON string to handle both single object and array JSON (Deserialize)
    /// </summary>
    /// <typeparam name="T">Type of the data model</typeparam>
    /// <param name="json">JSON string</param>
    /// <returns>List of parsed objects</returns>
    public static List<T> FromJson<T>(string json)
    {
        try
        {
            if (json.Trim().StartsWith("[") && json.Trim().EndsWith("]"))
            {
                return JsonConvert.DeserializeObject<List<T>>(json);
            }
            else
            {
                T singleObject = JsonConvert.DeserializeObject<T>(json);
                return new List<T> { singleObject };
            }
        }
        catch (Exception ex)
        {
            Debug.LogError($"Failed to parse JSON: {ex.Message}");
            return new List<T>();
        }
    }
}
