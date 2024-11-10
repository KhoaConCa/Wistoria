using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CampusD
{
    #region -- Properties --

    public string _id { get; set; }
    public string CampusName { get; set; }
    public string Room { get; set; }
    public DateTime createdAt { get; set; }
    public DateTime updatedAt { get; set; }
    public string __v { get; set; }

    #endregion
}
