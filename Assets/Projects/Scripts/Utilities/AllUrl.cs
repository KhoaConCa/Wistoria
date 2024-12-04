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

    #region -- Queue Manager --

    public static readonly string createQueue = "https://smart-printer-alpha.vercel.app/v1/queue/create";

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

    #region -- Manager --

    public static readonly string findManagerByID = "https://smart-printer-alpha.vercel.app/v1/manager/search/";
    public static readonly string getManagerByID = "https://smart-printer-alpha.vercel.app/v1/manager/getid/";

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
    public static readonly string searchPackageById = "https://smart-printer-alpha.vercel.app/v1/package/search/";
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
    public static readonly string deleteCallback = "https://smart-printer-alpha.vercel.app/v1/momo/delete";
    #endregion

    #endregion

    #region -- Printer Student --
    #region - GET -
    public static readonly string getStudentPrinter = "https://smart-printer-alpha.vercel.app/v1/printer";
    #endregion
    #endregion

    #region -- PrinterDoc --
    #region - POST -
    public static readonly string createPrinterDoc = "https://smart-printer-alpha.vercel.app/v1/printerdoc/create";
    #endregion
    #endregion

    #region -- Document --
    #region - POST -
    public static readonly string createDocument = "https://smart-printer-alpha.vercel.app/v1/document/create";
    #endregion
    #endregion

    #region -- Student Info --
    #region -- POST --

    #endregion

    #region -- GET --
    public static readonly string findStudentById = "https://smart-printer-alpha.vercel.app/v1/student/search/";
    public static readonly string getStudentByID = "https://smart-printer-alpha.vercel.app/v1/student/getid/";
    #endregion

    #region -- PATCH --
    public static readonly string updateStudentById = "https://smart-printer-alpha.vercel.app/v1/student/update";
    #endregion

    #endregion

    #region -- History --

    public static readonly string searchHistoryByID = "https://smart-printer-alpha.vercel.app/v1/printerdoc/search/studentid/status";
    public static readonly string searchPaymentByID = "https://smart-printer-alpha.vercel.app/v1/payment/search/student?id=";

    #endregion

    #region -- Package --
    #region - GET -
    public static readonly string getPackage = "https://smart-printer-alpha.vercel.app/v1/package";
    #endregion
    #endregion

    #region -- Payment --
    #region - POST -
    public static readonly string createPayment = "https://smart-printer-alpha.vercel.app/v1/payment/create";
    #endregion
    #endregion
}
