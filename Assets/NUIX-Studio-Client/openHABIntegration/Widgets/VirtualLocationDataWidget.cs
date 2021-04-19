using UnityEngine;
/// <summary>
/// A widget for the location item created by *VirtualLocationWidget
/// </summary>
public class VirtualLocationDataWidget : ItemWidget
{
    private Vector3 sentPosition = Vector3.zero;
    private GameObject virtualLocationWidget;

    /// <summary>
    /// Initialize ItemController
    /// </summary>
    void Start()
    {

        ConnectedItemController.updateItem += OnUpdate;

        InitWidget();
    }

    /// <summary>
    /// For public field initialization etc. This is to be able to use
    /// a generic start function for all widgets. This function is called for
    /// at end of Start()
    /// </summary>
    private void InitWidget()
    {
        //transform.position = _itemController.GetItemStateAsVector();
        virtualLocationWidget = GameObject.Find(item.Substring(0, item.Length - "_VirtualLocationData".Length) + " Widget"); // Only if it exists. TODO: make it find based on semanticmodel
        ConnectedItemController.updateItem?.Invoke();
        InvokeRepeating(nameof(OnSetItem), 1.0f, 0.064f);
    }

    /// <summary>
    /// When an item updates from server. This function is
    /// called from ItemController when Item is Updated on server.
    /// Begin with a check if Item and UI state is equal. Otherwise we
    /// might get flickering as the state event is sent after update from
    /// UI. This will Sync as long as Event Stream is online.
    /// </summary>
    public override void OnUpdate()
    {
        Vector3 receivedPosition = ConnectedItemController.GetItemStateAsVector();
        if (receivedPosition != sentPosition)
        {
            if (virtualLocationWidget != null)
                virtualLocationWidget.transform.position = receivedPosition;
        }
    }

    /// <summary>
    /// Update item from UI. Call itemcontroller and update Item on server.
    /// If update is true, an event will be recieved. If state is equal no
    /// new UI update is necesarry. If not equal the PUT has failed and we need
    /// to revert UI state to server state.
    /// </summary>
    public void OnSetItem()
    {
        if (sentPosition != virtualLocationWidget.transform.position)
        {
            if (virtualLocationWidget != null)
                sentPosition = virtualLocationWidget.transform.position;
            ConnectedItemController.SetItemStateAsVector(sentPosition);
        }
    }

    /// <summary>
    /// Stop event listening from controller
    /// </summary>
    void OnDisable()
    {
        ConnectedItemController.updateItem -= OnUpdate;
    }
}