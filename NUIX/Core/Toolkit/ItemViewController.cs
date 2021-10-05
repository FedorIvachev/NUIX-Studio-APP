using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tsinghua.NUIX
{

    public class ItemViewController : MonoBehaviour
    {
        public Inventory inventoryHolder;
        public Transform inventoryViewParent;
        public Transform infoViewParent;


        private GameObject infoViewPrefab;
        private GameObject itemViewPrefab;

        private GameObject infoView;


        public void Start()
        {
            itemViewPrefab = (GameObject)Resources.Load("Item");
            infoViewPrefab = (GameObject)Resources.Load("InfoView");

            foreach (var item in inventoryHolder.inventory)
            {
                var itemGO = GameObject.Instantiate(itemViewPrefab, inventoryViewParent);
                itemGO.GetComponent<ItemView>().InitItem(item, CreateInfoView);
            }
        }

        private void CreateInfoView(ItemData data)
        {
            if (infoView != null)
            {
                Destroy(infoView);
            }

            infoView = GameObject.Instantiate(infoViewPrefab, infoViewParent);
            infoView.GetComponent<InfoView>().Init(data);
        }

    }
}