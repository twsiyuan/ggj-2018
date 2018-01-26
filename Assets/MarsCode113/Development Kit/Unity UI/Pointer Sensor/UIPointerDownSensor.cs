using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UnityEngine.UI
{
    public class UIPointerDownSensor : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {

        #region [ Fields / Properties ]
        [SerializeField]
        private int id;
        public int Id
        {
            get { return id; }
        }

        [SerializeField]
        private MaskableGraphic target;
        public MaskableGraphic Target
        {
            get { return target; }
        }

        public Action<UIPointerDownSensor, bool> action;
        #endregion


        public static UIPointerDownSensor Init(MaskableGraphic target, Action<UIPointerDownSensor, bool> action, int id = 0)
        {
            var sensor = target.GetComponent<UIPointerDownSensor>();
            if(sensor == null)
                sensor = target.gameObject.AddComponent<UIPointerDownSensor>();

            sensor.id = id;
            sensor.target = target;
            sensor.action = action;

            return sensor;
        }


        public void OnPointerDown(PointerEventData data)
        {
            action(this, true);
        }


        public void OnPointerUp(PointerEventData data)
        {
            action(this, false);
        }

    }
}