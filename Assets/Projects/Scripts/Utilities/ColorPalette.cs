using System;
using System.Collections.Generic;

public static class ColorPalette
{
    #region -- Properties --

    public static Dictionary<int, string> Neutral { get; private set; } = new Dictionary<int, string>() 
    {
        [0] = "#FAFAFA",
        [1] = "#D9D9D9",
        [2] = "#8C8C8C",
        [3] = "#141414"
    };

    public static Dictionary<int, string> Primary { get; private set; } = new Dictionary<int, string>()
    {
        [0] = "#57C4FF",
        [1] = "#0496FF",
        [2] = "#005CB3"
    };

    public static Dictionary<int, string> Secondary { get; private set; } = new Dictionary<int, string>()
    {
        [0] = "#FFCE52",
        [1] = "#FEA601",
        [2] = "#B36800"
    };

    public static Dictionary<int, string> Semantics { get; private set; } = new Dictionary<int, string>()
    {
        [0] = "#40A2F8",
        [1] = "#F6B93E",
        [2] = "#3EF89E",
        [3] = "#F93943"
    };

    #endregion
}

