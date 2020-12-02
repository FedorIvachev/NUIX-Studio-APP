using UnityEngine;

namespace Tsinghua.HCI.IoThingsLab
{
    /// <summary>
    /// Item to operate with Light Component
    /// Attach it to the GameObject with Light Component
    /// </summary>
    public class LightItem : MonoBehaviour
    {
        GenericItem _lightItem;
        GenericItem _lightState;
        GenericItem _color;
        GenericItem _intensity;

        private Light _light;
        // Start is called before the first frame update
        void Start()
        {
            _light = GetComponent<Light>();
        }
        
        public void SetColor(Color newColor)
        {
            _light.color = newColor;
        }

        /// <summary>
        /// Toggle the light ON/OFF
        /// </summary>
        public void Toggle()
        {
            _light.enabled = !_light.enabled;
        }

        public void TurnOff()
        {
            _light.enabled = false;
        }

        public void TurnOn()
        {
            _light.enabled = true;
        }

        public void SetRange(float newRange)
        {
            _light.range = newRange;
        }

        public void SetIntensity(float newIntensity)
        {
            _light.intensity = newIntensity;
        }

        public void SetType(LightType lightType)
        {
            _light.type = lightType;
        }

        public Light GetLight()
        {
            return _light;
        }

        public void SerializeValue(string name = "")
        {
            _lightState.state = (_light.enabled ? "ON" : "OFF");
            _lightState.type = "Toggle";
            _lightState.name = name + "_lightState";

            _color.state = _light.color.ToString();
            _color.type = "Color";
            _color.name = name + "_color";

            _intensity.state = _light.intensity.ToString();
            _intensity.type = "FloatNumber";
            _intensity.name = name + "_intensity";

            _lightItem.state = _light.ToString(); // TODO: check if the return value has any sense
            _lightItem.type = "Light";
            _lightItem.name = name + "_lightItem";
        }
    }
}
