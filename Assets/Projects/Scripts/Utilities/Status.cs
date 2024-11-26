using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Status
{
    #region -- Properties --

    public static List<string> StatusEquipment { get { return _statusEquipment; } private set { _statusEquipment = value; } }

    #endregion

    #region  -- Fields --

    private static List<string> _statusEquipment = new List<string>() { "Available", "Unavailable" };

    #endregion
}
