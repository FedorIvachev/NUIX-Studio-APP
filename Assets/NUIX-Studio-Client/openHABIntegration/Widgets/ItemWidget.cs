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
    }

    public void SetItemController(ItemController setItemComtroller)
    {
        //itemController = SemanticModel.getInstance().items[item].itemController;
        //itemController.ItemId = item;

        itemController = setItemComtroller;
        itemController.updateItem += OnUpdate;
    }
}