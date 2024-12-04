using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Utilities
{
    public class MainHandler : MonoBehaviour
    {
        #region -- Methods --

        #region -- Json Convert --
        /// <summary>
        /// Parse JSON string to handle both single object and array JSON (Deserialize)
        /// </summary>
        /// <typeparam name="T">Type of the data model</typeparam>
        /// <param name="json">JSON string</param>
        /// <returns>List of parsed objects</returns>
        public static List<T> FromJson<T>(string json)
        {
            try
            {
                // With string.StartsWith(char) and string.EndsWith(char), only the first character of the string
                // is compared to the provided character, whereas the string versions of those methods have to do
                // checks about the current StringComparison and CultureInfo. Thus, the char overloads are
                // significantly faster for default comparison scenarios.
                // Instead of using "" -> ''
                if (json.Trim().StartsWith('[') && json.Trim().EndsWith(']'))
                {
                    return JsonConvert.DeserializeObject<List<T>>(json);
                }
                else
                {
                    T singleObject = JsonConvert.DeserializeObject<T>(json);
                    return new List<T> { singleObject };
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Failed to parse JSON: {ex.Message}");
                return new List<T>();
            }
        }

        /// <summary>
        /// Convert object or list of objects to JSON string (Serialize)
        /// </summary>
        /// <typeparam name="T">Type of object</typeparam>
        /// <param name="data">Object or list need to be serialized</param>
        /// <param name="indented">Need to see a beauty json (optional)</param>
        /// <returns>Chuỗi JSON</returns>
        public static string ToJson<T>(T data, bool indented = false)
        {
            try
            {
                return JsonConvert.SerializeObject(data, indented ? Formatting.Indented : Formatting.None);
            }
            catch (Exception ex)
            {
                Debug.LogError($"Failed to convert to JSON: {ex.Message}");
                return string.Empty;
            }
        }
        #endregion

        #region -- Prefab --
        /// <summary>
        /// Find the parent GameObject in the scene by path.
        /// </summary>
        /// <param name="path">Path of the GameObject in the hierarchy</param>
        public static void GetParent(string path)
        {
            _target = GameObject.Find(path);
            if (_target == null)
            {
                Debug.LogError("Parent object not found at path: " + path);
            }
        }

        /// <summary>
        /// Find object children with tag
        /// </summary>
        /// <param name="parentObject">Parent object transform</param>
        /// <param name="tagName">Target tag name</param>
        /// <returns>Target transform</returns>
        public static Transform FindChildObjectsByTag(Transform parentObject, string tagName)
        {
            if (parentObject.tag == tagName)
                return parentObject;

            for (int i = 0; i < parentObject.transform.childCount; i++)
            {
                Transform result = FindChildObjectsByTag(parentObject.transform.GetChild(i), tagName);

                if (result != null)
                    return result;
            }

            return null;
        }

        public static void SpawnPrefabByLabel(AssetLabelReference prefab, GameObject container, Action<GameObject> onSpawned = null)
        {
            var handle = Addressables.LoadAssetAsync<GameObject>(prefab);
            handle.Completed += (AsyncOperationHandle<GameObject> task) =>
            {
                if (task.Status == AsyncOperationStatus.Succeeded)
                {
                    GameObject spawnedPrefab = InstantiatePrefab(task, container);
                    onSpawned?.Invoke(spawnedPrefab);
                }
                else
                {
                    Debug.LogError("Failed to load prefab from addressable");
                }
            };
        }

        private static GameObject InstantiatePrefab(AsyncOperationHandle<GameObject> task, GameObject container)
        {
            if (container == null)
            {
                Debug.LogWarning("Target is null or has been destroyed. Cannot instantiate prefab.");
                return null;
            }

            GameObject spawnedPrefab = Instantiate(task.Result, container.transform);
            LastSpawnedPrefab = spawnedPrefab;

            spawnedPrefab.transform.localPosition = Vector3.zero;
            spawnedPrefab.transform.localScale = Vector3.one;

            spawnedPrefab.SetActive(true);

            _prefabList.Add(spawnedPrefab);

            return spawnedPrefab;
        }

        /// <summary>
        /// Clears all spawned prefabs.
        /// </summary>
        public static void ClearSpawnedPrefabs(bool isDestroy = false)
        {
            List<GameObject> newPrefab = new List<GameObject>();

            foreach (var prefab in _prefabList)
            {
                if (prefab != null)
                {
                    if (isDestroy)
                    {
                        Destroy(prefab);
                    }
                    else if (prefab.layer != LayerMask.NameToLayer("Feature"))
                    {
                        Destroy(prefab);
                    }
                    else
                    {
                        newPrefab.Add(prefab);
                    }
                }
            }

            _prefabList.Clear();
            _prefabList = newPrefab;
        }

        #endregion

        #endregion

        #region -- Properties --

        #region -- Prefab --
        public static GameObject LastSpawnedPrefab { get; set; }

        public static List<GameObject> PrefabList { get { return _prefabList; } }
        #endregion

        #endregion

        #region -- Fields --

        #region -- Prefab --
        private static GameObject _target = null;
        private static List<GameObject> _prefabList = new List<GameObject>();
        #endregion

        #endregion
    }
}

