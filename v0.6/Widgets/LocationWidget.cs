using UnityEngine;
using System.Globalization;
using TMPro;
using System.Threading.Tasks;

public class LocationWidget : ItemWidget
{
    [Header("Widget Setup")]
    public float location;

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

        LocationControl();
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
        location = float.Parse(_itemController.GetItemStateAsString());
        SynchronizeParentLocation();
    }

    /// <summary>
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
        _itemController.updateItem -= OnUpdate;
    }

    /// <summary>
    /// Is used only for location items
    /// TODO: Move it to a different class
    /// </summary>
    public void LocationControl()
    {
        InvokeRepeating(nameof(SetNameEqualToNumber), 0.0f, 1.0f);
    }

    private void SetNameEqualToNumber()
    {
        if (_itemController._Item != null)
        {
            int length = _itemController._ItemId.Length;
            print("HSSHJSBHJSBJH " + _itemController._ItemId);
            if (_itemController._ItemId[length - 1] == 'X') location = transform.parent.position.x;
            if (_itemController._ItemId[length - 1] == 'Y') location = transform.parent.position.y;
            if (_itemController._ItemId[length - 1] == 'Z') location = transform.parent.position.z;
            _itemController.SetItemStateAsString(location.ToString());
        }
    }

    private void SynchronizeParentLocation()
    {
        if (_itemController._Item != null)
        {
            int length = _itemController._ItemId.Length;
            if (_itemController._ItemId[length - 1] == 'X') transform.parent.position.Set(location, transform.parent.position.y, transform.parent.position.z);
            if (_itemController._ItemId[length - 1] == 'Y') transform.parent.position.Set(transform.parent.position.x, location, transform.parent.position.z);
            if (_itemController._ItemId[length - 1] == 'Z') transform.parent.position.Set(transform.parent.position.x, transform.parent.position.y, location);
        }
    }
}