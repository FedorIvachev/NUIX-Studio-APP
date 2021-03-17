using System.Collections.Generic;
using UnityEngine;

public class Item
{
    public EnrichedGroupItemDTO itemModel { get; set; }

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
}