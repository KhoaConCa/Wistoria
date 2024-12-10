using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreD
{
    #region  -- Methods --

    public void Initialize(IStoreCardData cardStore)
    {
        Id = cardStore.Id;

        Paper = cardStore.Paper;

        Price = cardStore.Price;

        Status = cardStore.Status;
    }

    #endregion

    #region -- Properties --

    [JsonProperty("_id")]
    public string Id { get; set; }

    [JsonProperty("Paper")]
    public int Paper { get; set; }

    [JsonProperty("Price")]
    public int Price { get; set; }

    [JsonProperty("Status")]
    public string Status { get; set; }

    [JsonProperty("createdAt")]
    public DateTime CreatedAt { get; set; }

    [JsonProperty("updatedAt")]
    public DateTime UpdatedAt { get; set; }

    [JsonProperty("__v")]
    public string V { get; private set; } = "0";

    #endregion
}
