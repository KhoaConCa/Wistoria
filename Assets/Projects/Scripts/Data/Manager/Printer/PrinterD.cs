using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PrinterD
{
    public string _id { get; set; }
    public string PrinterName { get; set; }
    public string PrinterType { get; set; }
    public string Description { get; set; }

    [JsonProperty("LocateAt")]
    public object LocateAtRaw { get; set; } // Có thể là chuỗi hoặc đối tượng

    [JsonIgnore]
    public string LocateAtID { get; private set; } // Gán giá trị sau khi deserialize

    [JsonIgnore]
    public CampusD LocateAt { get; private set; } // Gán giá trị sau khi deserialize

    public int Paper { get; set; }
    public int Ink { get; set; }
    public string Status { get; set; }
    public DateTime createdAt { get; set; }
    public DateTime updatedAt { get; set; }
    public string __v { get; set; }

    // Phương thức xử lý sau khi deserialize
    public void ProcessLocateAt()
    {
        if (LocateAtRaw is string locateAtId)
        {
            LocateAtID = locateAtId; // Nếu là chuỗi, gán vào LocateAtID
            LocateAt = null;         // Không có thông tin đầy đủ
        }
        else if (LocateAtRaw is JObject locateAtObject)
        {
            LocateAt = locateAtObject.ToObject<CampusD>(); // Parse thành CampusD
            LocateAtID = ""; // Lấy _id từ CampusD
        }
        else
        {
            LocateAtID = null;
            LocateAt = null;
        }
    }

    public void UpdateLocateAt(CampusD campus)
    {
        LocateAt = campus;
        LocateAtID = null;
    }

    public void UpdateLocateAtID(string campus)
    {
        LocateAt = null;
        LocateAtID = campus;
    }
}

