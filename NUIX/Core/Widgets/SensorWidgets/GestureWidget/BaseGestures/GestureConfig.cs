using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit.Utilities;
using UnityEngine;


/// <summary>
/// Legacy
/// </summary>
public class GestureConfig
{

    private static GameObject loadingGesture;
    private static GameObject loadingGesturePrefab;
    private static GestureConfig gestureConfigInstance;

    public GestureConfig()
    {
        loadingGesturePrefab = Resources.Load("Helping/LoadingGesture") as GameObject;
        loadingGesture = GameObject.Instantiate(loadingGesturePrefab, Vector3.zero, Quaternion.identity) as GameObject;
        loadingGesture.SetActive(false);
}
    public static GestureConfig getInstance()
    {
        if (gestureConfigInstance == null)
            gestureConfigInstance = new GestureConfig();
        return gestureConfigInstance;
    }
    public void GestureLoading(Handedness _handedness)
    {
        if (HandJointUtils.TryGetJointPose(TrackedHandJoint.IndexTip, _handedness, out MixedRealityPose pose))
        {
            loadingGesture.SetActive(true);
            loadingGesture.transform.position = pose.Position + Vector3.up / 10.0f;
        }
        else
        {
            loadingGesture.SetActive(false);
        }
    }

    public void GestureUnloading()
    {
        loadingGesture.SetActive(false);
    }


}