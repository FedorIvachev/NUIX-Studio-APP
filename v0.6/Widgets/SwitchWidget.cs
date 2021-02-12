using Microsoft.MixedReality.Toolkit.UI;
using UnityEngine;
using UnityEngine.UI;

public class SwitchWidget : ItemWidget
{
    [Header("Widget Setup")]
    public Interactable _Toggle;

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

        _itemController.Initialize(_Server, _Item, _SubscriptionType);
        _itemController.updateItem += OnUpdate;
        InitWidget();

    }

    /// <summary>
    /// For public field initialization etc. This is to be able to use
    /// a generic start function for all widgets. This function is called for
    /// at end of Start()
    /// </summary>
    private void InitWidget()
    {
        if (_Toggle == null)
        {
            _Toggle = GetComponent<Interactable>();
        }
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
        _Toggle.IsToggled = _itemController.GetItemStateAsSwitch();
    }

    /// <summary>
    /// Call this from ie OnButtonClicked() event in Unity UI
    /// Update item from UI. Call itemcontroller and update Item on server.
    /// If update is true, an event will be recieved. If state is equal no
    /// new UI update is necesarry. If not equal the PUT has failed and we need
    /// to revert UI state to server state.
    /// </summary>
    public void OnSetItem()
    {
        //_itemController.SetItemStateAsSwitch(_Toggle.isOn);
        _itemController.SetItemStateAsSwitch(_Toggle.IsToggled);
    }

    /// <summary>
    /// Stop event listening from controller
    /// </summary>
    void OnDisable()
    {
        _itemController.updateItem -= OnUpdate;
    }
}
