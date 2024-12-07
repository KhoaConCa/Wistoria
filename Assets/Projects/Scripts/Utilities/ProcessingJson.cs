using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.PackageManager.UI;
using UnityEngine;

public static class ProcessingJson
{
    public static T InitializaProperty<T>(object dataRaw)
    {
        return dataRaw.ConvertTo<T>();
    }

    public static string RemoveNullJson<T>(T data)
    {
        var settings = new JsonSerializerSettings
        {
            NullValueHandling = NullValueHandling.Ignore
        };

        return JsonConvert.SerializeObject(data, settings);
    }
}
