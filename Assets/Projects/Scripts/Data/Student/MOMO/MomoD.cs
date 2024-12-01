using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MomoD
{
    #region  -- Methods --

    public void Initialize(IMomoCardData momo)
    {
        Id = momo.Id;

        PartnerCode = momo.PartnerCode;

        OrderID = momo.OrderID;

        RequestID = momo.RequestID;

        Amount = momo.Amount;

        OrderInfo = momo.OrderInfo;

        OrderType = momo.OrderType;

        TransID = momo.TransID;

        ResultCode = momo.ResultCode;

        Message = momo.Message;

        PayType = momo.PayType;

        ResponseTime = momo.ResponseTime;

        ExtraData = momo.ExtraData;

        Signature = momo.Signature;

        PayURL = momo.PayURL;
    }

    #endregion

    #region -- Properties --

    [JsonProperty("_id")]
    [JsonIgnore]
    public string Id { get; set; }

    [JsonProperty("partnerCode")]
    [JsonIgnore]
    public string PartnerCode { get; set; }

    [JsonProperty("orderId")]
    public string OrderID { get; set; }

    [JsonProperty("requestId")]
    [JsonIgnore]
    public string RequestID { get; set; }

    [JsonProperty("amount")]
    public int Amount { get; set; }

    [JsonProperty("orderInfo")]
    [JsonIgnore]
    public string OrderInfo { get; set; }

    [JsonProperty("orderType")]
    [JsonIgnore]
    public string OrderType { get; set; }

    [JsonProperty("transId")]
    [JsonIgnore]
    public long TransID { get; set; }

    [JsonProperty("resultCode")]
    public int ResultCode { get; set; }

    [JsonProperty("message")]
    [JsonIgnore]
    public string Message { get; set; }

    [JsonProperty("payType")]
    [JsonIgnore]
    public string PayType { get; set; }

    [JsonProperty("responseTime")]
    [JsonIgnore]
    public int ResponseTime { get; set; }

    [JsonProperty("extraData")]
    [JsonIgnore]
    public string ExtraData { get; set; }

    [JsonProperty("signature")]
    [JsonIgnore]
    public string Signature { get; set; }

    [JsonProperty("createdAt")]
    public DateTime CreatedAt { get; set; }

    [JsonProperty("updatedAt")]
    public DateTime UpdatedAt { get; set; }

    [JsonProperty("payURL")]
    public string PayURL { get; set; }

    [JsonProperty("__v")]
    public string V { get; private set; } = "0";

    #endregion
}
