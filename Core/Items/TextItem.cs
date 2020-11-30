using UnityEngine;
using TMPro;

namespace Tsinghua.HCI.IoThingsLab
{
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
