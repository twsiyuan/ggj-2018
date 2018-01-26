using System;
using UnityEngine;

[CreateAssetMenu(fileName = "SoundPack", menuName = "Scriptable Object/Service Framework/Sound Pack", order = 62)]
public class SoundPack : ScriptableObject
{

    #region [ Fields / Properties ]

    [SerializeField]
    private string index;
    public string Index {
        get { return index; }
    }

    [SerializeField]
    private AudioClip[] clips;

    #endregion


    public AudioClip GetClipWith(string clipName)
    {
        for(var c = 0; c < clips.Length; c++) {
            if(clips[c].name == clipName)
                return clips[c];
        }

        throw new NullReferenceException(string.Format("Audio clip not exists: {0}", clipName));
    }


    public AudioClip GetClipAt(int pos)
    {
        return clips[pos];
    }

}