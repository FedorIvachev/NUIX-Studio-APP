using UnityEngine;
using System.Globalization;

public class GroupWidget : ItemWidget
{
    [Header("Widget Setup")]
    //public Text _dummyText;
    public string _state = "";
    public bool _isNumber = false;
    public string _numberFormat = "0.00";
    public string _culture = "en-GB";
    public string _preText = "";
    public string _postText = "";

    /// <summary>
    /// Initialize ItemController
    /// </summary>
    void Start()
    {
        ConnectedItemController.updateItem += OnUpdate;
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
        string txt = ConnectedItemController.GetItemStateAsString();
        if (_isNumber)
        {
            float number = float.Parse(txt);
            _state = _preText + number.ToString(_numberFormat, CultureInfo.CreateSpecificCulture(_culture)) + _postText;
        }
        else
        {
            _state = _preText + txt + _postText;
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

    }

    /// <summary>
    /// Stop event listening from controller
    /// </summary>
    void OnDisable()
    {
        ConnectedItemController.updateItem -= OnUpdate;
    }
}