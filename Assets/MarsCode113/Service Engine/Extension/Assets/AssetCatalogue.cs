using UnityEngine;

[CreateAssetMenu(fileName = "AssetIndex", menuName = "Scriptable Object/Core Engine/AssetManager Index", order = 61)]
public class AssetCatalogue : ScriptableObject
{

    #region [ Fields / Properties ]

    [SerializeField]
    private AssetCatalogueData[] assetBundleIndex;

    [SerializeField]
    private AssetCatalogueData[] resourceIndex;

    [SerializeField]
    private string[] symbol;
    public string[] Symbol {
        get { return symbol; }
#if UNITY_EDITOR
        set { symbol = value; }
#endif
    }

    #endregion


    public string GetAssetBundleURL(string index)
    {
        for(int id = 0; id < assetBundleIndex.Length; id++) {
            if(assetBundleIndex[id].Index == index)
                return assetBundleIndex[id].Url;
        }

        return "";
    }


    public string GetResourcePath(string index)
    {
        for(int id = 0; id < resourceIndex.Length; id++) {
            if(resourceIndex[id].Index == index)
                return resourceIndex[id].Url;
        }

        return "";
    }


    public string GetSymbolAt(int id)
    {
        return symbol[id];
    }


    #region [ Editor Compilation ]
#if UNITY_EDITOR
    public AssetCatalogueData[] AssetBundleIndex {
        get { return assetBundleIndex; }
        set { assetBundleIndex = value; }
    }

    public AssetCatalogueData[] ResourceIndex {
        get { return resourceIndex; }
        set { resourceIndex = value; }
    }

    [SerializeField, HideInInspector]
    private string[] filePaths;

    [SerializeField, HideInInspector]
    private bool autoRefresh;
#endif
    #endregion

}