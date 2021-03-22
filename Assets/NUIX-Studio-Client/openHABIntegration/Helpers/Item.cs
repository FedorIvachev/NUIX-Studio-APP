using System.Collections.Generic;
using UnityEngine;

public class Item
{
    public EnrichedGroupItemDTO ItemModel { get; set; }

    public List<GameObject> _itemWidgets;

    public ItemController itemController;

    public void AddWidget(GameObject itemWidget)
    {
        if (_itemWidgets == null)
        {
            _itemWidgets = new List<GameObject>();
        }

        _itemWidgets.Add(itemWidget);
    }

    public void DestroyWidgets()
    {

        // TODO: free full memory by destroying each of the objects
        // C# does not perform destructor on the object delete, so need to make it somehow different
        Debug.Log("Destroying item " + ItemModel.name);
        foreach(GameObject itemWidget in _itemWidgets)
        {
            Object.Destroy(itemWidget);
        }
    }
}