using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICampusCardData
{
    string CampusID { get; set; }
    string CampusName { get; set; }
    string CampusRoom { get; set; }
    void Initialize(string id, string name, string room);
}
