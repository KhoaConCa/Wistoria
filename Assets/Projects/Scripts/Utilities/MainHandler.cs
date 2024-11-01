using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
                // checks about the current StringComparison and CultureInfo. Thus, the char overloads are significantly
                // faster for default comparison scenarios.
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
        /// <typeparam name="T">T   ype of object</typeparam>
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
            target = GameObject.Find(path);
            if (target == null)
            {
                Debug.LogError("Parent object not found at path: " + path);
            }
        }

        /// <summary>
        /// Loads and spawns a prefab using Addressables asynchronously.
        /// </summary>
        /// <param name="prefab">Address of the prefab in Addressables</param>
        /// <param name="path">Path to parent the prefab</param>
        /// <param name="onSpawned">Callback after prefab has been spawned</param>
        public static void LoadAndSpawnPrefab(AssetLabelReference prefab, string path, Action<GameObject> onSpawned = null)
        {
            GetParent(path);

            var handle = Addressables.LoadAssetAsync<GameObject>(prefab);
            handle.Completed += (AsyncOperationHandle<GameObject> task) =>
            {
                if (task.Status == AsyncOperationStatus.Succeeded)
                {
                    GameObject spawnedPrefab = InstantiatePrefab(task);
                    onSpawned?.Invoke(spawnedPrefab);
                }
                else
                {
                    Debug.LogError("Failed to load prefab from addressable");
                }
            };
        }

        /// <summary>
        /// Instantiates the prefab and sets LastSpawnedPrefab.
        /// </summary>
        /// <param name="task">AsyncOperationHandle containing the prefab</param>
        /// <returns>The spawned prefab GameObject</returns>
        private static GameObject InstantiatePrefab(AsyncOperationHandle<GameObject> task)
        {
            GameObject spawnedPrefab = Instantiate(task.Result, target?.transform);

            spawnedPrefab.transform.localPosition = Vector3.zero;
            spawnedPrefab.transform.localScale = Vector3.one;

            spawnedPrefab.SetActive(true);

            _prefabList.Add(spawnedPrefab);
            LastSpawnedPrefab = spawnedPrefab;

            return spawnedPrefab;
        }

        /// <summary>
        /// Clears all spawned prefabs.
        /// </summary>
        public static void ClearSpawnedPrefabs()
        {
            foreach (var prefab in _prefabList)
            {
                if (prefab != null)
                {
                    Destroy(prefab);
                }
            }
            _prefabList.Clear();
        }
        #endregion

        #endregion

        #region -- Fields --

        #region -- Prefab --
        private static GameObject target;
        private static List<GameObject> _prefabList = new List<GameObject>();
        #endregion

        #endregion

        #region -- Properties --

        #region -- Prefab --
        public static GameObject LastSpawnedPrefab { get; private set; }
        #endregion

        #endregion
    }
}

