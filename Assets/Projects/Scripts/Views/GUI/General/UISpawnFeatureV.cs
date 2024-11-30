using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Utilities;

public class UISpawnFeatureV : MonoBehaviour
{
    private void Awake()
    {
        SetFeatureAsDefault();
    }

    private void SetFeatureAsDefault()
    {
        SwitchFeature(_defaultFeature);
    }

    public void SwitchFeature(string tagName)
    {
        GetFeatureAddressable(tagName);
        SpawnFeaturePrefab();
    }

    private void GetFeatureAddressable(string tagName)
    {
        if (_currentFeature != null && !string.IsNullOrEmpty(_currentFeature.labelString))
        {
            MainHandler.ClearSpawnedPrefabs(true);
        }

        _currentFeature = new AssetLabelReference { labelString = tagName };
    }

    private void SpawnFeaturePrefab()
    {
        MainHandler.SpawnPrefabByLabel(_currentFeature, GameObject.FindWithTag("FeatureContainer"));
    }

    #region -- Fields --

    [SerializeField] private string _defaultFeature = "CampusManagerF";
    [SerializeField] private AssetLabelReference _currentFeature = null;

    #endregion
}
