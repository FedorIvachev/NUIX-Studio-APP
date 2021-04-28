using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// TODO: FEDOR: connect to contact item


/// <summary>
/// A Widget which operates with two events:
/// Sensor trigger and untrigger
/// </summary>
public abstract class SensorWidget : ItemWidget
{
    [SerializeField]
    [Tooltip("Action performed after sensor is triggered")]
    private UnityEvent m_OnSensorTriggered;
    [SerializeField]
    [Tooltip("Action performed after sensor is triggered")]
    private UnityEvent m_OnSensorUntriggered;

    bool _isSensorTriggered = false;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        if (m_OnSensorTriggered == null)
            m_OnSensorTriggered = new UnityEvent();
        if (m_OnSensorUntriggered == null)
            m_OnSensorUntriggered = new UnityEvent();
    }

    void SensorTriggered()
    {
        _isSensorTriggered = true;
        m_OnSensorTriggered?.Invoke();
    }

    void SensorUntriggered()
    {
        _isSensorTriggered = false;
        m_OnSensorUntriggered?.Invoke();
    }

    public void SensorTrigger()
    {
        if (!_isSensorTriggered) SensorTriggered();
    }

    public void SensorUntrigger()
    {
        if (_isSensorTriggered) SensorUntriggered();
    }
}