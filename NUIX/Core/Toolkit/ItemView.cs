using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.MixedReality.Toolkit.UI;
using UnityEngine;
using UnityEngine.UI;


namespace Tsinghua.NUIX
{

    public class ItemView : MonoBehaviour
    {
        public Interactable button;

        private ItemData itemData;

        public void InitItem(ItemData item, Action<ItemData> callback)
        {
            this.itemData = item;

            button.OnClick.AddListener(() => callback(itemData));
        }
    }
}