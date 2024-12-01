using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaymentDManager
{
    #region -- Methods --

    public void Initialize()
    {

    }

    #endregion

    #region -- Properties --

    [JsonProperty("_id")]
    public string Id { get; set; }
    public int Paper { get; set; }

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
