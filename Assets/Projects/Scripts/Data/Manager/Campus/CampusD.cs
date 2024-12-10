using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CampusD
{
    #region  -- Methods --

    public void Initialize(ICampusCardData cardCampus)
    {
        Id = cardCampus.Id;
        Name = cardCampus.Name;
        Room = cardCampus.Room;
        Status = cardCampus.Status;
    }

    #endregion

    #region -- Properties --

    [JsonProperty("_id")]
    public string Id { get; set; }

    [JsonProperty("CampusName")]
    public string Name { get; set; }

    [JsonProperty("Room")]
    public string Room { get; set; }

    [JsonProperty("Status")]
    public string Status {  get; set; }

    [JsonProperty("createdAt")]
    public DateTime CreatedAt { get; set; }

    [JsonProperty("updatedAt")]
    public DateTime UpdatedAt { get; set; }

    [JsonProperty("__v")]
    public string V { get; private set; } = "0";

    #endregion
}
