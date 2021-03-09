using UnityEngine;
using System;

namespace Tsinghua.HCI.IoThingsLab
{
    /// <summary>
    /// Unique class. Enables controlling real and virtual lamp by gesture at the same time (by synchronizing them).
    /// </summary>
    public class YeelightColor : OpenHabItem
    {
        [SerializeField]
        [Tooltip("A GameObject with a ThumbsUpRotated component")] public GestureThumbsUpRotated _thumbsUpGesture;
        [SerializeField]
        [Tooltip("Whether synchronization to openHAB-connected item should be performed")] bool _enableNetworkCalls = true;
        [SerializeField]
        [Tooltip("Whether a virtual brightness should be synchronized")] bool _hasConnectedLightItem = true;
        [SerializeField]
        [Tooltip("A GameObject with a synchronized Light component")] public LightItem _connectedLightItem;

        private float _intensityTemp;
        private uint _brightnessTemp;

        public void SetBrightness(uint value)
        {
            if (_brightnessTemp != value && _enableNetworkCalls) if (NetworkPOSTCall("YeelightColorLEDBulb_Color", value.ToString(), out string response))
                {
                    _brightnessTemp = value;
                }
        }

        public void SetIntensity(float value)
        {
            if (_hasConnectedLightItem) _connectedLightItem.SetIntensity(value);
        }

        public void Start()
        {
            InvokeRepeating("SetBrightnessGesture", 2.0f, 0.016f);
            InvokeRepeating("SynchronizeBrightness", 2.0f, 0.1f); 
        }
        
        public void SetBrightnessGesture()
        {
            if (_thumbsUpGesture.TryGetNormalizedValue(out uint value))
            {
                value = (value > 1) ? value : 2;
                SetIntensity(value * 1.0f / 20f); // max intensity = 5.0
            }
        }

        public void SynchronizeBrightness()
        {
            if (_hasConnectedLightItem)
            {
                if (Math.Abs(_connectedLightItem.GetLight().intensity - _intensityTemp) > 0.2)
                {
                    _intensityTemp = _connectedLightItem.GetLight().intensity;
                    SetBrightness((uint) _intensityTemp * 20); // assuming max intensity = 5.0
                }
            }
        }
    }

}