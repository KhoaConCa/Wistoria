using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerD : MonoBehaviour
{
    #region -- Properties --

    [JsonProperty("_id")]
    public string Id { get; set; }
    public string FullName { get; set; }
    public string ManagerID { get; set; }
    public string DateOfBirth { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public string Status { get; set; }
    [JsonProperty("createdAt")]
    public string CreatedAt { get; private set; }
    [JsonProperty("updatedAt")]
    public string UpdatedAt { get; private set; }

    [JsonProperty("__v")]
    public string V { get; private set; } = "0";

    #endregion
}
