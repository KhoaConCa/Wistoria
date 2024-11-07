using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Globalization;
using System;

public class DocumentD : MonoBehaviour
{
    #region -- Properties --

    public string _id { get; set; }
    public string NameFile { get; set; }
    public string Size { get; set; }
    public string Owner { get; set; }

    public string FilePath { get; set; }

    public DateTime createdAt { get; set; }
    public DateTime updatedAt { get; set; }
    public string __v { get; set; }

    #endregion
}
