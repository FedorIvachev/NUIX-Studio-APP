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

        _itemController.updateItem += OnUpdate;

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
        _itemController.updateItem?.Invoke();
        InvokeRepeating(nameof(OnSetItem), 1.0f, 0.064f);
        if (SemanticModel.getInstance()._items.ContainsKey(_Item.Substring(0, _Item.Length - "VirtualLocation".Length)))
        {
            List<string> groupName = new List<string>();
            groupName.Add(_Item);
            SemanticModel.getInstance()._items[_Item.Substring(0, _Item.Length - "VirtualLocation".Length)]._itemModel.groupNames.Add(_Item);
            int tempcount = 1;
            if (GameObject.Find(_Item.Substring(0, _Item.Length - "VirtualLocation".Length) + " Widget") != null)
            {
                GameObject.Find(_Item.Substring(0, _Item.Length - "VirtualLocation".Length) + " Widget").transform.position = transform.position + Vector3.up / 5f * tempcount;
                tempcount += 1;
            }
            // TODO : get rid of hardcode, rewrite to support every tag
            if (GameObject.Find(_Item.Substring(0, _Item.Length - "VirtualLocation".Length) + " LightDimmerWidget") != null)
            {
                GameObject.Find(_Item.Substring(0, _Item.Length - "VirtualLocation".Length) + " LightDimmerWidget").transform.position = transform.position + Vector3.up / 5f * tempcount;
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
        Vector3 receivedPosition = _itemController.GetItemStateAsVector();
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
        if (_itemController._Item != null)
        {
            if (_itemController._Item.state != transform.position.ToString())
            {
                if (sentPosition != transform.position)
                {
                    sentPosition = transform.position;
                    _itemController.SetItemStateAsVector(sentPosition);
                }           
            }
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