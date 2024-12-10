using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaymentDManager
{
    #region -- Methods --

    public void ProcessPaper()
    {
        if (PaperRaw is string locateAtId)
        {
            PaperID = locateAtId;
            PaperData = null;
        }
        else if (PaperRaw is JObject locateAtObject)
        {
            PaperData = locateAtObject.ToObject<PackageJsonD>();
            PaperID = "";
        }
        else
        {
            PaperID = null;
            PaperData = null;
        }
    }

    public void UpdatePaper(PackageJsonD package)
    {
        PaperData = package;
        PaperID = null;
    }

    public void UpdatePaperID(string package)
    {
        PaperData = null;
        PaperID = package;
    }

    #endregion

    #region -- Properties --

    [JsonProperty("_id")]
    public string Id { get; set; }

    [JsonProperty("Paper")]
    public object PaperRaw { get; set; }

    [JsonIgnore]
    public PackageJsonD PaperData { get; set; }

    [JsonIgnore]
    public string PaperID { get; set; }

    [JsonProperty("Person")]
    public StudentD Student { get; set; }
    public string Status { get; set; }

    [JsonProperty("UpdateAt")]
    public DateTime CompletionTime { get; set; }

    [JsonProperty("createdAt")]
    public DateTime CreatedAt { get; set; }

    [JsonProperty("updatedAt")]
    public DateTime UpdatedAt { get; set; }

    [JsonProperty("__v")]
    public string V { get; private set; } = "0";

    #endregion
}
