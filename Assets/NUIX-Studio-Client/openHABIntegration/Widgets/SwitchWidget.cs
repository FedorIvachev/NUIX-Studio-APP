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
    public override void Start()
    {
        base.Start();
        InitWidget();

    }

    private void InitWidget()
    {
        if (_Toggle == null)
        {
            _Toggle = GetComponent<Interactable>();
        }
        itemController.updateItem?.Invoke();
    }

    /// <summary>
    /// This function is called from ItemController when Item is Updated on server.
    /// Begin with a check if Item and UI state is equal. Otherwise we
    /// might get flickering as the state event is sent after update from
    /// UI. This will Sync as long as Event Stream is online.
    /// </summary>
    public override void OnUpdate()
    {
        _Toggle.IsToggled = itemController.GetItemStateAsSwitch();
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
        itemController.SetItemStateAsSwitch(_Toggle.IsToggled);
    }

    /// <summary>
    /// Stop event listening from controller
    /// </summary>
    void OnDisable()
    {
        itemController.updateItem -= OnUpdate;
    }
}
