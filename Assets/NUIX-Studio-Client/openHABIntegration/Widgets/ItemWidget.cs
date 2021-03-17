using UnityEngine;

public class ItemWidget : MonoBehaviour
{
    [Header("Item & Server Setup")]
    [Tooltip("Item name in openhab. ie. gf_Hallway_Light")]
    public string item;
    [Tooltip("If you wan't to subscribe to events on this item. What event. Usually StateChanged")]
    public EvtType _SubscriptionType = EvtType.ItemStateChangedEvent;

    protected ItemController ConnectedItemController
    {
        get
        {
            return SemanticModel.getInstance().items[item].itemController;
        }
        set
        {
            SemanticModel.getInstance().items[item].itemController = value;
        }
    }
}