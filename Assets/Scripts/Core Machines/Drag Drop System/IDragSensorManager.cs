using UnityEngine;

public interface IDragSensorManager
{

    void RegisterSensor(int id);


    void DragSensor(Vector2 pos);


    void OverlapSensor(int id);


    void SplitSensor(int id);


    void RemoveSensor(int id);

}