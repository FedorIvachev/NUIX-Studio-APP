using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


namespace Tsinghua.HCI.IoTVRP
{
    public class SightItem : BasicSensorItem
    {
        [SerializeField] Transform _target;
        [SerializeField] Camera _camera;
        [SerializeField] bool _enableMotionSensor = false;


        void Start()
        {
        }

        void Update()
        {
            if (_enableMotionSensor) CheckForTargetInCameraView();
        }

        public void CheckForTargetInCameraView()
        {
            Vector3 viewPos = _camera.WorldToViewportPoint(_target.position);
            // example:
            //if (viewPos.x > 0.5F)
            //    print("target is on the right side!");
            //else
            //    print("target is on the left side!");

            // Also need to check for the speed of this object
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


    }
}
