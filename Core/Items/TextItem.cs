using UnityEngine;
using TMPro;

namespace Tsinghua.HCI.IoThingsLab
{
    /// <summary>
    /// An item to be attached to a GameObject with TextMeshPro
    /// Provides functionality to change and serialize the connected text
    /// </summary>
    public class TextItem : MonoBehaviour
    {
        GenericItem _text;

        public string Text
        {
            get
            {
                return GetComponent<TextMeshPro>().text;
            }
            set
            {
                GetComponent<TextMeshPro>().text = value;
            }
        }

        public void SerializeValue(string name = "")
        {
            _text.state = this.Text;
            _text.type = "Text";
            _text.name = name + "_text";
        }
    }
}
