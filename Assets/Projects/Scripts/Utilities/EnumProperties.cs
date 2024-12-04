using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using UnityEngine;

#region -- Status --
public enum GeneralStatus
{
    [Description("Kích hoạt")]
    Active = 1,

    [Description("Vô hiệu hóa")]
    Inactive = 2
}

public enum PaymentStatus
{
    [Description("Thành công")]
    Success = 1,

    [Description("Thất bại")]
    Failed = 2
}

public enum PrinterDocStatus
{
    [Description("Đang in")]
    In_Progress = 1,

    [Description("Đã in")]
    Done = 2
}

public enum PrinterStatus
{
    [Description("Đang hoạt động")]
    Available = 1,

    [Description("Không hoạt động")]
    Unavailable = 2
}

public enum Price
{
    [Description("Tăng dần")]
    Ascending = 1,

    [Description("Giảm dần")]
    Descending = 2
}
#endregion

public static class EnumProperties
{
    #region -- Methods --

    public static string GetEnumDescription(Enum value)
    {
        var field = value.GetType().GetField(value.ToString());
        var attribute = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) as DescriptionAttribute;

        return attribute == null ? value.ToString() : attribute.Description;
    }

    public static List<string> ConvertEnumToList<T>(string defaultSort = null) where T : Enum
    {
        List<string> descriptions = Enum.GetValues(typeof(T))
            .Cast<T>()
            .Select(e => EnumProperties.GetEnumDescription(e))
            .ToList();

        if (!string.IsNullOrEmpty(defaultSort)) 
            descriptions.Insert(0, defaultSort);

        return descriptions;
    }

    public static T? GetEnumByDescription<T>(string description) where T : struct, Enum
    {
        foreach (var value in Enum.GetValues(typeof(T)).Cast<T>())
        {
            if (GetEnumDescription(value).Equals(description, StringComparison.OrdinalIgnoreCase))
            {
                return value;
            }
        }

        return null;
    }

    public static int GetEnumIdByName<T>(string name) where T : struct, Enum
    {
        if (Enum.TryParse(typeof(T), name, true, out var result) && result != null)
        {
            return Convert.ToInt32(result) - 1;
        }

        return -1;
    }

    #endregion
}

