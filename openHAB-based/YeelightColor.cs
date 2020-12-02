using UnityEngine;

namespace Tsinghua.HCI.IoThingsLab
{
    public class YeelightColor : OpenHabItem
    {
        [SerializeField] bool _enableNetworkCalls = true;
        public void SetColorTemperature(int value)
        {
            if (_enableNetworkCalls) NetworkPOSTCall("YeelightColorLEDBulb_ColorTemperature", value.ToString());
        }
    }
}