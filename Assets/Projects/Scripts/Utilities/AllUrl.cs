using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static System.Net.WebRequestMethods;

public static class AllUrlManager
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

    #region -- Store Manager --

    #region - POST -
    public static readonly string createStore = "https://smart-printer-alpha.vercel.app/v1/package/create";
    #endregion

    #region - GET -
    public static readonly string getAllStore = "https://smart-printer-alpha.vercel.app/v1/package";
    #endregion

    #region - PATCH -
    public static readonly string updateStore = "https://smart-printer-alpha.vercel.app/v1/package/update";
    #endregion

    #endregion

    #region -- History Manager --

    #region - GET -
    public static readonly string getAllPayment = "https://smart-printer-alpha.vercel.app/v1/payment";
    public static readonly string getAllHistory = "https://smart-printer-alpha.vercel.app/v1/printerdoc";
    #endregion

    #endregion
}

public static class AllUrlStudent
{
    #region -- Store Student --

    #region - POST -
    public static readonly string createStore = "https://smart-printer-alpha.vercel.app/v1/package/create";
    #endregion

    #region - GET -
    public static readonly string getAllStore = "https://smart-printer-alpha.vercel.app/v1/package";
    #endregion

    #region - PATCH -
    public static readonly string updateStore = "https://smart-printer-alpha.vercel.app/v1/package/update";
    #endregion

    #endregion

    #region -- MOMO --

    #region - POST -
    public static readonly string createMOMO = "https://smart-printer-alpha.vercel.app/v1/momo/create";
    #endregion

    #region - GET -
    public static readonly string getMOMO = "https://smart-printer-alpha.vercel.app/v1/momo/search";
    #endregion

    #region - DELETE -
    public static readonly string deleteCallback = "https://smart-printer-alpha.vercel.app/v1/package/delete";
    #endregion

    #endregion
}

public static class StudentID
{
    public static string STUDENT_ID;
}

public static class ManagerID
{
    public static string MANAGER_ID;
}
