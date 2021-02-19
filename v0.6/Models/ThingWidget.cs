using System.Collections.Generic;
using UnityEngine;

public class ThingWidget : MonoBehaviour
{
    public string _name;

    private ThingController _thingController;

    public void Initialize(string name)
    {
        _name = name;

        if (GetComponent<ThingController>() != null)
        {
            _thingController = GetComponent<ThingController>();
        }
        else
        {
            _thingController = gameObject.AddComponent<ThingController>();
        }

        _thingController.Initialize(_name);
    }


}
