using System.Collections.Generic;
using UnityEngine;

public class BusCenterView : MonoBehaviour
{
    [SerializeField]
    private PassengerIndexSettings _allBus;

    private List<GameObject> _busViews;

    void Awake() {
        _busViews = new List<GameObject>();
    }
}