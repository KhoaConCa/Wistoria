using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#region -- Class Description --
/// <summary>
/// Data class for storing document details such as paper size, paper type, and page orientation.
/// Provides constructors for default values and custom values.
/// </summary>
#endregion
[System.Serializable]
public class DocumentDetailD
{
    #region -- Fields --

    public string PaperSize = "A4";
    public string PaperType = "Standard";
    public string PageOrientation = "Portrait";

    #endregion

    #region -- Constructors --

    /// <summary>
    /// Default constructor that initializes document properties with default values.
    /// </summary>
    public DocumentDetailD() { }

    /// <summary>
    /// Parameterized constructor that allows setting custom values for document properties.
    /// </summary>
    /// <param name="paperSize">The size of the paper.</param>
    /// <param name="paperType">The type of the paper.</param>
    /// <param name="pageOrientation">The orientation of the page.</param>
    public DocumentDetailD(string paperSize, string paperType, string pageOrientation)
    {
        PaperSize = paperSize;
        PaperType = paperType;
        PageOrientation = pageOrientation;
    }

    #endregion
}
