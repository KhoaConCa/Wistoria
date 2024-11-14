using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPackageData
{
    string Paper { get; set; }
    string Price { get; set; }
    void Initialize(string paper, string price);
}