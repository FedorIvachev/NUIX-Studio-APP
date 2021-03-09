using UnityEngine;

namespace Tsinghua.HCI.IoThingsLab
{
    public class ValueItem<T> : MonoBehaviour
    {
        GenericItem _valueItem;

        public T Data { get; set; }

        /// <summary>
        /// save the parameters inside the value item
        /// </summary>
        /// <param name="type"></param>
        /// <param name="name"></param>
        public void SerializeValue(string name = "valueItem", string type = "Value")
        {
            _valueItem.state = Data.ToString();
            _valueItem.type = type;
            _valueItem.name = name;
        }
    }
}
