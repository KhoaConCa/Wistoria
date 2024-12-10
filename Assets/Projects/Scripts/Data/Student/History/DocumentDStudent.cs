using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DocumentDStudent
{
    #region -- Properties --

    [JsonProperty("_id")]
    public string Id { get; set; }
    [JsonProperty("NameFile")]
    public string Name { get; set; }
    public int Size { get; set; }

    [JsonProperty("Owner")]
    public StudentD Student { get; set; }

    [JsonProperty("createdAt")]
    public DateTime CreatedAt { get; set; }

    [JsonProperty("updatedAt")]
    public DateTime UpdatedAt { get; set; }

    [JsonProperty("__v")]
    public string V { get; private set; } = "0";

    #endregion
}
