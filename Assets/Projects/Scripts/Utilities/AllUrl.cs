using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static System.Net.WebRequestMethods;

public static class AllUrl
{
    #region -- Campus Manager --

    #region - POST -
    public static readonly string createCampus = "https://server-wistoria-api.vercel.app/campus/create";
    #endregion

    #region - GET -
    public static readonly string getAllCampus = "https://server-wistoria-api.vercel.app/campus";
    public static readonly string getCampusUniqueNames = "https://server-wistoria-api.vercel.app/campus/unique-names";
    public static readonly string getCampusUniqueRooms = "https://server-wistoria-api.vercel.app/campus/unique-rooms";
    public static readonly string searchCampusByName = "https://server-wistoria-api.vercel.app/campus/search/name";
    #endregion

    #region - PATCH -
    public static readonly string updateCampus = "https://server-wistoria-api.vercel.app/campus/update";
    #endregion

    #endregion

    #region -- Printer Manager --

    #region - POST -
    public static readonly string createPrinter = "https://server-wistoria-api.vercel.app/printer/create";
    #endregion

    #region - GET -
    public static readonly string searchPrinterByName = "https://server-wistoria-api.vercel.app/printer/search/name";
    public static readonly string getAllPrinter = "https://server-wistoria-api.vercel.app/printer";
    #endregion

    #region - PATCH -
    public static readonly string updatePrinter = "https://server-wistoria-api.vercel.app/printer/update";
    #endregion

    #endregion

}
