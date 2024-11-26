using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Data;

public class MainData<T>
{
    #region -- Implements --

    public void Initialize()
    {
        if (DataRaw != null)
        {
            if (DataRaw["data"] is JArray)
                Data = DataRaw["data"]?.ToObject<List<T>>();
            else if (DataRaw["data"] is JObject)
            {
                var singleData = DataRaw["data"].ToObject<T>();
                Data = new List<T> { singleData };
            }

            // Parse "nextCursor" as a string
            DataID = DataRaw["nextCursor"]?.ToString();
        }
    }

    #region -- Properties --
    [JsonProperty("message")]
    public string Message { get; set; }

    [JsonProperty("status")]
    public int Status { get; set; }

    [JsonProperty("metaData")]
    public JObject DataRaw { get; set; }

    [JsonIgnore]
    public List<T> Data { get; set; }

    [JsonIgnore]
    public string DataID { get; set; }
    #endregion

    #endregion
}
