using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Serializes and deserializes the cached itemdata and actiondata
/// </summary>
public class ItemService : MonoBehaviour
{
    private ItemsData itemsData;

    // Start is called before the first frame update
    void Start()
    {
        if (itemsData == null)
        {
            itemsData = new ItemsData();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public ItemsData GetItems()
    {
        return itemsData;
    }

    public void SetItems(ItemsData itemsData)
    {
        this.itemsData = itemsData;
    }


    /// <summary>
    /// Search the cached itemData and return it, if found
    /// </summary>
    /// <param name="itemID">ID of the item</param>
    /// <returns></returns>
    public ItemData GetItemDataByID(string itemID)
    {
        foreach (ItemData itemData in itemsData.ConcatItemsData())
        {
            if (itemData.itemID == itemID)
            {
                return itemData;
            }
        }
        return null;
    }

    /// <summary>
    /// Search the cached actionData and return it, if found
    /// </summary>
    /// <param name="actionID">ID of an action</param>
    /// <returns></returns>
    public ActionData GetActionDataByID(string actionID)
    {
        foreach (ActionData actionData in itemsData.actionData)
        {
            if (actionData.actionID == actionID)
            {
                return actionData;
            }
        }
        return null;
    }


    /// <summary>
    /// Recognizes the type of itemData and caches it
    /// </summary>
    /// <param name="itemData">The data to cache</param>
    public void AddItemData(ItemData itemData)
    {
        switch (itemData.itemType)
        {
            case ItemType.TEXTPLATE:
                {
                    TextPlateItemData textPlateItemData = new(itemData, "Sample text", true);
                    itemsData.textPlateItemsData.Add(textPlateItemData);
                    break;
                }
            case ItemType.LIGHT:
                {
                    LightItemData lightItemData = new(itemData, true, 0);
                    itemsData.lightItemsData.Add(lightItemData);
                    break;
                }
            case ItemType.BUTTON:
                {
                    ButtonItemData buttonItemData = new(itemData, 0);
                    itemsData.buttonItemsData.Add(buttonItemData);
                    break;
                }
            case ItemType.IMAGE:
                {
                    ImageItemData imageItemData = new(itemData, 0);
                    itemsData.imageItemsData.Add(imageItemData);
                    break;
                }
            case ItemType.VIDEO:
                {
                    VideoItemData videoItemData = new(itemData, 0);
                    itemsData.videoItemsData.Add(videoItemData);
                    break;
                }
            case ItemType.AUDIO:
                {
                    AudioItemData audioItemData = new(itemData, 0);
                    itemsData.audioItemsData.Add(audioItemData);
                    break;
                }
            case ItemType.WEIGHTSCALER:
                {
                    WeightScalerItemData weightScalerItemData = new(itemData, 1);
                    itemsData.weightScalerItemsData.Add(weightScalerItemData);
                    break;
                }
            default:
                {
                    // Pose, STT, Camera, WeightScaler go here, because they don't have any specific fields to serialize atm
                    itemsData.itemsData.Add(itemData);
                    break;
                }
        }
    }

    /// <summary>
    /// Caches the given actionData
    /// </summary>
    /// <param name="actionData">A unique uncached actionData</param>
    public void AddActionData(ActionData actionData)
    {
        if (!itemsData.actionData.Contains(actionData))
        {
            itemsData.actionData.Add(actionData);
        }
        else
        {
            Debug.Log("ActionData already in the list!");
        }
    }
}
