using UnityEngine;

public class AudioController : MonoBehaviour
{
    [SerializeField]
    private AudioSource _bgm;

    [SerializeField]
    private GameObject _busStart;
    private float _busStartDuration = 5f;

    [SerializeField]
    private GameObject _busDoor;
    private float _busDoorDuration = 1f;

    [SerializeField]
    private AnimateManager _animateMgr;

    private void Start() {
        _bgm.Play();

        _animateMgr.StartBusEvent += _playStartBusAudio;
        _animateMgr.BusDoorOpenEvent += _playBusDoorAudio;
    }

    private void _playStartBusAudio() {
        GameObject obj = Instantiate(_busStart);
        Destroy(obj, _busStartDuration);
    }
    private void _playBusDoorAudio() {
        GameObject obj = Instantiate(_busDoor);
        Destroy(obj, _busDoorDuration);
    }
}