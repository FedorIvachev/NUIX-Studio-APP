/// <summary>
/// Tiny helper for cleaning up the controller classes when
/// doing rest calls etc. Various functions for creating
/// URI for the calls.
/// </summary>
public class UriBuilder
{

    /// <summary>
    /// Uri for Rest GET item from server
    /// If you want to get fancy. Make some uri sanity checks
    /// etc here.
    /// </summary>
    /// <param name="server">server adress including port</param>
    /// <param name="itemId">the item we want to call</param>
    /// <returns>string with uri for GET call</returns>
    public static string GetItemUri(string server, string itemId)
    {
        // ie. http://openhab:8080/rest/items/gf_hallway_temperature
        string uri = "";
        uri += server;
        uri += "/rest/items/";
        uri += itemId;
        //UnityEngine.Debug.Log("UriBuilder: " + uri);
        return uri;
    }

    public static System.Uri GetEventStreamUri(string server)
    {
        return new System.Uri(server + "/rest/events?topic=smarthome/items/");
    }
}
