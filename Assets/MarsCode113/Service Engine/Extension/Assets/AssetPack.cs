using UnityEngine;
using Object = UnityEngine.Object;

[CreateAssetMenu(fileName = "AssetPack", menuName = "Scriptable Object/Core Engine/Asset Pack", order = 61)]
public class AssetPack : ScriptableObject
{

    [Header("Asset Pack"), SerializeField]
    private Object[] assets;
    public Object[] Assets
    {
        get { return assets; }
    }


    public Object GetAsset(string index)
    {
        for(int a = 0; a < assets.Length; a++) {
            if(assets[a].name == index)
                return assets[a];
        }

        return null;
    }

}