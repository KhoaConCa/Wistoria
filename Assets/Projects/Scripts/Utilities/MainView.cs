using Sirenix.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;

namespace Utilities
{
    public static class MainView
    {
        #region -- Methods --

        #region -- Find Object By Tag --
        public static Transform FindObjectsByTag(Transform parentObject, string tagName)
        {
            if (parentObject.tag == tagName)
                return parentObject;

            for (int i = 0; i < parentObject.transform.childCount; i++)
            {
                Transform result = FindObjectsByTag(parentObject.transform.GetChild(i), tagName);

                if (result != null)
                    return result;
            }

            return null;
        }
        #endregion

        #region -- On Event Feature --
        public static void OnSuccess(string message)
        {
            Debug.Log(message);

            AssetLabelReference notificationCard = new AssetLabelReference { labelString = "SuccessNotification" };
            GameObject container = GameObject.FindWithTag("GUI");
            MainHandler.SpawnPrefabByLabel(notificationCard, container, spawnedPrefab =>
            {
                Transform messageValue = FindObjectsByTag(spawnedPrefab.transform, "MessageValue");
                if (messageValue != null) 
                    messageValue.GetComponent<TextMeshProUGUI>().text = message;

                SetUpNotificationCard(spawnedPrefab.GetComponent<RectTransform>(), - 192);
                DestroyNotificationCard(spawnedPrefab);
            });
        }

        public static void OnWarning(string message)
        {
            AssetLabelReference notificationCard = new AssetLabelReference { labelString = "WarningNotification" };
            GameObject container = GameObject.FindWithTag("GUI");
            MainHandler.SpawnPrefabByLabel(notificationCard, container, spawnedPrefab =>
            {
                Transform messageValue = FindObjectsByTag(spawnedPrefab.transform, "MessageValue");
                if (messageValue != null)
                    messageValue.GetComponent<TextMeshProUGUI>().text = message;

                SetUpNotificationCard(spawnedPrefab.GetComponent<RectTransform>(), -192);
                DestroyNotificationCard(spawnedPrefab);
            });
        }

        public static void OnFailed(string message)
        {
            AssetLabelReference notificationCard = new AssetLabelReference { labelString = "ErrorNotification" };
            GameObject container = GameObject.FindWithTag("GUI");
            MainHandler.SpawnPrefabByLabel(notificationCard, container, spawnedPrefab =>
            {
                Transform messageValue = FindObjectsByTag(spawnedPrefab.transform, "MessageValue");
                if (messageValue != null)
                    messageValue.GetComponent<TextMeshProUGUI>().text = message;

                SetUpNotificationCard(spawnedPrefab.GetComponent<RectTransform>(), -192);
                DestroyNotificationCard(spawnedPrefab);
            });
        }

        public static void OnReset(Action method, string message)
        {
            AssetLabelReference notificationCard = new AssetLabelReference { labelString = "ResetNotification" };
            GameObject container = GameObject.FindWithTag("GUI");
            MainHandler.SpawnPrefabByLabel(notificationCard, container, spawnedPrefab =>
            {
                Transform messageValue = FindObjectsByTag(spawnedPrefab.transform, "MessageValue");
                if (messageValue != null)
                    messageValue.GetComponent<TextMeshProUGUI>().text = message;

                if (method != null)
                {
                    Transform buttonTransform = FindObjectsByTag(spawnedPrefab.transform, "ResetButton");
                    Button resetButton = buttonTransform.GetComponent<Button>();
                    if (resetButton != null)
                        resetButton.onClick.AddListener(method.Invoke);
                }
            });
        }

        public static void OnChecking(Action method, string message)
        {
            AssetLabelReference notificationCard = new AssetLabelReference { labelString = "CheckingNotification" };
            GameObject container = GameObject.FindWithTag("GUI");
            MainHandler.SpawnPrefabByLabel(notificationCard, container, spawnedPrefab =>
            {
                Transform messageValue = FindObjectsByTag(spawnedPrefab.transform, "MessageValue");
                if (messageValue != null)
                    messageValue.GetComponent<TextMeshProUGUI>().text = message;

                if (method != null)
                {
                    Transform buttonTransform = FindObjectsByTag(spawnedPrefab.transform, "CheckingButton");
                    Button resetButton = buttonTransform.GetComponent<Button>();
                    if (resetButton != null)
                        resetButton.onClick.AddListener(method.Invoke);
                }
            });
        }

        public static void OnDebugged(string message)
        {
            Debug.Log(message);
        }
        #endregion

        #region -- Set Up Notification Card --
        private static void SetUpNotificationCard(RectTransform prefabTransform, float distanceBase)
        {
            // Cập nhật vị trí RectTransform
            AnchorTop(prefabTransform);

            float worldY = (prefabTransform.localPosition.y + distanceBase);

            // Cập nhật vị trí cho RectTransform
            prefabTransform.localPosition = new Vector2(prefabTransform.localPosition.x, worldY);
        }

        public static void DestroyNotificationCard(GameObject spawnedPrefab)
        {
            MainHandler.PrefabList.Remove(spawnedPrefab);

            UnityEngine.Object.Destroy(spawnedPrefab, 5f);

        }
        #endregion

        #region -- Set Up Anchor --
        public static void AnchorTop(RectTransform prefabTransform)
        {
            prefabTransform.anchorMax = new Vector2(0.5f, 1);
            prefabTransform.anchorMin = new Vector2(0.5f, 1);
            prefabTransform.pivot = new Vector2(0.5f, 1);
        }

        public static void AnchorBottom(RectTransform prefabTransform)
        {
            prefabTransform.anchorMax = new Vector2(1, 0.5f);
            prefabTransform.anchorMin = new Vector2(1, 0.5f);
            prefabTransform.pivot = new Vector2(1, 0.5f);
        }

        public static void AnchorLeft(RectTransform prefabTransform)
        {
            prefabTransform.anchorMax = new Vector2(0, 0.5f);
            prefabTransform.anchorMin = new Vector2(0, 0.5f);
            prefabTransform.pivot = new Vector2(0, 0.5f);
        }

        public static void AnchorRight(RectTransform prefabTransform)
        {
            prefabTransform.anchorMax = new Vector2(0.5f, 0);
            prefabTransform.anchorMin = new Vector2(0.5f, 0);
            prefabTransform.pivot = new Vector2(0.5f, 0);
        }
        #endregion

        #endregion
    }
}

