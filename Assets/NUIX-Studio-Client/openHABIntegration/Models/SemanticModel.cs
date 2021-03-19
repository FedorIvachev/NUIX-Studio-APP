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

    public Dictionary<string, Item> items = new Dictionary<string, Item>();

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

    public void AddItem(EnrichedGroupItemDTO enrichedGroupItemDTO, List<GameObject> Widgets)
    {
        if (items.ContainsKey(enrichedGroupItemDTO.name))
        {
            // We already have this item in the semanticmodel
            return;
        }
        else
        {
            Item item = new Item
            {
                ItemModel = enrichedGroupItemDTO,
                itemController = new ItemController
                {
                    _ItemId = enrichedGroupItemDTO.name
                }
            };

            // Add the item widgets
            foreach (GameObject itemWidget in Widgets)
            {
                item.AddWidget(itemWidget);
            }

            // Add the created item to the SemanticModel dict
            items[enrichedGroupItemDTO.name] = item;
        }
    }

    
    public void RemoveItem(string itemId)
    {
        Debug.Log("REMOVING ITEM");
        items[itemId].DestroyWidgets();
        items.Remove(itemId);
    }
    
}