using System.Collections;
using System.Collections.Generic;
using Tsinghua.HCI.IoThingsLab;
using UnityEngine;

public class GestureControlHelper : MonoBehaviour
{

    [SerializeField]
    [Tooltip("A GameObject with a ThumbsUpRotated component")] public GestureThumbsUpRotated _thumbsUpRotatedGesture;


    private void Start()
    {
        InvokeRepeating(nameof(SetBrightnessGesture), 2.0f, 0.2f);
    }

    public void SetBrightnessGesture()
    {
        if (_thumbsUpRotatedGesture.TryGetNormalizedValue(out uint value))
        {
            //GameObject.Find("BrightnessControlledItem").GetComponent<VirtualLightController>().SetLightBrightness(value * 1.0f / 10f);
            foreach (GameObject dimmer in GameObject.FindGameObjectsWithTag("Dimmer"))
            {
                dimmer.GetComponent<DimmerWidget>()._PinchSlider.SliderValue = value / 100f;
                dimmer.GetComponent<DimmerWidget>().OnSetItem();

            }
        }
    }




}
