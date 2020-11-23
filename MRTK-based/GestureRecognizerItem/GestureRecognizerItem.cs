using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit.Utilities;

// TODO: refactor code to get; set 
namespace Tsinghua.HCI.IoThingsLab
{
    public class GestureRecognizerItem : SensorItem
    {
        private float EPS = 0.005f;
        // Start is called before the first frame update
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {
            Vector3 RightIndexTipPosition = Vector3.zero;
            if (HandJointUtils.TryGetJointPose(TrackedHandJoint.IndexTip, Handedness.Right, out MixedRealityPose poseRightIndexTip))
            {
                RightIndexTipPosition = poseRightIndexTip.Position;
            }

            Vector3 LeftIndexTipPosition = Vector3.zero;
            if (HandJointUtils.TryGetJointPose(TrackedHandJoint.IndexTip, Handedness.Left, out MixedRealityPose poseLeftIndexTip))
            {
                LeftIndexTipPosition = poseLeftIndexTip.Position;
            }
            if (Vector3.Distance(LeftIndexTipPosition, RightIndexTipPosition) < EPS)
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
