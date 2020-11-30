using System.Collections;
using System.Collections.Generic;
using System;

using UnityEngine;
using TMPro;
using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit.Utilities;
using Microsoft.MixedReality.Toolkit;
using Microsoft.MixedReality.Toolkit.XRSDK.Oculus.Input;

#if OCULUSINTEGRATION_PRESENT
using static OVRSkeleton;
#endif

namespace Tsinghua.HCI.IoThingsLab
{
    public class GestureRecognizerItem : SensorItem
    {
        protected readonly Dictionary<TrackedHandJoint, MixedRealityPose> jointPoses = new Dictionary<TrackedHandJoint, MixedRealityPose>();

        [SerializeField] public GameObject _logger;
        
        private float EPS = 0.005f;
        // Start is called before the first frame update
        void Start()
        {
        }
        

        protected bool IsInThumbsUpPose
        {
            get
            {
                Handedness handedness = Handedness.Right;
                //if (jointPoses.TryGetValue(TrackedHandJoint.Palm, out var palmPose)) return false;

                Camera mainCamera = CameraCache.Main;

                if (mainCamera == null)
                {
                    return false;
                }

                Transform cameraTransform = mainCamera.transform;

                MixedRealityPose palmPose = MixedRealityPose.ZeroIdentity;
                if (HandJointUtils.TryGetJointPose(TrackedHandJoint.Palm, handedness, out MixedRealityPose palmpose))
                {
                    palmPose = palmpose;
                }
                // We check if the palm up is roughly in line with the camera up
                return Vector3.Dot(palmPose.Up, cameraTransform.right) > 0.6f
                       && !HandPoseUtils.IsThumbGrabbing(handedness) && HandPoseUtils.IsMiddleGrabbing(handedness) && HandPoseUtils.IsIndexGrabbing(handedness);
            }
        }

        public void Update()
        {
            if (IsInThumbsUpPose)
            {
                SensorTrigger();
            }
            else
            {
                SensorUntrigger();
            }
        }
    }
}