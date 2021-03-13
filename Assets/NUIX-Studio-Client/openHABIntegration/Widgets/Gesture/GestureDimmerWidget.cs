using Microsoft.MixedReality.Toolkit.UI;
using UnityEngine;

using Tsinghua.HCI.IoThingsLab;

public class GestureDimmerWidget : ItemWidget 
{
    public GestureThumbsUpRotated _Gesture;

    GestureDimmerWidget()
    {
        _SubscriptionType = EvtType.None;
    }

    /// <summary>
    /// Initialize ItemController
    /// </summary>
    void Start()
    {
        // Add or get controller component
        if (GetComponent<ItemController>() != null)
        {
            _itemController = GetComponent<ItemController>();
        }
        else
        {
            _itemController = gameObject.AddComponent<ItemController>();
        }

        _itemController.Initialize(_Item, _SubscriptionType);


        // OnUpdate is not needed for Gestures only OnSetItem
        //_itemController.updateItem += OnUpdate;
        InitWidget();
    }

    /// <summary>
    /// For public field initialization etc. This is to be able to use
    /// a generic start function for all widgets. This function is called for
    /// at end of Start()
    /// </summary>
    private void InitWidget()
    {
        if (_Gesture == null) _Gesture = GetComponent<GestureThumbsUpRotated>();
        InvokeRepeating(nameof(OnSetItem), 1.0f, 0.1f);
    }

    /// <summary>
    /// When an item updates from server. This function is
    /// called from ItemController when Item is Updated on server.
    /// Begin with a check if Item and UI state is equal. Otherwise we
    /// might get flickering as the state event is sent after update from
    /// UI. This will Sync as long as Event Stream is online.
    /// </summary>
    public void OnUpdate()
    {

    }

    /// <summary>
    /// Update item from UI. Call itemcontroller and update Item on server.
    /// If update is true, an event will be recieved. If state is equal no
    /// new UI update is necesarry. If not equal the PUT has failed and we need
    /// to revert UI state to server state.
    /// </summary>
    public void OnSetItem()
    {
        if (_Gesture.TryGetNormalizedValue(out uint value))
        {
            _itemController.SetItemStateAsDimmer((int) value);
        }
    }

    /// <summary>
    /// Stop event listening from controller
    /// </summary>
    void OnDisable()
    {
        _itemController.updateItem -= OnUpdate;
    }
}