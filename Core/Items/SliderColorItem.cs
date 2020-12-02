using Microsoft.MixedReality.Toolkit.UI;
using UnityEngine;

namespace Tsinghua.HCI.IoThingsLab
{
    /// <summary>
    /// A helper class for the slider (change green color)
    /// </summary>
    public class SliderColorItem : MonoBehaviour
    {
        public void OnSliderGreenUpdated(SliderEventData eventData)
        {
            GetComponent<Light>().color = new Color(GetComponent<Light>().color.r, eventData.NewValue, GetComponent<Light>().color.b);
        }
    }
}
