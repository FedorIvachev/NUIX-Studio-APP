using System.Collections;
using UnityEngine;
using EvtSource;
using Proyecto26;

public class ItemController : MonoBehaviour
{
    private string _ServerURL = "http://localhost:8080"; //URL to server rest api ie. http://openhab:8080/rest
    private string _ItemId; // name of item in openhab
    private ItemModel _Item; // The item this controller handles
    private EvtType _SubscribeTo; //Subscribe to this eventtype or if none, don't subscribe
    private EventController _EventController;

    public delegate void OnItemUpdate();
    public OnItemUpdate updateItem;

    public bool OnItemUpdated { get; set; } // used to block the update of the item while the state on the server is updating

    /// <summary>
    /// Initialize the ItemController.
    /// </summary>
    /// <param name="serverUrl">server url from widget</param>
    /// <param name="itemId">item name from widget</param>
    /// <param name="subType">subscription type from widget</param>
    public void Initialize(string serverUrl, string itemId, EvtType subType)
    {
        _ServerURL = serverUrl;
        _ItemId = itemId;
        _SubscribeTo = subType;
        OnItemUpdated = true;
        GetItemFromServer();
        if (_SubscribeTo != EvtType.None)
        {
            _EventController = GameObject.FindGameObjectWithTag("Eventbus").GetComponent<EventController>();
            if (_EventController != null)
            {
                _EventController.Subscribe(gameObject);
            }
            else
            {
                Debug.Log("Failed to find Eventbus. Are you sure you have an EventController attached to a GameObject with \"Eventbus\" tag.");
            }

        }
    }

    /// <summary>
    /// Eventcontroller passes an event to ItemController
    /// </summary>
    /// <param name="evt">the event as eventmodel</param>
    public void ReceivedEvent(EventModel evt)
    {
        if (_Item.state != evt._Payload.value) _Item.state = evt._Payload.value;
        Debug.Log("Event value: " + evt._Payload.value + " New Item state value: " + _Item.state);
        if (OnItemUpdated) updateItem?.Invoke(); // Send event to Widgets for UI updates
    }

    /// <summary>
    /// Make a REST call and propagate this object with data
    /// </summary>
    private void GetItemFromServer()
    {
        RestClient.Get(UriBuilder.GetItemUri(_ServerURL, _ItemId)).Then(response => {
            if (response.StatusCode >= 200 && response.StatusCode < 300)
            {
                _Item = JsonUtility.FromJson<ItemModel>(response.Text);
                //Debug.Log(_Item.ToString());
                if (OnItemUpdated) updateItem?.Invoke(); // Send event to Widgets
            }
            else
            {
                Debug.Log("Rest GET status for Item: " + _ItemId + " was not in OK span. (" + response.StatusCode + ")\n" + response.Error);
            }
        });
    }

    /// <summary>
    /// Update item state with POST method (plaintext)
    /// </summary>
    /// <param name="newState">New state</param>
    private void SetItemOnServer(string newState)
    {
        OnItemUpdated = false;
        Debug.Log("New state: " + newState);
        RestClient.DefaultRequestHeaders["content-type"] = "text/plain";
        RestClient.Post(UriBuilder.GetItemUri(_ServerURL, _ItemId), newState).Then(response => {
            //Debug.Log("Posted " + newState + " to " + _ItemId + ". With responsecode " + response.StatusCode);
            OnItemUpdated = true;
            RestClient.ClearDefaultHeaders();
        });
    }

    /// <summary>
    /// Creates an item on server with PUT method (GroupItemDTO item)
    /// </summary>
    /// <param name="item"></param>
    public void CreateItemOnServer(GroupItemDTO item)
    {
        RestClient.DefaultRequestHeaders["Authorization"] = "Bearer eyJraWQiOm51bGwsImFsZyI6IlJTMjU2In0.eyJpc3MiOiJvcGVuaGFiIiwiYXVkIjoib3BlbmhhYiIsImV4cCI6MTYxMzYwNjkxMywianRpIjoiX0Z6VERZVC1JZzdXMmRPOWNfcWM0ZyIsImlhdCI6MTYxMzYwMzMxMywibmJmIjoxNjEzNjAzMTkzLCJzdWIiOiJhZG1pbiIsImNsaWVudF9pZCI6Imh0dHA6Ly9sb2NhbGhvc3Q6ODA4MCIsInNjb3BlIjoiYWRtaW4iLCJyb2xlIjpbImFkbWluaXN0cmF0b3IiXX0.KCaO6zRlK8IwGQ-4jgFLYhwnsGcyq3dVKLdFWLJk8-IK_P4YNVy6jLzBCwY_A2wcn612QtygJ_NxEGLW5j9zPgKSkFlH1hpdoWIlLBubjcS4Dmwdy36WhlFlzWOGdNXmY0OvMt1ylTpwRrfWoVCqqRnd5Mtf-FvjkMsWk9vwJ80z5FsycVTJ-yzNKHIHtfQS0ZMPVLSh0sNc--_rpsxX9xJVcKgRd6VEV5DGxcblRwSKLD01RVAZurSyQsNFXpgrK3Zhc0cI91EopGrPd8iDjYZVFIJT9amWYKFSHrn9_XOkYFlUzv_NFQWD57KorLf-GjXds7VuujT1txU711-Dnw";
        OnItemUpdated = false;
        Debug.Log("Creating item");
        RestClient.DefaultRequestHeaders["content-type"] = "application/json";
        RequestHelper currentRequest;
        currentRequest = new RequestHelper
        {
            Uri = UriBuilder.GetItemUri(_ServerURL, "example_location"),
            //Uri = "https://jsonplaceholder.typicode.com",
            //Body = JsonUtility.ToJson(item),
            Body = item,
            Retries = 5,
            RetrySecondsDelay = 1,
            RetryCallback = (err, retries) => {
                Debug.Log(string.Format("Retry #{0} Status {1}\nError: {2}", retries, err.StatusCode, err));
            }
        };

        //print(JsonUtility.ToJson(item));
        /*
        RestClient.Put<PutItemResponseModel>(currentRequest, (err, res, body) => {
            if (err != null)
            {
                print("Error creating item on server");
                print(err.Message);
            }
            else
            {
                print("Success" + JsonUtility.ToJson(body, true));
            }
        });
        */
        /*
        RestClient.Put(UriBuilder.GetItemUri(_ServerURL, "example_location"), item).Then(response => {
            //Debug.Log("Posted " + newState + " to " + _ItemId + ". With responsecode " + response.StatusCode);
            print("Create item response: " + response.Text);
            OnItemUpdated = true;
            RestClient.ClearDefaultHeaders();
        });
        */

        RestClient.Put<string>(currentRequest, (err, res, body) => {
            if (err != null)
            {
                print("Error creating item on server");
                print(err.Message);
            }
            else
            {
                print("Success" + JsonUtility.ToJson(body, true));
            }
        });

    }

    /// <summary>
    /// Return Item state (value) as string
    /// </summary>
    /// <returns>string with current state</returns>
    public string GetItemStateAsString()
    {
        return _Item.state.ToString();
    }

    /// <summary>
    /// Try to parse the state as a float
    /// </summary>
    /// <returns>result if float parse try</returns>
    public float GetItemStateAsFloat()
    {
        float result;
        float.TryParse(_Item.state, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.CurrentCulture, out result);
        return result;
    }

    /// <summary>
    /// Parse item value to integer. Assume dimmer state is between
    /// 0 - 100. If not, return -1 for error. (Disable slider in that case)
    /// </summary>
    /// <returns>Dimmer as percent in integer</returns>
    public float GetItemStateAsDimmer()
    {
        float value = 0;
        value = float.Parse(_Item.state);
        //Debug.Log("Value parsed to: " + value);
        if (value >= 0f && value <= 100f)
        {
            return value;
        }
        else
        {
            return -1f;
        }
    }

    /// <summary>
    /// Update dimmer item on server
    /// </summary>
    /// <param name="value">Needs to be integer between or equal to 0 and 100</param>
    public void SetItemStateAsDimmer(int value)
    {
        if (value >= 0 && value <= 100) SetItemOnServer(value.ToString());
    }

    /// <summary>
    /// Try to parse the state as a switch
    /// </summary>
    /// <returns>Switch state as bool (ON = True, OFF=False)</returns>
    public bool GetItemStateAsSwitch()
    {
        if (_Item.state == "ON") return true;
        return false;
    }

    /// <summary>
    /// Update item on server as a switch
    /// </summary>
    /// <param name="newState">bool True/False On/Off</param>
    public void SetItemStateAsSwitch(bool newState)
    {
        if (newState)
        {
            SetItemOnServer("ON");
        }
        else
        {
            SetItemOnServer("OFF");
        }
    }

    /// <summary>
    /// Set a Player Item Play/Pause state
    /// </summary>
    /// <param name="newState"></param>
    public void SetItemStateAsPlayerPlayPause(bool newState)
    {
        if (newState)
        {
            SetItemOnServer("PLAY");
        }
        else
        {
            SetItemOnServer("PAUSE");
        }
    }

    /// <summary>
    /// Search functions on Player Item
    /// </summary>
    /// <param name="cmd">ff = Fastforward, rw = Rewind, next = Next, prev = Previous</param>
    public void SetItemStateAsPlayerSearch(string cmd)
    {
        switch (cmd)
        {
            case "ff":
                SetItemOnServer("FASTFORWARD");
                break;
            case "rw":
                SetItemOnServer("REWIND");
                break;
            case "next":
                SetItemOnServer("NEXT");
                break;
            case "prev":
                SetItemOnServer("PREVIOUS");
                break;
            default:
                Debug.Log("Command is not recognized by player item");
                break;
        }
    }


    /// <summary>
    /// Getter for item ID
    /// </summary>
    /// <returns></returns>
    public string GetItemID()
    {
        return _ItemId;
    }

    /// <summary>
    /// Getter for subtype
    /// </summary>
    /// <returns></returns>
    public EvtType GetItemSubType()
    {
        return _SubscribeTo;
    }
}
/// <summary>
/// EventTypes available from Event stream of items
/// </summary>
public enum EvtType
{
    ItemStateChangedEvent,
    ItemStateEvent,
    ItemCommandEvent,
    ItemUpdatedEvent,
    ItemRemovedEvent,
    ItemAddedEvent,
    None

    /**
    ItemAddedEvent          /added
    ItemRemovedEvent        /removed
    ItemUpdatedEvent        /updated
    ItemCommandEvent        /command
    ItemStateEvent          /state
    ItemStateChangedEvent   /statechanged   (This is 99% of times what we want)
    **/
}