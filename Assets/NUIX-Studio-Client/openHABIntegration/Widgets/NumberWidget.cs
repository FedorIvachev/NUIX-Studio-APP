using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// A widget representing a Number item.
/// Does not take dimensions into account, thus it is stored in String.
/// User needs to parse the number in number widget himself.
/// </summary>
public class NumberWidget : ItemWidget
{
    [Header("Widget Setup")]
    public TextMeshPro text;

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
    /// Call this from ie OnButtonClicked() or other events in Unity UI
    /// Update item from UI. Call itemcontroller and update Item on server.
    /// If update is true, an event will be recieved. If state is equal no
    /// new UI update is necesarry. If not equal the PUT has failed and we need
    /// to revert UI state to server state.
    /// </summary>
    public void OnSetItem()
    {
        itemController.SetItemStateAsString(text.text);
    }

    /// <summary>
    /// Stop event listening from controller
    /// </summary>
    void OnDisable()
    {
        itemController.updateItem -= OnUpdate;
    }
}
