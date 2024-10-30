using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PaymentD 
{
    #region -- Properties --

    public string _id { get; set; }
    public string Paper { get; set; }
    public string PaymentDay { get; set; }
    public StudentD student { get; set; }
    public string Status { get; set; }
    public string createdAt { get; set; }
    public string updatedAt { get; set; }
    public string __v { get; set; }

    #endregion
}
