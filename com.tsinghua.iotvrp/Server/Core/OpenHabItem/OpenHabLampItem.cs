using System.Text;
using UnityEngine;
using UnityEngine.Networking;

namespace Tsinghua.HCI.IoTVRP
{
    public class OpenHabLampItem : OpenHabItem
    {
        [SerializeField] bool _enableNetworkCalls = true;
        public void SetColorTemperature(int value)
        {
            if (_enableNetworkCalls) NetworkPOSTCall("YeelightColorLEDBulb_ColorTemperature", value.ToString());
        }
        public void Update()
        {
            
        }
        void GetColorTemperature()
        {

        }
    }
    
}

