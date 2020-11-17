using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Tsinghua.HCI.IoTVRP
{
    public class BasicSensorItem : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("Action performed after sensor is triggered")]
        private UnityEvent m_OnSensorTriggered;
        [SerializeField]
        [Tooltip("Action performed after sensor is triggered")]
        private UnityEvent m_OnSensorUntriggered;


        bool _isSensorTriggered = false;

        // Start is called before the first frame update
        void Start()
        {

            if (m_OnSensorTriggered == null)
                m_OnSensorTriggered = new UnityEvent();
            if (m_OnSensorUntriggered == null)
                m_OnSensorUntriggered = new UnityEvent();
        }

        // Update is called once per frame
        void Update()
        {

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

    }
}