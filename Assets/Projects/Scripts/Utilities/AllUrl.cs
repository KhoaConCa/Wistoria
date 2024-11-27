using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static System.Net.WebRequestMethods;

public static class AllUrl
{
    #region -- Campus Manager --

    #region - POST -
    public static readonly string createCampus = "https://smart-printer-alpha.vercel.app/v1/campus/create";
    #endregion

    #region - GET -
    public static readonly string getAllCampus = "https://smart-printer-alpha.vercel.app/v1/campus";
    public static readonly string getCampusUniqueNames = "https://smart-printer-alpha.vercel.app/v1/campus/search/unique-name";
    public static readonly string getCampusUniqueRooms = "https://smart-printer-alpha.vercel.app/v1/campus/search/unique-room";
    public static readonly string searchCampusByName = "https://smart-printer-alpha.vercel.app/v1/campus/search/name";
    #endregion

    #region - PATCH -
    public static readonly string updateCampus = "https://smart-printer-alpha.vercel.app/v1/campus/update";
    #endregion

    #endregion

    #region -- Printer Manager --

    #region - POST -
    public static readonly string createPrinter = "https://smart-printer-alpha.vercel.app/v1/printer/create";
    #endregion

    #region - GET -
    public static readonly string searchPrinterByName = "https://smart-printer-alpha.vercel.app/v1/printer/search/name";
    public static readonly string getAllPrinter = "https://smart-printer-alpha.vercel.app/v1/printer";
    #endregion

    #region - PATCH -
    public static readonly string updatePrinter = "https://smart-printer-alpha.vercel.app/v1/printer/update";
    #endregion

    #endregion

}
