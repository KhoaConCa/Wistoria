using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Diagnostics;

[System.Serializable]
public class PaymentD 
{
    #region -- Properties --

    public string Paper { get; set; }
    public string Person { get; set; }
    public string Status { get; set; }

    #endregion
}

public class PaymentJsonD
{
    #region -- Methods --

    public void ProgressPerson()
    {
        if (PersonRaw is string personID)
        {
            PersonID = personID;
            Person = null;
        }
        else if (PersonRaw is JObject personD)
        {
            PersonID = "";
            Person = personD.ToObject<StudentD>();
        }
        else
        {
            PersonID = "";
            Person = null;
        }
    }

    #endregion

    #region -- Properties --

    [JsonProperty("_id")]
    public string Id { get; set; }

    [JsonProperty("Paper")]
    public PackageJsonD Paper { get; set; }

    [JsonProperty("Person")]
    public object PersonRaw { get; set; }

    [JsonIgnore]
    public string PersonID { get; set; }

    [JsonIgnore]
    public StudentD Person { get; set; }

    [JsonProperty("Status")]
    public string Status { get; set; }

    [JsonProperty("UpdateAt")]
    public DateTime updateDate { get; set; }

    [JsonProperty("createdAt")]
    public DateTime CreatedAt { get; set; }

    [JsonProperty("updatedAt")]
    public DateTime UpdatedAt { get; set; }

    [JsonProperty("__v")]
    public string V { get; private set; } = "0";

    #endregion
}
