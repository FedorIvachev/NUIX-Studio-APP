using Microsoft.MixedReality.Toolkit.UI;
using UnityEngine;

public abstract class ItemWidget : MonoBehaviour
{
    [Header("Item & Server Setup")]
    [Tooltip("Item name in openhab. ie. gf_Hallway_Light")]
    public string item = "None";
    [Tooltip("If you wan't to subscribe to events on this item. What event. Usually StateChanged")]
    protected ItemController itemController;


    public abstract void OnUpdate();

    public virtual void Start()
    {
        // If there is bo itemController attached, it means that this widget was created outside the SemanticModelController
        // In this case synchronizing the data requires ItemDTo (including item.state), which need to be created additionaly
        if (itemController == null)
        {
            itemController = new ItemController(item, EvtType.None);
        }
    }

    public void SetItemController(ItemController setItemComtroller)
    {
        itemController = setItemComtroller;
        itemController.updateItem += OnUpdate;
    }
}