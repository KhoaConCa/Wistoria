using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Globalization;
using System;
using Newtonsoft.Json;

public class DocumentD 
{
    #region -- Properties --

    public string NameFile { get; set; }
    public string Size { get; set; }
    public StudentD Owner { get; set; }

    #endregion
}
