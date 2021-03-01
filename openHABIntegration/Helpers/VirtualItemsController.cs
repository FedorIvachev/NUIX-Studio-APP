using UnityEngine;

/// <summary>
/// Help class to process Location and other controllers methods
/// </summary>
public class VirtualItemsController : MonoBehaviour
{
    public void SyncVirtualItems()
    {
        VirtualLocationController.getInstance().SyncVirtualLocation();
    }
}