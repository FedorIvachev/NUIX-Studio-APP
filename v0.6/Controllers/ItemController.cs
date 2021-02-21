using System.Collections;
using UnityEngine;
using EvtSource;
using Proyecto26;
using System.IO;
using Microsoft.MixedReality.Toolkit.UI;
using System.Collections.Generic;

/// <summary>
/// Responsible for updating the state of the item
/// Usually is attached to the ItemWidget
/// </summary>
public class ItemController : MonoBehaviour
{
    private string _ServerURL = ClientConfig.getInstance()._ServerURL; //URL to server rest api ie. http://openhab:8080/rest
    public string _ItemId { get; set; } // name of item in openhab
    public EnrichedGroupItemDTO _Item; // The item this controller handles
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
                _Item = JsonUtility.FromJson<EnrichedGroupItemDTO>(response.Text);
                print("Rest GET status for Item: " + _Item.name + " was OK");
                if (OnItemUpdated) updateItem?.Invoke(); // Send event to Widgets
                if (_Item.type == "Group") // TODO move away to invoke method
                {
                    CreateLocationItemOnServer();
                    GetGroupItems();
                }
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


    // TODO: Create a widget for the item on Success calback
    /// <summary>
    /// Creates an item on server with PUT method (GroupItemDTO item)
    /// </summary>
    /// <param name="item"></param>
    public void CreateItemOnServer(GroupItemDTO item)
    {
        RestClient.DefaultRequestHeaders["Authorization"] = "Bearer eyJraWQiOm51bGwsImFsZyI6IlJTMjU2In0.eyJpc3MiOiJvcGVuaGFiIiwiYXVkIjoib3BlbmhhYiIsImV4cCI6MTYxMzk1MjQ1NiwianRpIjoiOG1KeUx5X1hpZWtmR2duQnJKeFJxZyIsImlhdCI6MTYxMzk0ODg1NiwibmJmIjoxNjEzOTQ4NzM2LCJzdWIiOiJhZG1pbiIsImNsaWVudF9pZCI6Imh0dHA6Ly9sb2NhbGhvc3Q6ODA4MCIsInNjb3BlIjoiYWRtaW4iLCJyb2xlIjpbImFkbWluaXN0cmF0b3IiXX0.BlZrstn6ZZobDf-5SKQC3xBGuLWtoZh4T8NP-6A20V5rcBvkH-yJH0RigDtguSp0NX0YxrOy8XrQHNICqs7EhtAt_mn3wiKvqv2dCpCx-QFSQveBhmwx4k5qXDmDWlHyBRejK6L_TZI6Mx2SuiDrtpbjtFMyYsz-tZ68ZRyJVW7VKC6Q_RaxAN5KkAEebMr0QKi38qBsPgMupdd9EYJJ3P2uxss7O56eYf-U3sXPYolrAmYHUdGHD20isFhET9B81PFfsm1c5dOjYu_MGGHg1w9zS7tz_Bl-KbVgIi4ihZMCQI2SrZ6z7LnMcKaHUhLK4reX2O2eS4053GMTHcirvg";
        OnItemUpdated = false;
        Debug.Log("Creating item");
        RestClient.DefaultRequestHeaders["content-type"] = "application/json";
        RequestHelper currentRequest;
        currentRequest = new RequestHelper
        {
            Uri = UriBuilder.GetItemUri(_ServerURL, item.name),
            Body = item,
            Retries = 5,
            RetrySecondsDelay = 1,
            RetryCallback = (err, retries) => {
                Debug.Log(string.Format("Retry #{0} Status {1}\nError: {2}", retries, err.StatusCode, err));
            }
        };

        RestClient.Put<string>(currentRequest, (err, res, body) => {
            if (err != null)
            {
                print("Error creating item on server");
                print(err.Message);
            }
            else
            {
                print("Success " + res.StatusCode + JsonUtility.ToJson(body, true));
            }
        });

    }

    /// <summary>
    /// Adds a location Item for current item on server
    /// </summary>
    public void CreateLocationItemOnServer()
    {
        List<string> asGroup = new List<string>();
        asGroup.Add(_ItemId);

        //List<string> tag = new List<string>();
        //asGroup.Add("Location");
        // TODO: get rid of Copypaste
        GroupItemDTO locationItem = new GroupItemDTO
        {
            type = "Number",
            name = _ItemId + "_X",
            label = _ItemId + " X",
            category = "Virtual Location",
            //tags = tag,
            groupNames = asGroup
        };
        CreateItemOnServer(locationItem);

        // TODO: get rid of Copypaste
        locationItem = new GroupItemDTO
        {
            type = "Number",
            name = _ItemId + "_Y",
            label = _ItemId + " Y",
            category = "Virtual Location",
            //tags = tag,
            groupNames = asGroup
        };
        CreateItemOnServer(locationItem);

        // TODO: get rid of Copypaste
        locationItem = new GroupItemDTO
        {
            type = "Number",
            name = _ItemId + "_Z",
            label = _ItemId + " Z",
            category = "Virtual Location",
            //tags = tag,
            groupNames = asGroup
        };
        CreateItemOnServer(locationItem);

    }


    public void GetGroupItems()
    {
        RestClient.Get(ClientConfig.getInstance()._ServerURL + "/rest/items/" + _ItemId).Then(res => {
            if (res.StatusCode >= 200 && res.StatusCode < 300)
            {
                EnrichedGroupItemDTO equipmentItems = JsonUtility.FromJson<EnrichedGroupItemDTO>(res.Text);

                Vector3 _shift = Vector3.zero;
                float delta = 0.5f;
                _shift.Set(_shift.x, _shift.y, _shift.z + delta);


                foreach (EnrichedItemDTO item in equipmentItems.members)
                {

                    if (ClientConfig.getInstance()._widgetPrefabs.ContainsKey(item.type))
                    {
                        if (item.category != "Virtual Location")
                        {
                            GameObject createdItem;
                            createdItem = Instantiate(ClientConfig.getInstance()._widgetPrefabs[item.type], this.transform.position + _shift, Quaternion.identity) as GameObject;

                            createdItem.GetComponent<ItemWidget>()._Item = item.name;
                            //if (_name == "YeelightColorLEDBulb") DontDestroyOnLoad(createdItem);
                            createdItem.transform.SetParent(this.gameObject.transform);
                            createdItem.name = item.name + "Widget";

                            GameObject itemToolTip;

                            itemToolTip = Instantiate(LoadPrefabFromFile("ToolTip"), this.transform.position + _shift, Quaternion.identity) as GameObject;
                            itemToolTip.GetComponent<ToolTip>().ToolTipText = item.name;
                            itemToolTip.GetComponent<ToolTipConnector>().Target = createdItem;
                            itemToolTip.transform.SetParent(createdItem.transform);

                            _shift.Set(_shift.x, _shift.y, _shift.z + delta);
                        }
                        else
                        {
                            GameObject createdItem;
                            createdItem = Instantiate(ClientConfig.getInstance()._widgetPrefabs["Virtual Location"], this.transform.position, Quaternion.identity) as GameObject;

                            createdItem.GetComponent<ItemWidget>()._Item = item.name;
                            //if (_name == "YeelightColorLEDBulb") DontDestroyOnLoad(createdItem);
                            createdItem.transform.SetParent(this.gameObject.transform);
                            createdItem.name = item.name + "Widget";
                        }
                    }
                    else
                    {
                        print(equipmentItems.name + " item " + item.name + " of type " + item.type + " has no predefined prefab for this type.");
                    }
                }

            }
            else
            {
                Debug.Log("Rest GET status for Item: " + " was not in OK span. (" + res.StatusCode + ")\n" + res.Error);
            }
        });
    }


    //Move it away from this class; make loading of prefabs in semanticmodelcontroller using this method by default
    private UnityEngine.Object LoadPrefabFromFile(string filename)
    {
        Debug.Log("Trying to load LevelPrefab from file (" + filename + ")...");
        var loadedObject = Resources.Load("Prefabs/" + filename);
        if (loadedObject == null)
        {
            throw new FileNotFoundException("...no file found - please check the configuration");
        }
        return loadedObject;
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
    /// Update text item on server
    /// </summary>
    /// <param name="value"></param>
    public void SetItemStateAsString(string value)
    {
        SetItemOnServer(value);
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