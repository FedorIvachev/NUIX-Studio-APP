using System.Collections.Generic;
using UnityEngine;

public class ClientConfig
{
    public string _ServerURL = "http://oh.Token.e8X5h5IrTU7wqXtNXvGzcaYbzVhIqVC8IO88yH5CToKzGtUfO6jIM0ldj6RrxQHO5TPnSEbkO5CM6uvJB5Ag:@localhost:8080"; //URL to server rest api ie. http://openhab:8080/rest

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
    public string authenticate(string username, string password)
    {
        string auth = username + ":" + password;
        auth = System.Convert.ToBase64String(System.Text.Encoding.GetEncoding("ISO-8859-1").GetBytes(auth));
        auth = "Basic " + auth;
        return auth;
    }

}