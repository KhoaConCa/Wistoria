using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueueD
{
    #region -- Properties --

    public string Id { get; set; }
    public PrinterD Printer { get; set; }
    public string FirstSlot { get; set; }
    public string FirstSlotTime { get; set; }
    public string SecondSlot { get; set; }
    public string SecondSlotTime { get; set; }
    public string ThirdSlot { get; set; }
    public string ThirdSlotTime { get; set; }
    public string Status { get; set; }

    [JsonProperty("createdAt")]
    public string CreatedAt { get; set; }

    [JsonProperty("updatedAt")]
    public string UpdatedAt { get; set; }

    [JsonProperty("__v")]
    public string V { get; set; } = "0";

    #endregion
}
