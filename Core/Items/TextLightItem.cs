using UnityEngine;

namespace Tsinghua.HCI.IoThingsLab
{
    /// <summary>
    /// A helper class for showing the light intensity in the text item.
    /// </summary>
    public class TextLightItem : TextItem
    {
        [SerializeField] LightItem _connectedLightItem;
        private void Update()
        {
            Text = _connectedLightItem.GetLight().intensity.ToString();
        }
    }

}
