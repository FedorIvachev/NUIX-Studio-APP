using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class StringWidget : ItemWidget
{
    [Header("Widget Setup")]
    public TextMeshPro text;


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
        if (text == null)
        {
            text = GetComponent<TextMeshPro>();
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
        text.text = itemController.GetItemStateAsString();
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
    }

    /// <summary>
    /// Stop event listening from controller
    /// </summary>
    void OnDisable()
    {
        itemController.updateItem -= OnUpdate;
    }
}
