using Microsoft.MixedReality.Toolkit.UI;
using UnityEngine;

namespace Tsinghua.HCI.IoThingsLab
{
    public class SliderColorItem : MRTKBasedItem
    {
        public void OnSliderGreenUpdated(SliderEventData eventData)
        {
            GetComponent<Light>().color = new Color(GetComponent<Light>().color.r, eventData.NewValue, GetComponent<Light>().color.b);
        }
    }
}
