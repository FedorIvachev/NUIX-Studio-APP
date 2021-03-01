using UnityEngine;

public class VirtualItemsController : MonoBehaviour
{
    public void SyncVirtualItems()
    {
        VirtualLocationController.getInstance().SyncVirtualLocation();
    }
}