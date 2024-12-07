using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.PackageManager.UI;
using UnityEngine;

public static class ProcessingJson
{
    public static T InitializaProperty<T>(object dataRaw)
    {
        if (dataRaw is JObject jData)
            return jData.ToObject<T>();

        return default(T);
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
