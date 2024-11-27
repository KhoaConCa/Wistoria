using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public static class Status
{
    #region -- Properties --

    public static List<string> StatusEquipment { get { return _statusEquipment; } }

    #endregion

    #region  -- Fields --

    private static List<string> _statusEquipment = new List<string>() { "Available", "Unavailable" };

    #endregion
}

public enum PriceFilter
{
    [Description("Tăng dần")]
    Ascending = 1,

    [Description("Giảm dần")]
    Descending = 2
}

public static class EnumProperties
{
    public static string GetEnumDescription(Enum value)
    {
        var field = value.GetType().GetField(value.ToString());
        var attribute = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) as DescriptionAttribute;

        return attribute == null ? value.ToString() : attribute.Description;
    }
}
