using Microsoft.MixedReality.Toolkit.Utilities;

public class GestureFist : GestureWidget
{
    public override bool GestureCondition()
    {
        return HandPoseUtils.IsThumbGrabbing(_handedness) && 
            HandPoseUtils.IsMiddleGrabbing(_handedness) && 
            HandPoseUtils.IsIndexGrabbing(_handedness) &&
            IsPinkyGrabbing(_handedness) &&
            IsRingGrabbing(_handedness);
    }
}
