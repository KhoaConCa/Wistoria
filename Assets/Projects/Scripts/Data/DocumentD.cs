using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Globalization;
using System;

public class DocumentD : MonoBehaviour
{
    #region -- Properties --

    public string _id;
    public string NameFile;
    public string Size;
    public string Owner;
    public DateTime createdAt { get; set; }
    public DateTime updatedAt { get; set; }
    public string __v { get; set; }

    #endregion
}
