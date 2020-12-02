using Microsoft.MixedReality.Toolkit.Utilities;
using System;
using UnityEngine;
using TMPro;
using Microsoft.MixedReality.Toolkit.UI;

namespace Tsinghua.HCI.IoThingsLab
{
    /// <summary>
    /// Requires GridObjectCollection;
    /// Adds buttons used for instantiating the defined items
    /// </summary>
    /// 
    [ExecuteAlways]
    public class ThingDesignerItemCollection : MonoBehaviour
    {
        [Tooltip("Button for adding items. Use ThingItem.prefab by default")]
        [SerializeField] public GameObject _buttonPrefab;

        GridObjectCollection gridObjectCollection;

        [Tooltip("The items to be used in the Thing Designer")]
        [SerializeField] public GameObject[] items;

        public void Start()
        {
            gridObjectCollection =  GetComponent<GridObjectCollection>();

        }

        /// <summary>
        /// Adds buttons into collection, responsible for instantiating a corresponding item
        /// </summary>
        public void AddItemsToCollection()
        {
            foreach (GameObject item in items)
            {
                GameObject button = _buttonPrefab;
                GameObject cloned_button = Instantiate(button);
                cloned_button.transform.SetParent(gameObject.transform);
                cloned_button.name = "Button for " + item.name;
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
