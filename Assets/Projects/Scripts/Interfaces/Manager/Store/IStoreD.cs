using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IStoreCardData
{
    #region -- Methods --

    void Initialize(StoreD store);

    #endregion

    #region -- Properties --

    string Id { get; set; }
    int Paper { get; set; }
    int Price { get; set; }
    string Status { get; set; }

    #endregion
}
