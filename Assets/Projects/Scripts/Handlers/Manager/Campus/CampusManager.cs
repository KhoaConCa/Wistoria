using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CampusManager : ICampusManager
{
    #region -- Implements --

    public void AddCampus(string id, CampusD campus)
    {
        if (!_campuses.ContainsKey(id))
        {
            _campuses.Add(id, campus);
            Debug.Log(_campuses[id]);
        }
        else
        {
            Debug.Log("This Campus is already existed in Dictionanry");
        }
    }

    public void RemoveCampus(string id)
    {
        if (_campuses.ContainsKey(id))
        {
            _campuses.Remove(id);
        }
        else
        {
            Debug.Log("This Campus is not existed in Dictionanry");
        }
    }

    public void UpdateCampus(string id, CampusD campus)
    {
        if (_campuses.ContainsKey(id))
        {
            _campuses[id] = campus;
        }
        else
        {
            Debug.Log("This Campus is not existed in Dictionanry");
        }
    }

    public CampusD GetCampus(string id)
    {
        if (_campuses.ContainsKey(id))
        {
            return _campuses[id];
        }

        return null;
    }

    #endregion

    #region -- Methods --



    #endregion

    #region -- Fields --

    private Dictionary<string, CampusD> _campuses = new();

    #endregion
}
