using System;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public abstract class DragDropGame_Base : MonoBehaviour, IDragSensorManager
{

    [SerializeField]
    protected CanvasGroup canvasGroup;

    [SerializeField]
    protected TableSlot[] table;

    [SerializeField]
    protected int start = -1;

    [SerializeField]
    protected int end = -1;

    [SerializeField]
    protected Image[] marks;

    [SerializeField]
    protected EmptyGraphic[] sensors;

    protected Vector2 pressPos;


    protected virtual void Awake()
    {
        InitTable();

        InitSensors();
    }


    private void InitTable()
    {
        for(var id = 0; id < marks.Length; id++) {
            if(marks[id] == null)
                continue;

            var element = new TableElement();
            element.Set<Image>(marks[id]);

            table[id].SetElement(element);
        }

        marks = null;
    }


    private void InitSensors()
    {
        var id = 0;
        foreach(var s in sensors)
            TableSlotDragSensor.Init(s, this, id++);

        sensors = null;
    }


    public void RegisterSensor(int sensorID)
    {
        if(!table[sensorID].IsExist())
            return;

        if(start == end)
            end = -1;

        start = sensorID;

        RegisterSensorHook();
    }


    public void OverlapSensor(int sensorID)
    {
        if(start == -1)
            return;

        if(!table[sensorID].IsExist())
            return;

        if(!table[start].IsNeighbor(sensorID))
            return;

        end = sensorID;

        OverlapSensorHook();
    }


    public void SplitSensor(int sensorID)
    {
        if(end == -1)
            return;

        if(!table[end].IsExist())
            return;

        SplitSensorHook();

        end = -1;
    }


    public void RemoveSensor(int sensorID)
    {
        RemoveSensorHook();

        start = -1;

        end = -1;
    }


    public virtual void DragSensor(Vector2 pos) { }


    protected virtual void RegisterSensorHook() { }


    protected virtual void OverlapSensorHook() { }


    protected virtual void SplitSensorHook() { }


    protected virtual void RemoveSensorHook() { }

}