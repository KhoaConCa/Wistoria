using TMPro;
using UnityEngine;

public class SetInfoCampusV : Singleton<SetInfoCampusV>, ISetDataCampusInfo
{
    #region -- Implements --

    /// <summary>
    /// Set campus name in campus name field
    /// </summary>
    /// <param name="name">Name of Campus</param>
    public void SetCampusNameInput(string name)
    {
        if (nameField != null)
        {
            nameField.text = name;
        }
        else
        {
            Debug.LogWarning("nameField is not assigned.");
        }
    }

    /// <summary>
    /// Set campus room in campus room field.
    /// </summary>
    /// <param name="room">Room of Campus</param>
    public void SetCampusRoomInput(string room)
    {
        if (roomField != null)
        {
            roomField.text = room;
        }
        else
        {
            Debug.LogWarning("roomField is not assigned.");
        }
    }

    #endregion

    #region -- Methods --
    /// <summary>
    /// Get campus name in campus room field.
    /// </summary>
    /// <returns>Name of Campus</returns>
    public string GetCampusNameInput()
    {
        if (nameField != null)
        {
            return nameField.text;
        }
        else
        {
            Debug.LogWarning("nameField is not assigned.");
            return string.Empty;
        }
    }

    /// <summary>
    /// get campus room in campus room field.
    /// </summary>
    /// <returns>Phòng của Campus</returns>
    public string GetCampusRoomInput()
    {
        if (roomField != null)
        {
            return roomField.text;
        }
        else
        {
            Debug.LogWarning("roomField is not assigned.");
            return string.Empty;
        }
    }

    #endregion

    #region -- Fields --

    [SerializeField] private TMP_InputField nameField;
    [SerializeField] private TMP_InputField roomField;

    #endregion
}
