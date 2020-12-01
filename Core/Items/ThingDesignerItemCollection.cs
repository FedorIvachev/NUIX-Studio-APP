using Microsoft.MixedReality.Toolkit.Utilities;
using System;
using UnityEngine;
using TMPro;
using Microsoft.MixedReality.Toolkit.UI;

namespace Tsinghua.HCI.IoThingsLab
{
    /// <summary>
    /// Operates with connected children as items
    /// Requires GridObjectCollection
    /// </summary>
    /// 
    [ExecuteAlways]
    public class ThingDesignerItemCollection : MonoBehaviour
    {
        [SerializeField] public GameObject _buttonPrefab;

        GridObjectCollection gridObjectCollection;

        [SerializeField] public GameObject[] items;

        public void Start()
        {
            gridObjectCollection =  GetComponent<GridObjectCollection>();

        }

        public void AddItemsToCollection()
        {
            foreach (GameObject item in items)
            {
                GameObject button = _buttonPrefab;
                GameObject cloned_button = Instantiate(button);
                cloned_button.transform.SetParent(gameObject.transform);
                cloned_button.name = "ThingItem" + transform.childCount;
                cloned_button.GetComponent<ButtonConfigHelper>().MainLabelText = item.name;
                cloned_button.GetComponent<ItemCreator>().itemPrefab = item;
            }
            
            gridObjectCollection.UpdateCollection();
            OnAddItemToCollection?.Invoke(this);
        }
        
        /// <summary>
        /// Action called when collection is updated
        /// </summary>
        public Action<ThingDesignerItemCollection> OnAddItemToCollection { get; set; }
    }
}
