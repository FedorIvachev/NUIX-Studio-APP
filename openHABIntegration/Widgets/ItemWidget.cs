using UnityEngine;

public class ItemWidget : MonoBehaviour
{
    [Header("Item & Server Setup")]
    [Tooltip("Server url with port if needed. ie. http://localhost:8080")]
    public string _Server = ClientConfig.getInstance()._ServerURL;
    [Tooltip("Item name in openhab. ie. gf_Hallway_Light")]
    public string _Item;
    [Tooltip("If you wan't to subscribe to events on this item. What event. Usually StateChanged")]
    public EvtType _SubscriptionType = EvtType.ItemStateChangedEvent;

    protected ItemController _itemController;



    protected void CreateLocationItem()
    {
        if (_itemController != null)
        {
            _itemController.CreateLocationItemOnServer();
        }
    }


}