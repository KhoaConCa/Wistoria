using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICampusCardData
{
    #region -- Methods --

    void Initialize(CampusD campus);

    #endregion
    
    #region -- Properties --

    string Id { get; set; }
    string Name { get; set; }
    string Room { get; set; }
    string Status { get; set; }

    #endregion
}
