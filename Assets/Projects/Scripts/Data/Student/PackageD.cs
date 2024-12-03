using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Globalization;
using Newtonsoft.Json;

public class PackageD
{
    #region -- Properties --

    public string Paper { get; set; }
    public string Price { get; set; }

    #endregion
}

public class PackageJsonD
{
    [JsonProperty("_id")]
    public string Id { get; set; }
    public int Paper {  get; set; }
    public int Price { get; set; }
    public string Status {  get; set; }

    [JsonProperty("createAt")]
    public DateTime CreateAt {  get; set; }

    [JsonProperty("updateAt")]
    public DateTime UpdateAt { get; set; }

    [JsonProperty("__v")]
    public string V { get; private set; } = "0";
}
