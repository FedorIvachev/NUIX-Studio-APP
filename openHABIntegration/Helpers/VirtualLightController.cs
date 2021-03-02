using UnityEngine;
/// <summary>
/// Singleton to process location sync
/// </summary>
public class VirtualLightController : MonoBehaviour
{
    /// <summary>
    /// Set virtual light intensity equal to the float value
    /// intensity = 0..10
    /// </summary>
    /// <param name="brightness"></param>
    public void SetLightBrightness(float brightness)
    {
        gameObject.GetComponent<Light>().intensity = brightness / 10.0f;
    }

    /// <summary>
    /// Get virtual light intensity
    /// intensity = 0..10
    /// </summary>
    /// <returns></returns>
    public float GetLightBrightness()
    {
        return gameObject.GetComponent<Light>().intensity * 10.0f;
    }
}