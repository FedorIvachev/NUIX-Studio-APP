/// <summary>
/// Singleton to process location sync
/// </summary>
public class VirtualLocationController
{
    public delegate void OnLocationSync();
    public OnLocationSync locationSync;

    private static VirtualLocationController _virtualLocationControllerInstance;

    public static VirtualLocationController getInstance()
    {
        if (_virtualLocationControllerInstance == null)
            _virtualLocationControllerInstance = new VirtualLocationController();
        return _virtualLocationControllerInstance;
    }

    public void SyncVirtualLocation()
    {
        locationSync?.Invoke();
    }

}
