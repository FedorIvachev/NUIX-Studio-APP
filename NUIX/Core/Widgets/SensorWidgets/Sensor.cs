using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


/// <summary>
/// A Widget which operates with two events:
/// Sensor trigger and untrigger
/// </summary>
public abstract class Sensor : MonoBehaviour
{
    [Header("Virtual Sensor Setup")]
    [SerializeField]
    [Tooltip("Action performed after sensor is triggered")]
    private UnityEvent m_OnSensorTriggered;
    [SerializeField]
    [Tooltip("Action performed after sensor is triggered")]
    private UnityEvent m_OnSensorUntriggered;

    bool _isSensorTriggered = false;

    protected string SensorType = "Sensor";

    // Start is called before the first frame update
    public virtual void Start()
    {
        if (m_OnSensorTriggered == null)
            m_OnSensorTriggered = new UnityEvent();
        if (m_OnSensorUntriggered == null)
            m_OnSensorUntriggered = new UnityEvent();
    }

    public virtual void Update() { }

    void SensorTriggered()
    {
        _isSensorTriggered = true;
        Debug.Log(SensorType + " triggered");
        m_OnSensorTriggered?.Invoke();
    }

    void SensorUntriggered()
    {
        _isSensorTriggered = false;
        Debug.Log(SensorType + " untriggered");
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