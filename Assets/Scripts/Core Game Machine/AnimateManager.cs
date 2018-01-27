using System;
using UnityEngine;
using System.Collections;

[Serializable]
public class AnimateManager : MonoBehaviour, IAnimateManager
{
    private IBusView _busView;
    private IBusViewFactory _busViewFactory;

    public AnimateManager() {
        _busViewFactory = new BusViewFactory();
    }

    public void PlayBusAnimate(IBus bus) {

        Debug.Log("Play animate");

        StartCoroutine(_playBusAnimate());
    }

    private IEnumerator _playBusAnimate() {

        _busView = _busViewFactory.MakeBusView();

        _busView.InitAnimate();

        Debug.Log("bus start");
        yield return new WaitForSeconds(0.5f);
        Debug.Log("bus go 1");
        yield return new WaitForSeconds(0.5f);
        Debug.Log("bus go 2");

    }
}