using UnityEngine;

public class LightDimmerWidget : ItemWidget
{
    public Light _light;

    /// <summary>
    /// Initialize ItemController
    /// </summary>
    void Start()
    {

        ConnectedItemController.Initialize(item, _SubscriptionType);

        ConnectedItemController.updateItem += OnUpdate;
        InitWidget();
    }

    /// <summary>
    /// For public field initialization etc. This is to be able to use
    /// a generic start function for all widgets. This function is called for
    /// at end of Start()
    /// </summary>
    private void InitWidget()
    {
        if (_light == null) _light = GetComponent<Light>();
        ConnectedItemController.updateItem?.Invoke();
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
        float value = ConnectedItemController.GetItemStateAsDimmer();

        // Failed to parse the dimmer
        if (value == -1 || value > 100) _light.intensity = 0;
        else _light.intensity = value / 50f;
    }
}