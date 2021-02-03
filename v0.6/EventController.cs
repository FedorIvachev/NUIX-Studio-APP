using System.Collections.Generic;
using UnityEngine;
using EvtSource;
using System;
using System.Threading.Tasks;

class EventController : MonoBehaviour
{
    [SerializeField]
    private bool _isConnected = false;
    public string _serverUri = "http://localhost:8080";
    public List<GameObject> _subscribers;

    private EventSourceReader _evt;


    public delegate void OnConnectedEventBus(bool eventbusConnection);
    public OnConnectedEventBus connectedEventBus;

    void Start()
    {
        StartListen();
    }

    /// <summary>
    /// Subscribe to topic
    /// </summary>
    /// <param name="item">the item to subscribe</param>
    public void Subscribe(GameObject item)
    {
        Debug.Log("Subscribe to topic " + item.GetComponent<ItemController>().GetItemID() + " for " + item.GetComponent<ItemController>().GetItemSubType().ToString() + " events.");
        if (_subscribers != null)
        {
            _subscribers.Add(item);
        }
        else
        {
            _subscribers = new List<GameObject>();
            _subscribers.Add(item);
        }

    }

    /// <summary>
    /// Unsubscribe to topic
    /// </summary>
    /// <param name="item">the item to unsubscribe</param>
    public void Unsubscribe(GameObject item)
    {
        _subscribers.Remove(item);
    }

    /// <summary>
    /// Starts the Event Listener
    /// </summary>
    public void StartListen()
    {
        _evt = new EventSourceReader(UriBuilder.GetEventStreamUri(_serverUri)).Start();
        _evt.MessageReceived += (object sender, EventSourceMessageEventArgs e) => NewEvent(e);
        _evt.Disconnected += async (object sender, DisconnectEventArgs err) => {
            _isConnected = false;
            connectedEventBus?.Invoke(false); // Lost the eventbus, handle thiswith reconnection after 3 secs.
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
        //Debug.Log("NewEvent!!!\nParsed new Object:\n" + ev.ToString());

        // New revision, send event to specific item, not a fun of iterating through lists but...
        foreach (GameObject item in _subscribers)
        {
            ItemController ic = item.GetComponent<ItemController>();
            if (ic.GetItemID() == ev.itemId && ic.GetItemSubType() == ev._eventType) ic.ReceivedEvent(ev);
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
