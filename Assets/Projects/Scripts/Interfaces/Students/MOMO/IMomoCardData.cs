using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMomoCardData
{
    #region -- Methods --

    void Initialize(MomoD momo);

    #endregion

    #region -- Properties --

     string Id { get; set; }

     string PartnerCode { get; set; }

     string OrderID { get; set; }

     string RequestID { get; set; }

     int Amount { get; set; }

     string OrderInfo { get; set; }

     string OrderType { get; set; }

     long TransID { get; set; }

     int ResultCode { get; set; }

     string Message { get; set; }

     string PayType { get; set; }

     int ResponseTime { get; set; }

     string ExtraData { get; set; }

     string Signature { get; set; }
     
     string PayURL { get; set; }

    #endregion
}
