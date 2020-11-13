using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Microsoft.MixedReality.Toolkit.UI;

// Should be connected to the light gameobject

namespace Tsinghua.HCI.IoTVRP
{

    public class SliderColorItem : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void OnSliderGreenUpdated(SliderEventData eventData)
        {
            GetComponent<Light>().color = new Color(GetComponent<Light>().color.r, eventData.NewValue, GetComponent<Light>().color.b);
        }
    }
}