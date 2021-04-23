using UnityEngine;

public abstract class ItemWidget : MonoBehaviour
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
            if (SemanticModel.getInstance().items.ContainsKey(item)) return SemanticModel.getInstance().items[item].itemController;
            else return null;
        }
        set
        {
            SemanticModel.getInstance().items[item].itemController = value;
        }
    }

    public bool connectedToServer = false;

    public abstract void OnUpdate();

    public virtual void Start()
    {
        if (connectedToServer) ConnectedItemController.updateItem += OnUpdate;
    }

    public void OnWidgetConnectedToServer()
    {
        connectedToServer = true;
        ConnectedItemController.updateItem += OnUpdate;
        Debug.Log("Widget " + item + " connected to the server updates");
    }

    public void OnWidgetDisconnectedFromServer()
    {
        connectedToServer = false;
        ConnectedItemController.updateItem -= OnUpdate;
    }

}