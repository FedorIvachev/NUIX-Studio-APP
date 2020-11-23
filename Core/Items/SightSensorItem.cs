using UnityEngine;

namespace Tsinghua.HCI.IoThingsLab
{
    public class SightSensorItem : SensorItem
    {
        [SerializeField] Transform _target;
        [SerializeField] Camera _camera;
        [SerializeField] bool _enableMotionSensor = false;
        
        void Update()
        {
            if (_enableMotionSensor) CheckForTargetInCameraView();
        }

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
