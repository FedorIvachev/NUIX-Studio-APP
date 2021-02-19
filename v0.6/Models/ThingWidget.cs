using System.Collections.Generic;
using UnityEngine;

public class ThingWidget : MonoBehaviour
{
    public string _name;

    private Dictionary<string, GameObject> _widgetPrefabs;


    private ThingController _thingController;

    public void Initialize(string name, Dictionary<string, GameObject> widgetPrefabs)
    {
        _name = name;
        _widgetPrefabs = widgetPrefabs;

        if (GetComponent<ThingController>() != null)
        {
            _thingController = GetComponent<ThingController>();
        }
        else
        {
            _thingController = gameObject.AddComponent<ThingController>();
        }

        _thingController.Initialize(_name, _widgetPrefabs);
    }


}
