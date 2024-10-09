using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayoutD : MonoBehaviour
{
    #region -- Fields --

    [SerializeField] List<GameObject> layout = new List<GameObject>();

    #endregion

    #region -- Properties --

    public List<GameObject> Layout { get { return layout; } }

    #endregion
}
