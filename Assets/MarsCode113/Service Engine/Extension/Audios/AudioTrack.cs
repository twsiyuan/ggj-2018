using System;
using System.Collections;
using UnityEngine;

[Serializable]
public class AudioTrack
{

    #region [ Fields / Properties ]

#if UNITY_EDITOR
    [SerializeField]
    public string Index = "Audio Track";
#endif

    [SerializeField]
    private AudioSource au;
    public AudioSource Au {
        get { return au; }
    }

    [SerializeField, Range(0, 1)]
    private float volume = 1;
    public float Volume {
        get { return volume; }
        set {
            volume = Mathf.Clamp01(value);
            au.volume = volume;
        }
    }

    [SerializeField, Range(0, 5)]
    private float duration = 0.5f;
    public float Duration {
        get { return duration; }
        set { duration = Mathf.Clamp(value, 0, 5); }
    }

    public IEnumerator Launcher;

    #endregion


    #region [ Core ]

    public AudioTrack(AudioSource au)
    {
        this.au = au;
    }


    public void ReadyToSwitch(AudioClip clip)
    {
        if(au.clip == null) {
            au.clip = clip;
            ReadyToPlay();
        }
        else
            Launcher = SwitchAudioClip(clip);
    }


    public void ReadyToPlay()
    {
        Launcher = ExchangeVolume(true, null);
        au.Play();
    }


    public void ReadyToPause()
    {
        Launcher = ExchangeVolume(false, () => au.Pause());
    }


    public void ReadyToStop()
    {
        Launcher = ExchangeVolume(false, () => au.Stop());
    }


    public void PlayOneShot(AudioClip clip)
    {
        au.PlayOneShot(clip, volume);
    }

    #endregion


    #region [ Utility ]

    public void Clean()
    {
        Launcher = null;

        au.clip = null;

        au.volume = volume;
    }


    private IEnumerator SwitchAudioClip(AudioClip clip)
    {
        if(duration == 0) {
            au.clip = clip;
            au.volume = volume;
            au.Play();
        }
        else {
            var endOfFrame = new WaitForEndOfFrame();
            while(au.volume != 0) {
                au.volume = Mathf.MoveTowards(au.volume, 0, ( 1 / duration ) * Time.deltaTime);
                yield return endOfFrame;
            }

            au.clip = clip;
            au.Play();

            while(au.volume != GetEndVolume(true)) {
                au.volume = Mathf.MoveTowards(au.volume, GetEndVolume(true), ( 1 / duration ) * Time.deltaTime);
                yield return endOfFrame;
            }
        }

        Launcher = null;
    }


    private IEnumerator ExchangeVolume(bool fadeIn, Action callback)
    {
        if(duration == 0)
            au.volume = GetEndVolume(fadeIn);
        else {
            var endOfFrame = new WaitForEndOfFrame();
            while(au.volume != GetEndVolume(fadeIn)) {
                au.volume = Mathf.MoveTowards(au.volume, GetEndVolume(fadeIn), ( 1 / duration ) * Time.deltaTime);
                yield return endOfFrame;
            }
        }

        if(callback != null)
            callback();

        Launcher = null;
    }


    private float GetEndVolume(bool fadeIn)
    {
        return fadeIn ? volume : 0;
    }

    #endregion

}