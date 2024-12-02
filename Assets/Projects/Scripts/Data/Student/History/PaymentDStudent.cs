using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaymentDStudent
{

    [JsonProperty("_id")]
    public string Id { get; set; }
    public int Paper { get; set; }
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
}
