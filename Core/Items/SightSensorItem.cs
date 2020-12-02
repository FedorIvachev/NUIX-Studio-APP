using UnityEngine;

namespace Tsinghua.HCI.IoThingsLab
{
    /// <summary>
    /// Provides functionality for triggering the sensor
    /// when the connected item is inside the defined camera view
    /// </summary>
    public class SightSensorItem : SensorItem
    {
        [SerializeField]
        [Tooltip("The tracked object")] Transform _target;
        [SerializeField] [Tooltip("Should be either Character camera or any other camera in the scene")] Camera _camera;
        [SerializeField] [Tooltip("Turn on/off the sensing")] bool _enableMotionSensor = false;
        
        void Update()
        {
            if (_enableMotionSensor) CheckForTargetInCameraView();
        }

        /// <summary>
        /// When object is in front of camera, trigger the sensor
        /// </summary>
        public void CheckForTargetInCameraView()
        {
            Vector3 viewPos = _camera.WorldToViewportPoint(_target.position);
            // Checking if the target object is inside the defined camera view
            if ((viewPos.z > 0.0F) && (viewPos.x < 1.0F) && (viewPos.x > 0.0F))
            {
                SensorTrigger();
            }
            else
            {
                SensorUntrigger();
            }
        }

        public Vector3 GetViewportLocation()
        {
            return _camera.WorldToViewportPoint(_target.position);
        }

        void SerializeValue(string name = "")
        {
            base.SerializeValue(name + "_motionSensorTrigger");
        }
    }
}
