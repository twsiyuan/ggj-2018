using UnityEngine;

public interface IDragSensorManager
{

    void StartDragging(int sensorID);


    void CheckInSensor(int sensorID);


    void CheckOutSensor(int sensorID);


    void CompleteDragging(int sensorID);


    void SyncPointerLocation(Vector2 pos);

}