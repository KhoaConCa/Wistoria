using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using UnityEngine;

#region -- Queue Slot --
public enum QueueSlot
{
    FirstSlot = 3,
    SecondSlot = 2,
    ThirdSlot = 1,
}
#endregion

#region -- Document Type --

public enum Orientation
{
    [Description("Chiều dọc")]
    Portrait = 1,

    [Description("Chiều ngang")]
    Landscape = 2
}

public enum PaperSize
{
    [Description("Giấy A4")]
    A4 = 1,

    [Description("Giấy A3")]
    A3 = 2
}

public enum PaperSide
{
    [Description("Một mặt")]
    One_side = 1,

    [Description("Hai mặt")]
    Two_side = 2
}

#endregion

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
    Done = 2,

    [Description("In thất bại")]
    Failed = 3,
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

    /// <summary>
    /// Trả về mô tả Description của một giá trị Enum
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static string GetEnumDescription(Enum value)
    {
        var field = value.GetType().GetField(value.ToString());
        var attribute = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) as DescriptionAttribute;

        return attribute == null ? value.ToString() : attribute.Description;
    }

    /// <summary>
    /// Chuyển đổi tất cả các giá trị của một Enum sang danh sách các chuỗi mô tả 
    /// (Description), và thêm một mục tùy chọn vào đầu danh sách (nếu được chỉ định).
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="defaultSort"></param>
    /// <returns></returns>
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

    /// <summary>
    /// Trả về giá trị Enum tương ứng với mô tả (Description). Nếu không tìm thấy, trả về null.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="description"></param>
    /// <returns></returns>
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

    /// <summary>
    /// Trả về giá trị nguyên (int) của Enum bằng cách truyền vào tên Enum (name). 
    /// Nếu không tìm thấy, trả về -1.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="name"></param>
    /// <returns></returns>
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

