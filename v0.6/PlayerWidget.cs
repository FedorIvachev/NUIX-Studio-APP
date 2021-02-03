using UnityEngine;
using UnityEngine.UI;

public class PlayerWidget : MonoBehaviour
{
    [Header("Item & Server Setup")]
    [Tooltip("Server url with port if needed. ie. http://localhost:8080")]
    public string _Server = "http://openhab:8080";
    [Tooltip("Item name in openhab. ie. gf_Hallway_Light")]
    public string _Item;
    [Tooltip("If you wan't to subscribe to events on this item. What event. Usually StateChanged")]
    public EvtType _SubscriptionType = EvtType.ItemStateChangedEvent;

    [Header("Widget Setup")]
    public Toggle _playPauseToggle;
    public Image _playPauseButtonImage;
    public Sprite _playIcon;
    public Sprite _pauseIcon;
    [SpaceAttribute(10)]
    public Image _muteButtonImage;
    public Sprite _mutedIcon;
    public Sprite _unmutedIcon;
    public SwitchWidget _muteSwitch; // Example of how to control a widget from an other widget

    private ItemController _itemController;

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
        _muteSwitch.gameObject.GetComponent<ItemController>().updateItem += OnMuteUpdate; //Subscribe to mutebutton events in this script
        InitWidget();

    }

    /// <summary>
    /// For public field initialization etc. This is to be able to use
    /// a generic start function for all widgets. This function is called for
    /// at end of Start()
    /// </summary>
    private void InitWidget()
    {
        if (_muteSwitch == null) Debug.Log("Muteswitch not assigned. in Playerwidget");

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
        if (_itemController.GetItemStateAsString() == "PLAY")
        {
            _playPauseButtonImage.sprite = _pauseIcon;
        }
        else
        {
            _playPauseButtonImage.sprite = _playIcon;
        }
    }

    /// <summary>
    /// Update item from UI. Call itemcontroller and update Item on server.
    /// If update is true, an event will be recieved. If state is equal no
    /// new UI update is necesarry. If not equal the PUT has failed and we need
    /// to revert UI state to server state.
    /// 
    /// Player item has more then bool state commands
    /// </summary>
    public void OnSetPlayPauseItem()
    {
        if (_itemController.GetItemStateAsString() == "PLAY")
        {
            _itemController.SetItemStateAsPlayerPlayPause(false);
        }
        else if (_itemController.GetItemStateAsString() == "PAUSE")
        {
            _itemController.SetItemStateAsPlayerPlayPause(true);
        }
        else
        {

        }
    }

    /// <summary>
    /// Use player widget to update the muteswitch widget for custom image swaps.
    /// </summary>
    public void OnMuteUpdate()
    {
        if (_muteSwitch.gameObject.GetComponent<ItemController>().GetItemStateAsSwitch())
        {
            // Mute is on
            _muteButtonImage.sprite = _mutedIcon;
        }
        else
        {
            _muteButtonImage.sprite = _unmutedIcon;
        }
    }

    /// <summary>
    /// Next button action
    /// </summary>
    public void OnSetNext()
    {
        _itemController.SetItemStateAsPlayerSearch("next");
    }

    /// <summary>
    /// Previous button action
    /// </summary>
    public void OnSetPrevious()
    {
        _itemController.SetItemStateAsPlayerSearch("prev");
    }

    /// <summary>
    /// Fastforward action
    /// </summary>
    public void OnSetFf()
    {
        _itemController.SetItemStateAsPlayerSearch("ff");
    }

    /// <summary>
    /// Rewind action
    /// </summary>
    public void OnSetRw()
    {
        _itemController.SetItemStateAsPlayerSearch("rw");
    }

    /// <summary>
    /// Stop event listening from controller
    /// </summary>
    void OnDisable()
    {
        _itemController.updateItem -= OnUpdate;
        _muteSwitch.gameObject.GetComponent<ItemController>().updateItem -= OnMuteUpdate;
    }
}