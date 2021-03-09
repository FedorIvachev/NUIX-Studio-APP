using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Tsinghua.HCI.IoThingsLab
{
    /// <summary>
    /// Basic item, operates with two events:
    /// Sensor trigger and untrigger
    /// </summary>
    public class SensorItem : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("Action performed after sensor is triggered")]
        private UnityEvent m_OnSensorTriggered;
        [SerializeField]
        [Tooltip("Action performed after sensor is triggered")]
        private UnityEvent m_OnSensorUntriggered;

        GenericItem _sensorTriggerItem;
        bool _isSensorTriggered = false;

        // Start is called before the first frame update
        void Start()
        {
            if (m_OnSensorTriggered == null)
                m_OnSensorTriggered = new UnityEvent();
            if (m_OnSensorUntriggered == null)
                m_OnSensorUntriggered = new UnityEvent();
        }

        void SensorTriggered()
        {
            _isSensorTriggered = true;
            m_OnSensorTriggered?.Invoke();
        }

        void SensorUntriggered()
        {
            _isSensorTriggered = false;
            m_OnSensorUntriggered?.Invoke();
        }

        public void SensorTrigger()
        {
            if (!_isSensorTriggered) SensorTriggered();
        }

        public void SensorUntrigger()
        {
            if (_isSensorTriggered) SensorUntriggered();
        }


        /// <summary>
        /// save the parameters inside the generic item
        /// </summary>
        /// <param name="type"></param>
        /// <param name="name"></param>
        public void SerializeValue(string name = "sensorItem", string type = "Trigger")
        {
            _sensorTriggerItem.state = (_isSensorTriggered ? "ON" : "OFF");
            _sensorTriggerItem.type = type;
            _sensorTriggerItem.name = name;
        }
    }
}
