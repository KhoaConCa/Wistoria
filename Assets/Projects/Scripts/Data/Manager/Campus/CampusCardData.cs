using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Data;

public class CampusCardData : MonoBehaviour, ICampusCardData
{
    #region -- Methods --

    public void Initialize(CampusD campus)
    {
        Id = campus.Id;
        Name = campus.Name;
        Room = campus.Room;
        Status = campus.Status;
    }

    #endregion

    #region -- Properties --

    public string Id {  get; set; }
    public string Name { get; set; }
    public string Room {  get; set; }
    public string Status {  get; set; }

    #endregion
}
