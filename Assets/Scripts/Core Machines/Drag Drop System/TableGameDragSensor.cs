using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TableGameDragSensor : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
{

    [SerializeField]
    private int id;
    public int Id {
        get { return id; }
    }

    private IDragSensorManager manager;


    public static TableGameDragSensor Init(MaskableGraphic target, IDragSensorManager manager, int id)
    {
        var sensor = target.GetComponent<TableGameDragSensor>();
        if(sensor == null)
            sensor = target.gameObject.AddComponent<TableGameDragSensor>();

        sensor.manager = manager;
        sensor.id = id;

        return sensor;
    }


    public void OnPointerDown(PointerEventData data)
    {
        manager.StartDragging(id);
    }


    public void OnPointerUp(PointerEventData data)
    {
        manager.CompleteDragging(id);
    }


    public void OnPointerEnter(PointerEventData data)
    {
        manager.CheckInSensor(id);
    }


    public void OnPointerExit(PointerEventData data)
    {
        manager.CheckOutSensor(id);
    }


    public void OnDrag(PointerEventData data)
    {
        manager.SyncPointerLocation(data.position - data.pressPosition);
    }

}

