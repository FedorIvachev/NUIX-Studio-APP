using System.Collections.Generic;
using UnityEngine;

public class SemanticModel
{
    public static SemanticModel instance;
    public static SemanticModel getInstance()
    {
        if (instance == null)
            instance = new SemanticModel();
        return instance;
    }

    public Dictionary<string, Item> _items = new Dictionary<string, Item>();

    private Vector3 spawnPosition = Vector3.one;
    public Vector3 SpawnPosition
    {
        get 
        {
            spawnPosition.Set(spawnPosition.x, spawnPosition.y, spawnPosition.z - 0.2f);
            return spawnPosition; 
        }
        set
        {
            spawnPosition = value;
        }
    }

    /// <summary>
    /// TODO
    /// </summary>
    /// <param name="itemID"></param>
    /// <param name="itemWidget"></param>
    public void AddWidget(string itemID, GameObject itemWidget)
    {
        if (_items.ContainsKey(itemID))
        {
            _items[itemID].AddWidget(itemWidget);
        }
        else
        {
            Item item = new Item();
            item.AddWidget(itemWidget);
            //item._itemModel = itemWidget.GetComponent<ItemWidget>()
        }
    }
}