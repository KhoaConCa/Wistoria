using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DocumentDetailD : IDocumentDetailData
{
    #region -- Fields --


    public string PaperSize { get; set; } = "A4";
    public string PaperType { get; set; } = "Standard";
    public string PageOrientation { get; set; } = "Portrait";

    #endregion

    #region -- Constructors --

    public DocumentDetailD() { }

    public DocumentDetailD(string paperSize, string paperType, string pageOrientation)
    {
        PaperSize = paperSize;
        PaperType = paperType;
        PageOrientation = pageOrientation;
    }

    #endregion
}
