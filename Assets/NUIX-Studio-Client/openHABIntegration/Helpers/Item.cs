using System.Collections.Generic;
using UnityEngine;

public class Item
{
    public EnrichedGroupItemDTO _itemModel { get; set; }

    public List<GameObject> _itemWidgets;

    public void AddWidget(GameObject itemWidget)
    {
        if (_itemWidgets == null)
        {
            _itemWidgets = new List<GameObject>();
        }

        _itemWidgets.Add(itemWidget);
    }
}