using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    #region -- Methods --

    /// <summary>
    /// Gets the singleton instance of the class. If an instance does not already exist, 
    /// it searches for an existing instance in the scene. If no instance is found, it 
    /// creates a new GameObject with the singleton component.
    /// </summary>
    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<T>();

                if (_instance == null)
                {
                    GameObject singleton = new GameObject(typeof(T).Name);
                    _instance = singleton.AddComponent<T>();
                    DontDestroyOnLoad(singleton);
                }
            }
            return _instance;
        }
    }

    /// <summary>
    /// Called when the singleton instance is first initialized.
    /// Ensures that only one instance of the singleton exists. If an instance already exists, 
    /// this GameObject is destroyed. Optionally keeps the GameObject and its root persistent 
    /// across scenes if <c>_dontDestroyOnLoad</c> is set to true.
    /// </summary>
    protected virtual void Awake()
    {
        if (_instance == null)
        {
            _instance = this as T;

            if (_dontDestroyOnLoad)
            {
                var root = transform.root;

                if (root != transform)
                {
                    DontDestroyOnLoad(root);
                }
                else
                {
                    DontDestroyOnLoad(this.gameObject);
                }
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    #endregion

    #region -- Fields --

    /// <summary>
    /// Specifies whether the singleton instance should be kept persistent across scenes.
    /// If true, the instance will not be destroyed when loading a new scene.
    /// </summary>
    [SerializeField] protected bool _dontDestroyOnLoad = true;

    /// <summary>
    /// Holds the instance of the singleton class. Ensures that only one instance of the
    /// class exists in the application.
    /// </summary>
    private static T _instance;

    #endregion
}
