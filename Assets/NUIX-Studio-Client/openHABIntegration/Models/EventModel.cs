using Proyecto26;
using UnityEngine;

public class Payload
{
    public string type;
    public string value;
    public string oldType;
    public string oldValue;
    public string status;
    public string statusDetail;
}

public class EventModel
{
    public string itemId;
    public string topic;
    public string type;
    public string payload;
    public Payload _Payload;
    public EvtType _eventType;

    /// <summary>
    /// Parse the event payload and that as Payload object
    /// instead of a jsonstring
    /// </summary>
    public void Parse()
    {
        _Payload = JsonUtility.FromJson<Payload>(payload);

        _eventType = type switch
        {
            "ItemAddedEvent" => EvtType.ItemAddedEvent,
            "ItemRemoved" => EvtType.ItemRemovedEvent,
            "ItemUpdateEvent" => EvtType.ItemUpdatedEvent,
            "ItemCommandEvent" => EvtType.ItemCommandEvent,
            "ItemStateEvent" => EvtType.ItemStateEvent,
            "ItemStateChangedEvent" => EvtType.ItemStateChangedEvent,
            _ => EvtType.None,
        };
        /**
        ItemAddedEvent          /added
        ItemRemovedEvent        /removed
        ItemUpdatedEvent        /updated
        ItemCommandEvent        /command
        ItemStateEvent          /state
        ItemStateChangedEvent   /statechanged   (This is 99% of times what we want)
        **/

        // smarthome/items/Power_gfRgbLedShelf/statechanged
        string[] parseTopic = topic.Split('/');
        if (parseTopic.Length == 4)
        {
            itemId = parseTopic[2];
        }
    }

    /// <summary>
    /// Dump this object to string for debugging. Library not included in Project
    /// </summary>
    /// <returns>Object as string</returns>
    /**
    public override string ToString()
    {
        return Dumper.Dump(this);
    }
    */
}

