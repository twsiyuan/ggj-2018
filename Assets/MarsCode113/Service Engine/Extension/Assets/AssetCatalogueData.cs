using System;
using UnityEngine;

[Serializable]
public class AssetCatalogueData
{

    #region [ Fields / Properties ]

    [SerializeField]
    private string index;
    public string Index
    {
        get { return index; }
    }

    [SerializeField]
    private string url;
    public string Url
    {
        get { return url; }
    }

    #endregion


    public AssetCatalogueData(string index, string url)
    {
        this.index = index;
        this.url = url;
    }

}