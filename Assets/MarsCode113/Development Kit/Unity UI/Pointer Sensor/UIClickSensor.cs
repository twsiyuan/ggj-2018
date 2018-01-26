using System;
using UnityEngine.EventSystems;

namespace UnityEngine.UI
{
    public class UIClickSensor : MonoBehaviour, IPointerClickHandler
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

        Action<UIClickSensor> action;
        #endregion


        public static UIClickSensor Init(MaskableGraphic target, Action<UIClickSensor> action, int id = 0)
        {
            var sensor = target.GetComponent<UIClickSensor>();
            if(sensor == null)
                sensor = target.gameObject.AddComponent<UIClickSensor>();

            sensor.id = id;
            sensor.target = target;
            sensor.action = action;

            return sensor;
        }


        public void OnPointerClick(PointerEventData data)
        {
            action(this);
        }

    }
}