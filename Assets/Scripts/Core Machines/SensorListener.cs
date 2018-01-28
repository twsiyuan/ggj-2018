using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[Serializable]
public class SensorListener : ISensorListener
{
    private enum Status {
        Waiting,
        Complete,
    }
    private Status _status;

    public List<int> _result;
    public List<int> Result { get { return _result; } }

    private GameMachine_Prototype _gameMachine;

    public SensorListener(GameMachine_Prototype gameMachine) {
        _gameMachine = gameMachine;
        _gameMachine.SensorComplete = CompleteAction;
        _result = new List<int>();
    }

    public IEnumerator ListenNextBusPath() {

        _status = Status.Waiting;

        while (_status == Status.Waiting) {

            yield return null;
             
        }
    }

    private void CompleteAction(List<int> busTargets) {
        _status = Status.Complete;
        _result.Clear();
        _result.AddRange(busTargets);
    }
}