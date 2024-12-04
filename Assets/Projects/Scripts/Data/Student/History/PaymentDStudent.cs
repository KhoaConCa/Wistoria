using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaymentDStudent
{
    #region -- Properties --

    [JsonProperty("_id")]
    public string Id { get; set; }

    [JsonProperty("Paper")]
    public object PaperRaw { get; set; }
    [JsonIgnore]
    public PackageJsonD PaperData { get; set; }

    [JsonIgnore]
    public string PaperID { get; set; }
    public StudentD Person { get; set; }
    public string Status {  get; set; }

    [JsonProperty("createAt")]
    public string CreateAt {  get; set; }

    [JsonProperty("updateAt")]
    public string UpdateAt { get; set; }

    [JsonProperty("UpdateAt")]
    public DateTime CompletionTime { get; set; }

    [JsonProperty("__v")]
    public string V { get; private set; } = "0";

    #endregion

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
}
