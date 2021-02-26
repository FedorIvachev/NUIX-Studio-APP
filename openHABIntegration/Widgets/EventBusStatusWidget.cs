using UnityEngine;
using UnityEngine.UI;

public class EventBusStatusWidget : MonoBehaviour
{
    public Color _DisconnectedColor;
    public Color _ConnectedColor;
    public Image _StatusImage;

    private EventController _Eventbus;

    /// <summary>
    /// Search for Eventbus gameobject and subscribe to events
    /// </summary>
    void Start()
    {
        _Eventbus = GameObject.FindGameObjectWithTag("Eventbus").GetComponent<EventController>();
        if (_Eventbus != null)
        {
            _Eventbus.connectedEventBus += ConnectionStatus;
        }
        else
        {
            _StatusImage.color = _DisconnectedColor;
        }
    }

    /// <summary>
    /// Change fill color based on connectionstatus
    /// </summary>
    /// <param name="eventbusConnection"></param>
    private void ConnectionStatus(bool eventbusConnection)
    {
        if (eventbusConnection)
        {
            _StatusImage.color = _ConnectedColor;
        }
        else
        {
            _StatusImage.color = _DisconnectedColor;
        }
    }

    /// <summary>
    /// Unsubscribe when disabled
    /// </summary>
    private void OnDisable()
    {
        if (_Eventbus != null) _Eventbus.connectedEventBus -= ConnectionStatus;
    }
}