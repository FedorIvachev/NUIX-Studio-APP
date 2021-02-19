using System.Collections.Generic;
using UnityEngine;

public class ClientConfig
{
    public string _ServerURL = "http://localhost:8080"; //URL to server rest api ie. http://openhab:8080/rest

    // Links to the Item Widgets prefabs
    public Dictionary<string, GameObject> _widgetPrefabs;

    private static ClientConfig instance;
    public static ClientConfig getInstance()
    {
        if (instance == null)
            instance = new ClientConfig();
        return instance;
    }
}