using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StudentDManager
{
    #region -- Methods --

    public void Initialize()
    {

    }

    #endregion

    #region -- Properties --

    [JsonProperty("_id")]
    public string Id { get; set; }

    [JsonProperty("FullName")]
    public string Name { get; set; }
    public string DateOfBirth { get; set; }

    [JsonProperty("PhoneNumber")]
    public string Phone { get; set; }
    public string Email { get; set; }
    public string Class { get; set; }
    public int Course { get; set; }
    public int Paper { get; set; }
    public string Status { get; set; }

    [JsonProperty("createdAt")]
    public DateTime CreatedAt { get; set; }

    [JsonProperty("updatedAt")]
    public DateTime UpdatedAt { get; set; }

    [JsonProperty("__v")]
    public string V { get; private set; } = "0";

    #endregion
}
