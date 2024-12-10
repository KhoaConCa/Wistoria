[System.Serializable]
public class DocumentDetailD : IDocumentDetailData
{
    #region -- Implements --

    public string PaperSize { get; set; } = "A4";
    public string PaperType { get; set; } = "1";
    public string PageOrientation { get; set; } = "Portrait";
    public bool UseDefaultPages { get; set; } = true;
    public string CustomPages { get; set; } = "";
    public bool NoCopies { get; set; } = true;
    public int CustomCopies { get; set; } = 0;

    #endregion

    #region -- Constructors --

    public DocumentDetailD() { }

    public DocumentDetailD(string paperSize, string paperType, string pageOrientation,
                            bool useDefaultPages, string customPages,
                            bool noCopies, int customCopies)
    {
        PaperSize = paperSize;
        PaperType = paperType;
        PageOrientation = pageOrientation;
        UseDefaultPages = useDefaultPages;
        CustomPages = customPages;
        NoCopies = noCopies;
        CustomCopies = customCopies;
    }

    #endregion
}
