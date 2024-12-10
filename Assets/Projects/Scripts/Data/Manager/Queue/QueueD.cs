using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueueD
{
    #region -- Properties --

    [JsonProperty("_id")]
    public string Id { get; set; }

    #region -- Printer --
    [JsonProperty("Printer")]
    public object PrinterRaw {  get; set; }

    [JsonIgnore] 
    public string printerId { get; set; }

    [JsonIgnore]
    public PrinterD Printer { get; set; }
    #endregion

    #region -- FirstSlot --
    [JsonProperty("FirstSlot")]
    public object FirstSlotRaw { get; set; }

    [JsonIgnore]
    public string FirstSlotId {  get; set; }

    [JsonIgnore]
    public PrinterDocDStudent FirstSlot { get; set; }
    #endregion

    #region -- SecondSlot --
    [JsonProperty("SecondSlot")]
    public object SecondSlotRaw { get; set; }

    [JsonIgnore]
    public string SecondSlotId { get; set; }

    [JsonIgnore]
    public PrinterDocDStudent SecondSlot { get; set; }
    #endregion

    #region -- ThirdSlot --
    [JsonProperty("ThirdSlot")]
    public object ThirdSlotRaw { get; set; }

    [JsonIgnore]
    public string ThirdSlotId { get; set; }

    [JsonIgnore]
    public PrinterDocDStudent ThirdSlot { get; set; }
    #endregion

    public int SlotRemaining { get; set; }

    [JsonProperty("createdAt")]
    public string CreatedAt { get; set; }

    [JsonProperty("updatedAt")]
    public string UpdatedAt { get; set; }

    [JsonProperty("__v")]
    public string V { get; set; } = "0";

    #endregion
}
