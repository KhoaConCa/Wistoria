using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DocumentDetailD : IDocumentDetailData
{
    #region -- Fields --
    public string PaperSize { get; set; } = "A4";
    public string PaperType { get; set; } = "1";
    public string PageOrientation { get; set; } = "Portrait";
    public bool UseDefaultPages { get; set; } = true;  // Default: "Mặc định"
    public string CustomPages { get; set; } = "";     // Custom pages if "Tùy chỉnh số trang" is selected
    public bool NoCopies { get; set; } = true;        // Default: "Không tạo bản sao"
    public int CustomCopies { get; set; } = 0;        // Custom copies if "Tùy chỉnh số bản sao" is selected

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
