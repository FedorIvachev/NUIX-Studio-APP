using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContactWidget : SensorWidget
{
    private Transform _transform;

    [SerializeField]
    [Tooltip("Connect a Gameobject transform to calculate distance to it")] Transform _connectedItemTransform;

    [SerializeField]
    [Tooltip("The available distance to the connected item. If the distance is higher than defined, the sensor triggers.")]
    private float _availableDistanceToConnectedItem = 0.0f;

    private float _calculatedDistanceToConnectedItem;
    private bool isInContact = true;


    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        InitWidget();
    }

    private void InitWidget()
    {
        if (_transform == null) _transform = GetComponent<Transform>();
        itemController.updateItem?.Invoke();
    }

    // Update is called once per frame
    void Update()
    {
        _transform.parent = _transform; // ???? maybe wrong
        CalculateDictanceToConnectedItem();
    }

    public void GetConnectedItemDistance()
    {
        _calculatedDistanceToConnectedItem = Vector3.Distance(_transform.position, _connectedItemTransform.position);
    }

    /// <summary>
    /// Calculate the distance to the connected item. If the distance is higher than defined, the sensor triggers.
    /// </summary>
    public void CalculateDictanceToConnectedItem()
    {
        GetConnectedItemDistance();
        if (_calculatedDistanceToConnectedItem >= _availableDistanceToConnectedItem)
        {
            SensorTrigger(); // Sensor triggers if the distance is too big
            isInContact = false;
        }
        else
        {
            SensorUntrigger(); // Otherwise untriggers
            isInContact = true;
        }
    }


    public override void OnUpdate()
    {
    }

    public void OnSetItem()
    {
        itemController.SetItemStateAsSwitch(isInContact);
    }
}
