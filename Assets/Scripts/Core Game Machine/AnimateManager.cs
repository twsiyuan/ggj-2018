using System;
using UnityEngine;
using System.Collections;

[Serializable]
public class AnimateManager : MonoBehaviour, IAnimateManager
{
    public void PlayBusAnimate() {

        Debug.Log("Play animate");

        StartCoroutine(_playBusAnimate());
    }

    private IEnumerator _playBusAnimate() {

        Debug.Log("bus start");
        yield return new WaitForSeconds(0.5f);
        Debug.Log("bus go 1");
        yield return new WaitForSeconds(0.5f);
        Debug.Log("bus go 2");

    }
}