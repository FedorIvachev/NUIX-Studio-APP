using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Provides functionality for triggering the sensor
/// when the connected item is inside the defined camera view
/// </summary>
public class SightSensor : Sensor
{
    [SerializeField]
    [Tooltip("The tracked object")] Transform _target;

    [SerializeField]
    [Tooltip("Should be either Character camera or any other camera in the scene. Main camera by default.")]
    Camera _camera;

    public override void Start()
    {
        base.Start();
        InitWidget();
    }

    private void InitWidget()
    {
        if (_target == null) _target = gameObject.transform;
        if (_camera == null) _camera = Camera.main;
        SensorType = this.GetType().Name;
        Debug.Log(SensorType + " initialized");
    }

    public override void Update()
    {
        base.Update();
        CheckForTargetInCameraView();
    }

    /// <summary>
    /// When object is in front of camera, trigger the sensor
    /// </summary>
    public void CheckForTargetInCameraView()
    {
        Vector3 viewPos = _camera.WorldToViewportPoint(_target.position);
        // Checking if the target object is inside the defined camera view
        if ((viewPos.z > 0.0F) && (viewPos.x < 1.0F) && (viewPos.x > 0.0F))
        {
            SensorTrigger();
        }
        else
        {
            SensorUntrigger();
        }
    }

    public Vector3 GetViewportLocation()
    {
        return _camera.WorldToViewportPoint(_target.position);
    }
}
