using Microsoft.MixedReality.Toolkit.Utilities;

#if OCULUSINTEGRATION_PRESENT
#endif

namespace Tsinghua.HCI.IoThingsLab
{
    /// <summary>
    /// Unique fist gesture.
    /// </summary>
    public class GestureFist : Gesture
    {
        public override bool GestureCondition()
        {
            return HandPoseUtils.IsThumbGrabbing(_handedness) && HandPoseUtils.IsMiddleGrabbing(_handedness) && HandPoseUtils.IsIndexGrabbing(_handedness);
        }

        public override void GestureEventTrigger()
        {
            if (GestureCondition()) _trigger.SensorTrigger();
            else _trigger.SensorUntrigger();
        }
        
        public override bool TryGetGestureValue(out float value)
        {
            value = 0;
            return true;
        }
    }
}