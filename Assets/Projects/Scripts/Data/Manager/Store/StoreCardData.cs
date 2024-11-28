using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Data;

public class StoreCardData : MonoBehaviour, IStoreCardData
{
    #region -- Methods --

    public void Initialize(StoreD store)
    {
        Id = store.Id;
        Paper = store.Paper;
        Price = store.Price;
        Status = store.Status;
    }

    #endregion

    #region -- Properties --

    public string Id { get; set; }
    public int Paper { get; set; }
    public int Price { get; set; }
    public string Status { get; set; }

    #endregion
}
