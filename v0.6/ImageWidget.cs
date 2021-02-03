using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class ImageWidget : MonoBehaviour
{
    [Header("Item & Server Setup")]
    [Tooltip("Server url with port if needed. ie. http://localhost:8080")]
    public string _Server = "http://openhab:8080";
    [Tooltip("Item name in openhab. ie. gf_Hallway_Light")]
    public string _Item;
    [Tooltip("If you wan't to subscribe to events on this item. What event. Usually StateChanged")]
    public EvtType _SubscriptionType = EvtType.ItemStateChangedEvent;

    [Header("Widget Setup")]
    public Image _imageGameObject;
    public Sprite _defaultImage;


    public string _currentUrl;

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
        InitWidget();

    }

    /// <summary>
    /// For public field initialization etc. This is to be able to use
    /// a generic start function for all widgets. This function is called for
    /// at end of Start()
    /// </summary>
    private void InitWidget()
    {
        if (_imageGameObject == null)
        {
            _imageGameObject = GetComponent<Image>();
        }
        if (_defaultImage != null) _imageGameObject.sprite = _defaultImage;
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
        if (_itemController.GetItemStateAsString() != _currentUrl) StartCoroutine(AsyncImageUpdate());
    }

    /// <summary>
    /// Coroutine for WebRequest. Make that shit Async per Unity standard.
    /// </summary>
    /// <returns></returns>
    private IEnumerator AsyncImageUpdate()
    {
        // Check so there is a valid adress here. Small sanity check.
        if (_itemController.GetItemStateAsString() != "" &&
            _itemController.GetItemStateAsString() != "UNDEF" &&
            _itemController.GetItemStateAsString() != "NULL" &&
            _itemController.GetItemStateAsString() != "UNDEFINED")
        {
            _currentUrl = _itemController.GetItemStateAsString();
            UnityWebRequest www = UnityWebRequestTexture.GetTexture(_currentUrl);
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                // Set default image don't download image correctly
                if (_defaultImage != null) _imageGameObject.sprite = _defaultImage;
                Debug.Log("There where an error downloading image from URL\n" + www.error);
            }
            else
            {
                Texture2D texture = ((DownloadHandlerTexture)www.downloadHandler).texture;
                _imageGameObject.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0, 0));
            }

        }
        else
        {
            _currentUrl = _itemController.GetItemStateAsString();
            _imageGameObject.sprite = _defaultImage;
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
        _itemController.updateItem -= OnUpdate;
    }
}