using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Data;
using Unity.VisualScripting;

public class MainData<T>
{
    #region -- Methods --

    public void Initialize()
    {
        if (DataRaw != null)
        {
            if (DataRaw["data"] is JArray)
            {
                Data = DataRaw["data"]?.ToObject<List<T>>();
                return;
            }
            else if (DataRaw["data"] is JObject)
            {
                var singleData = DataRaw["data"].ToObject<T>();
                Data = new List<T> { singleData };
                return;
            }

/*            if (DataRaw is JArray)
            {
                Data = DataRaw?.ToObject<List<T>>();
                return;
            }
            else if (DataRaw is JObject)
            {
                var singleData = DataRaw.ToObject<T>();
                Data = new List<T> { singleData };
                return;
            }*/

            // Parse "nextCursor" as a string
            DataID = DataRaw["nextCursor"]?.ToString();
        }
    }

    #endregion

    #region -- Properties --

    [JsonProperty("message")]
    public string Message { get; set; }

    [JsonProperty("status")]
    public int Status { get; set; }

    [JsonProperty("metadata")]
    public JObject DataRaw { get; set; }

    [JsonIgnore]
    public List<T> Data { get; set; }

    [JsonIgnore]
    public string DataID { get; set; }

    #endregion
}
