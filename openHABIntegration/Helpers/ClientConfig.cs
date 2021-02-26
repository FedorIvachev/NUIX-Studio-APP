using System.Collections.Generic;
using UnityEngine;

public class ClientConfig
{
    [SerializeField]
    public string _ServerURL = "http://localhost:8080";
    public string _Username = "admin";
    public string _Password = "admin";

    // Links to the Item Widgets prefabs
    public Dictionary<string, GameObject> _widgetPrefabs = new Dictionary<string, GameObject>();


    // Links to the Category classes mprefabs
    public Dictionary<string, GameObject> _categoryPrefabs = new Dictionary<string, GameObject>();

    private static ClientConfig instance;
    public static ClientConfig getInstance()
    {
        if (instance == null)
            instance = new ClientConfig();
        return instance;
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