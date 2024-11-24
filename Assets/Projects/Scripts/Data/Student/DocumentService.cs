using UnityEngine;

public static class DocumentService 
{
    public static string DocumentId { get; set; } = string.Empty;
    public static string PaperSize { get; set; } = string.Empty;
    public static string Orientation { get; set; } = string.Empty;
    public static string Side { get; set; } = string.Empty;
    public static int PageBegin { get; set; } = 0;
    public static int PageEnd { get; set; } = 0;
    public static int Copies { get; set; } = 0;
    public static bool Color { get; set; } = false;
}
