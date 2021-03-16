using System.Collections.Generic;
using UnityEngine;
using EvtSource;
using System;
using System.Threading.Tasks;

class EventController : MonoBehaviour
{

    [SerializeField]
    private bool _isConnected = false;
    public List<GameObject> _subscribers;
    public Dictionary<string, Item> _subscribersFixed; // = new Dictionary<string, Item>();


    private EventSourceReader _evt;


    public delegate void OnConnectedEventBus(bool eventbusConnection);
    public OnConnectedEventBus connectedEventBus;

    void Start()
    {
        //StartListen();
    }

    /// <summary>
    /// Starts the Event Listener
    /// </summary>
    public void StartListen()
    {
        _evt = new EventSourceReader(UriBuilder.GetEventStreamUri(ClientConfig.getInstance()._ServerURL)).Start();
        _evt.MessageReceived += (object sender, EventSourceMessageEventArgs e) => NewEvent(e);
        _evt.Disconnected += async (object sender, DisconnectEventArgs err) => {
            _isConnected = false;
            connectedEventBus?.Invoke(false); // Lost the eventbus, handle this with reconnection after 3 sec.
            Console.WriteLine($"Retry in: {err.ReconnectDelay} - Error: {err.Exception}");
            await Task.Delay(err.ReconnectDelay);
            _evt.Start(); // Reconnect to the same URL
        };
    }

    /// <summary>
    /// Parse a new event and trigger
    /// </summary>
    /// <param name="sender">The eventsourcereader</param>
    /// <param name="e">event of type EvenSourceMessageEventArgs</param>
    private void NewEvent(EventSourceMessageEventArgs e)
    {
        if (!_isConnected)
        {
            _isConnected = true;
            connectedEventBus?.Invoke(true);
            Debug.Log("Connected to OpenHab Eventbus again.");
        }
        EventModel ev = JsonUtility.FromJson<EventModel>(e.Message);
        ev.Parse();
        Debug.Log("NewEvent!!!\nParsed new Object:\n" + ev.ToString());


        if (SemanticModel.getInstance()._items.ContainsKey(ev.itemId))
        {
            foreach (GameObject itemWidget in SemanticModel.getInstance()._items[ev.itemId]._itemWidgets)
            {
                if (itemWidget.GetComponent<ItemController>().GetItemSubType() == ev._eventType)
                {
                    itemWidget.GetComponent<ItemController>().ReceivedEvent(ev);
                }
            }
        }

        // This sends the event to all items.
        // It would be better if event is sent to specific item.
        //newEvent?.Invoke(ev);




    }


    /// <summary>
    /// Dispose EvtSource if disabling Eventbus gameobject
    /// </summary>
    private void OnDestroy()
    {
        //_evt.Dispose();
    }


}
