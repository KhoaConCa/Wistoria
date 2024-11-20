using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static System.Net.WebRequestMethods;

public static class AllUrl
{
    #region -- Campus --

    #region - POST -
    public static readonly string createCampus = "https://server-wistoria-api.vercel.app/campus/create";
    #endregion

    #region - GET -
    public static readonly string getCampusUniqueNames = "https://server-wistoria-api.vercel.app/campus/unique-names";
    public static readonly string getCampusUniqueRooms = "https://server-wistoria-api.vercel.app/campus/unique-rooms";
    public static readonly string searchCampusByName = "https://server-wistoria-api.vercel.app/campus/search/name";
    public static readonly string getAllCampus = "https://server-wistoria-api.vercel.app/campus";
    #endregion

    #region - PATCH -
    public static readonly string updateCampus = "https://server-wistoria-api.vercel.app/campus/update";
    #endregion

    #endregion
}
