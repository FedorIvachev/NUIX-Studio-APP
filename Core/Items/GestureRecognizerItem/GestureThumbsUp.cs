using Microsoft.MixedReality.Toolkit.Utilities;
using Microsoft.MixedReality.Toolkit.Input;

using UnityEngine;

#if OCULUSINTEGRATION_PRESENT
#endif

namespace Tsinghua.HCI.IoThingsLab
{
    /// <summary>
    /// Unique gesture for Thumbs up. The value is dot product between the palm and cameratransform.
    /// </summary>
    public class GestureThumbsUp : Gesture
    {
        
        public override bool GestureCondition()
        {
            return !HandPoseUtils.IsThumbGrabbing(_handedness) && HandPoseUtils.IsMiddleGrabbing(_handedness) && HandPoseUtils.IsIndexGrabbing(_handedness);
        }

        public override void GestureEventTrigger()
        {
            if (TryGetGestureValue(out float value))
            {
                if (value > 0.6f)
                {
                    _trigger.SensorTrigger();
                }
                else
                {
                    _trigger.SensorUntrigger();
                }
            }
            else
            {
                _trigger.SensorUntrigger();
            }
        }

        public override bool TryGetGestureValue(out float value)
        {
            value = 0f;
            if (!GestureCondition()) return false;

            Camera mainCamera = CameraCache.Main;
            if (mainCamera == null)
            {
                Debug.Log("Camera is inaccessible");
                return false;
            }
            Transform cameraTransform = mainCamera.transform;
            MixedRealityPose palmPose = MixedRealityPose.ZeroIdentity;
            if (HandJointUtils.TryGetJointPose(TrackedHandJoint.Palm, _handedness, out MixedRealityPose palmpose))
            {
                palmPose = palmpose;
            }
            if (_handedness == Handedness.Right) value = Vector3.Dot(palmPose.Up, cameraTransform.right);
            else value = Vector3.Dot(palmPose.Up, cameraTransform.right) * -1f;
            //_valueItem.Data = value;
            return true;
        }
    }
}