using UnityEngine;
using System.Globalization;
using TMPro;
using System.Threading.Tasks;
using System.Collections.Generic;

/// <summary>
/// Is used to sync the transform position of the item widget to the server
/// </summary>
public class LocationWidget : ItemWidget
{
    private Vector3 sentPosition = Vector3.zero;
    /// <summary>
    /// Initialize ItemController
    /// </summary>
    void Start()
    {

        ConnectedItemController.updateItem += OnUpdate;

        //VirtualLocationController.getInstance().locationSync += OnSetItem;

        //LocationControl();

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
        ConnectedItemController.updateItem?.Invoke();
        InvokeRepeating(nameof(OnSetItem), 1.0f, 0.064f);
        if (SemanticModel.getInstance().items.ContainsKey(item.Substring(0, item.Length - "VirtualLocation".Length)))
        {
            List<string> groupName = new List<string>();
            groupName.Add(item);
            SemanticModel.getInstance().items[item.Substring(0, item.Length - "VirtualLocation".Length)].ItemModel.groupNames.Add(item);
            int tempcount = 1;
            if (GameObject.Find(item.Substring(0, item.Length - "VirtualLocation".Length) + " Widget") != null)
            {
                GameObject.Find(item.Substring(0, item.Length - "VirtualLocation".Length) + " Widget").transform.position = transform.position + Vector3.up / 5f * tempcount;
                tempcount += 1;
            }
            // TODO : get rid of hardcode, rewrite to support every tag
            if (GameObject.Find(item.Substring(0, item.Length - "VirtualLocation".Length) + " LightDimmerWidget") != null)
            {
                GameObject.Find(item.Substring(0, item.Length - "VirtualLocation".Length) + " LightDimmerWidget").transform.position = transform.position + Vector3.up / 5f * tempcount;
            }
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
        Vector3 receivedPosition = ConnectedItemController.GetItemStateAsVector();
        if (receivedPosition != sentPosition) transform.position = receivedPosition;
    }

    /// <summary>
    /// Update item from UI. Call itemcontroller and update Item on server.
    /// If update is true, an event will be recieved. If state is equal no
    /// new UI update is necesarry. If not equal the PUT has failed and we need
    /// to revert UI state to server state.
    /// </summary>
    public void OnSetItem()
    {
        if (sentPosition != transform.position)
        {
            sentPosition = transform.position;
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
