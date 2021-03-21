using System.Collections.Generic;
using UnityEngine;

public class ClientConfig
{
    public string _ServerURL; // = "http://openhab:8080";//"http://192.168.3.3:8080"; // "https://ivaccchev%40gmail.com:TF85ygARC5K4@home.myopenhab.org"; // "http://localhost:8080";
    public string _Username = "admin";
    public string _Password = "password";

    // Links to the Item Widgets prefabs
    public Dictionary<string, GameObject> _widgetPrefabs = new Dictionary<string, GameObject>();

    private static ClientConfig _ClientConfigInstance;
    public static ClientConfig getInstance()
    {
        if (_ClientConfigInstance == null)
            _ClientConfigInstance = new ClientConfig();
        return _ClientConfigInstance;
    }

    /// <summary>
    /// Get the Basic auth string to connect to the server
    /// </summary>
    /// <returns></returns>
    public string Authenticate()
    {
        string auth = _Username + ":" + _Password;
        auth = System.Convert.ToBase64String(System.Text.Encoding.GetEncoding("ISO-8859-1").GetBytes(auth));
        auth = "Basic " + auth;
        return auth;
    }
}
