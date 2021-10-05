using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tsinghua.NUIX
{

    public class Inventory : MonoBehaviour
    {
        public ItemData[] inventory;

        int index = 0;

        public void NextItemInfo()
        {
            if (index > inventory.Length)
            {
                index = 0;
            }

            Debug.Log("Item name: " + inventory[index].name);

            switch (inventory[index].type)
            {
                case ItemType.Switch:
                    Debug.Log("Item type: Switch");
                    break;

                case ItemType.Dimmer:
                    Debug.Log("Item type: Dimmer");
                    break;

                default:
                    Debug.Log("Item type: Another");
                    break;
            }

            index++;
        }

        private void Update()
        {

        }

    }
}