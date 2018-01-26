using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TableSlotDragSensor : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
{

    [SerializeField]
    private int id;
    public int Id {
        get { return id; }
    }

    private IDragSensorManager manager;


    public static TableSlotDragSensor Init(MaskableGraphic target, IDragSensorManager manager, int id)
    {
        var sensor = target.GetComponent<TableSlotDragSensor>();
        if(sensor == null)
            sensor = target.gameObject.AddComponent<TableSlotDragSensor>();

        sensor.manager = manager;
        sensor.id = id;

        return sensor;
    }


    public void OnPointerDown(PointerEventData data)
    {
        manager.RegisterSensor(id);
    }


    public void OnPointerUp(PointerEventData data)
    {
        manager.RemoveSensor(id);
    }


    public void OnPointerEnter(PointerEventData data)
    {
        manager.OverlapSensor(id);
    }


    public void OnPointerExit(PointerEventData data)
    {
        manager.SplitSensor(id);
    }


    public void OnDrag(PointerEventData data)
    {
        manager.DragSensor(data.position - data.pressPosition);
    }

}

