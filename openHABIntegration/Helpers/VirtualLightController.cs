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

    /// <summary>
    /// Set virtual light color temperature equal to the float value
    /// intensity = 0..10
    /// </summary>
    /// <param name="brightness"></param>
    public void SetLightColorTemperature(float colorTemperature)
    {

        // min 1700 to 6500
        //print("Setting light color temperature " + colorTemperature.ToString());
        //print("CURRENT COLOR TEMPERATURE " + gameObject.GetComponent<Light>().colorTemperature);
        //gameObject.GetComponent<Light>().colorTemperature = 1700 + (6500 - 1700) * colorTemperature / 100;
        gameObject.GetComponent<Light>().color = new Color(gameObject.GetComponent<Light>().color.r, colorTemperature / 100, colorTemperature / 100);
    }

    /// <summary>
    /// Get virtual light intensity
    /// intensity = 0..10
    /// </summary>
    /// <returns></returns>
    public float GetLightColorTemperature()
    {
        return gameObject.GetComponent<Light>().color.g * 100;
    }
}