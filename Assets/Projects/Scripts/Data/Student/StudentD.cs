using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StudentD 
{
    #region -- Properties --

    [JsonProperty("_id")] 
    public string Id { get; set; }
    public string FullName { get; set; }
    public string StudentID { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }  
    public string Class {  get; set; }
    public int Course { get; set; }
    public int Paper { get; set; }
    public string Status { get; set; }
    [JsonProperty("createdAt")]
    public string CreatedAt { get; private set; }
    [JsonProperty("updatedAt")]
    public string UpdatedAt { get; private set; }

    [JsonProperty("__v")]
    public string V { get; private set; } = "0";

    #endregion
}
