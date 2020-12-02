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
    /// <summary>
    /// Provides an example gesture to be recognized, 
    /// and trigger an event as result of successful recognition
    /// </summary>
    public class GestureRecognizerItem : SensorItem
    {      
        /// <summary>
        /// Thumbs Up gesture for the right hand;
        /// Triggers as sensor when, you are right, thumbs up is performed
        /// </summary>
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

                // We check if the palm up is roughly in line with the camera right
                // From all fingers only thumb should not be grabbing
                return Vector3.Dot(palmPose.Up, cameraTransform.right) > 0.6f
                       && !HandPoseUtils.IsThumbGrabbing(handedness) && HandPoseUtils.IsMiddleGrabbing(handedness) && HandPoseUtils.IsIndexGrabbing(handedness);
            }
        }

        // TODO: Just checking the gesture every frame causes false sensor triggers
        // Add an extra statement, check function
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