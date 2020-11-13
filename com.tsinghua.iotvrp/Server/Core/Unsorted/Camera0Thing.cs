using UnityEngine;

namespace Tsinghua.HCI.IoTVRP
{
    public class Camera0Thing : BasicThing
    {
        [SerializeField] GameObject _sightItem;
        [SerializeField] GameObject _textItem;

        public void Update()
        {
            Vector3 viewportLocation = _sightItem.GetComponent<SightItem>().GetViewportLocation();
            _textItem.GetComponent<TextItem>().UpdateText(viewportLocation.x.ToString() + ", " + viewportLocation.y.ToString());
        }
    }

}
