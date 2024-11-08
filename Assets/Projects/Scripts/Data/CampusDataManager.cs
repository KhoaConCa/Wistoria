using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CampusDataManager : Singleton<CampusDataManager>
{
    #region -- Methods --

    /// <summary>
    /// Adds or updates a campus data entry. If an entry with the same ID exists, it is updated.
    /// Otherwise, a new entry is added
    /// </summary>
    /// <param name="campus">The campus data to add or update</param>
    public void SetCampusData(CampusD campus)
    {
        if (_campusData.ContainsKey(campus._id))
        {
            _campusData[campus._id] = campus;
        }
        else
        {
            _campusData.Add(campus._id, campus);
        }
    }

    /// <summary>
    /// dds or updates a campus data entry. If an entry with the same ID exists, it is updated.
    /// Otherwise, a new entry is added
    /// </summary>
    /// <param name="campuses">List of campuses</param>
    public void SetCampusData(List<CampusD> campuses)
    {
        ClearAllData();

        foreach (var campus in campuses)
        {
            _campusData[campus._id] = campus;
        }
    }

    /// <summary>
    /// Retrieves campus data by ID. Returns null if no data is found
    /// </summary>
    /// <param name="id">The ID of the campus data to retrieve</param>
    /// <returns>The campus data, or null if not found</returns>
    public CampusD GetCampusData(string id)
    {
        _campusData.TryGetValue(id, out CampusD campus);
        return campus;
    }

    /// <summary>
    /// Deletes a campus data entry by ID
    /// </summary>
    /// <param name="id">The ID of the campus data to delete</param>
    public void DeleteCampusData(string id)
    {
        if (_campusData.ContainsKey(id))
        {
            _campusData.Remove(id);
        }
    }

    /// <summary>
    /// Clears all campus data entries
    /// </summary>
    public void ClearAllData()
    {
        _campusData.Clear();
    }

    #endregion

    #region -- Fields --

    private Dictionary<string, CampusD> _campusData = new Dictionary<string, CampusD>();

    #endregion
}
