using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIStudenPrinterTransformV : MonoBehaviour, IStudentPrinterTransformUI
{
    #region -- Implements --

    /// <summary>
    /// Activates the specified UI object and updates button states.
    /// </summary>
    /// <param name="targetObject">GameObject to activate.</param>
    public void SetActiveObjectUI(GameObject targetObject)
    {
        if (_objectUIs.Count <= 0) SetupDictionary();

        // Create a separate list of keys to iterate safely
        var keys = new List<GameObject>(_objectState.Keys);

        foreach (var key in keys)
        {
            _objectState[key] = false;
        }

        if (_objectState.ContainsKey(targetObject))
        {
            _objectState[targetObject] = true;
        }
        else
        {
            Debug.LogWarning("GameObject is not included in the dictionary.");
        }

        UpdateObjectUI();
    }


    public void SetActiveObjectUI(string targetTag)
    {
        try
        {
            if (_objectUIs.Count <= 0) SetupDictionary();

            GameObject targetObject = null;

            foreach (var item in _objectUIs)
            {
                if (item.tag == targetTag)
                {
                    targetObject = item;
                    break;
                }
            }

            var keys = new List<GameObject>(_objectState.Keys);

            foreach (var key in keys)
                _objectState[key] = false;

            if (_objectState.ContainsKey(targetObject))
                _objectState[targetObject] = true;

            UpdateObjectUI();
        }
        catch (Exception e)
        {
            Debug.LogWarning($"Error in: {e.Message}");
        }

    }

    /// <summary>
    /// Handles the click event to navigate to the Property UI from the Upload UI.
    /// </summary>
    public void OnUploadToProperty()
    {
        GameObject propertyUI = FindTargetObjectByTag("Property");
        if (propertyUI != null)
        {
            SetActiveObjectUI(propertyUI);
            Debug.Log("Navigated to Property UI from Upload UI.");
        }
        else
        {
            Debug.LogError("Property UI not found!");
        }
    }

    /// <summary>
    /// Activates the next UI in the sequence.
    /// </summary>
    public void OnNextButton()
    {
        if (_currentUIIndex < _objectUIs.Count - 1)
        {
            _currentUIIndex++;
            SetActiveObjectUI(_objectUIs[_currentUIIndex]);
        }
        else
        {
            Debug.Log("Already at the last UI.");
        }
    }

    /// <summary>
    /// Activates the previous UI in the sequence.
    /// </summary>
    public void OnBackButton()
    {
        if (_currentUIIndex > 0)
        {
            _currentUIIndex--;
            SetActiveObjectUI(_objectUIs[_currentUIIndex]);
        }
        else
        {
            Debug.Log("Already at the first UI.");
        }
    }

    #endregion

    #region -- Methods --

    private void Start()
    {
        SetupDictionary();
        SetActiveObjectUI(_objectUIs[_currentUIIndex]); // Activate the first UI by default
        SetupButtonListeners();
    }

    /// <summary>
    /// Updates the visibility of all UI objects based on the active state.
    /// </summary>
    private void UpdateObjectUI()
    {
        try
        {
            foreach (var entry in _objectState)
            {
                entry.Key.SetActive(entry.Value);
                Debug.Log($"{entry.Key.name} active: {entry.Value}");
            }
        }
        catch (Exception e)
        {
            Debug.LogWarning($"Error updating UI: {e.Message}");
        }
    }


    /// <summary>
    /// Initializes the dictionary with all child GameObjects of the parent.
    /// </summary>
    private void SetupDictionary()
    {
        try
        {
            _objectState.Clear();
            _objectUIs.Clear(); // Clear the list to avoid duplicates

            foreach (Transform child in _uiParent)
            {
                _objectState[child.gameObject] = false;
                _objectUIs.Add(child.gameObject);
            }
        }
        catch (Exception e)
        {
            Debug.LogWarning($"Error setting up dictionary: {e.Message}");
        }
    }


    /// <summary>
    /// Sets up listeners for the Next and Back buttons and the Upload button.
    /// </summary>
    private void SetupButtonListeners()
    {
        if (nextButton != null)
            nextButton.onClick.AddListener(OnNextButton);
        else
            Debug.LogWarning("Next button is not assigned.");

        if (backButton != null)
            backButton.onClick.AddListener(OnBackButton);
        else
            Debug.LogWarning("Back button is not assigned.");

        if (uploadToPropertyButton != null)
            uploadToPropertyButton.onClick.AddListener(OnUploadToProperty);
        else
            Debug.LogWarning("Upload to Property button is not assigned.");
    }

    /// <summary>
    /// Find target object in the list of GameObjects by tag.
    /// </summary>
    /// <param name="tagTarget">The target tag.</param>
    /// <returns>The target GameObject.</returns>
    public GameObject FindTargetObjectByTag(string tagTarget)
    {
        foreach (var item in _objectUIs)
        {
            if (item.tag == tagTarget)
                return item;
        }

        return null;
    }

    #endregion

    #region -- Fields --

    [Header("Parent Transform for UI Objects")]
    [SerializeField] private Transform _uiParent;

    [Header("Navigation Buttons")]
    [SerializeField] private Button nextButton;
    [SerializeField] private Button backButton;

    [Header("Custom Buttons")]
    [SerializeField] private Button uploadToPropertyButton;

    private int _currentUIIndex = 0; // Tracks the currently active UI

    private List<GameObject> _objectUIs = new List<GameObject>(); // List of UI objects
    private Dictionary<GameObject, bool> _objectState = new Dictionary<GameObject, bool>(); // Tracks active states

    #endregion
}
