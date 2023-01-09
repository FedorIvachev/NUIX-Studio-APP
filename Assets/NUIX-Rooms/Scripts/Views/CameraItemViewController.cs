using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraItemViewController : ItemViewController
{
    [SerializeField]
    [Tooltip("The tracked object, main camera by default")] Transform _target;

    [SerializeField]
    [Tooltip("Should be either Character camera or any other camera in the scene.")]
    Camera _camera;

    private bool isTriggered = false;

    public void Start()
    {
        if (_target == null) _target = Camera.main.transform;
        CreateNewOrUpdateExistingSenderMethod(new ActionData(itemID, nameof(TrackedObjectInsideCameraView)));
    }

    public void Update()
    {
        CheckForTargetInCameraView();
    }


    public void TrackedObjectInsideCameraView()
    {
        Debug.Log($"The target {_target.name} is inside the {_camera.name} view");
        CallReceiverMethod(nameof(TrackedObjectInsideCameraView));
    }
    public void TrackedObjectOutsideCameraView()
    {
        Debug.Log($"The target {_target.name} is outside the {_camera.name} view");
        CallReceiverMethod(nameof(TrackedObjectOutsideCameraView));
    }

    public void CheckForTargetInCameraView()
    {
        Vector3 viewPos = _camera.WorldToViewportPoint(_target.position);
        // Checking if the target object is inside the defined camera view
        if ((viewPos.z > 0.0F) && (viewPos.x < 1.0F) && (viewPos.x > 0.0F))
        {
            if (!isTriggered) TrackedObjectInsideCameraView();
            isTriggered = true;
        }
        else
        {
            if (isTriggered) TrackedObjectOutsideCameraView();
            isTriggered = false;
        }
    }

}
