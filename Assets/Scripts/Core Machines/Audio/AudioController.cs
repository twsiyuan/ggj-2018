using UnityEngine;
using MarsCode113.ServiceFramework;

public class AudioController : MonoBehaviour
{
    [SerializeField]
    private AudioClip _bgm;

    [SerializeField]
    private AudioClip _busStart;
    private float _busStartDuration = 5f;

    [SerializeField]
    private AudioClip _busDoor;
    private float _busDoorDuration = 1f;

    [SerializeField]
    private AnimateManager _animateMgr;

    private IAudioManager _manager;

    private void Start() { 
        _manager = ServiceEngine.Instance.GetManager<IAudioManager>();
        _manager.Play(0, _bgm); 

        _animateMgr.StartBusEvent += _playStartBusAudio;
        _animateMgr.BusDoorOpenEvent += _playBusDoorAudio;
    }

    private void _playStartBusAudio() {
        _manager.PlayOneShot(0, _busStart);
    }
    private void _playBusDoorAudio() {
        _manager.PlayOneShot(0, _busDoor);
    }
}