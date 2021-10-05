using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace Tsinghua.NUIX
{
    public class InfoView : MonoBehaviour
    {
        public TMPro.TMP_Text nameText;
        public TMPro.TMP_Text typeText;

        public void Init(ItemData data)
        {
            nameText.text = data.name;
            print(nameText.text);
            print(data.name);

            switch (data.type)
            {
                case ItemType.Button: typeText.text = "Type: Button"; break;
                case ItemType.Dimmer: typeText.text = "Type: Dimmer"; break;
                case ItemType.Switch: typeText.text = "Type: Switch"; break;
                case ItemType.Image: typeText.text = "Type: Image"; break;
            }
        }
    }
}