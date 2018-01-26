using System;
using UnityEngine.EventSystems;

namespace UnityEngine.UI
{
    public class UIPointerEnterSensor : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
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

        public Action<UIPointerEnterSensor, bool> action;
        #endregion


        public static UIPointerEnterSensor Init(MaskableGraphic target, Action<UIPointerEnterSensor, bool> action, int id = 0)
        {
            var sensor = target.GetComponent<UIPointerEnterSensor>();
            if(sensor == null)
                sensor = target.gameObject.AddComponent<UIPointerEnterSensor>();

            sensor.id = id;
            sensor.target = target;
            sensor.action = action;

            return sensor;
        }


        public void OnPointerEnter(PointerEventData data)
        {
            action(this, true);
        }


        public void OnPointerExit(PointerEventData data)
        {
            action(this, false);
        }

    }
}