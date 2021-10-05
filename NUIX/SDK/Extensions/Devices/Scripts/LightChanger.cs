using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class LightChanger : MonoBehaviour
{
    public Light _light;
    // Start is called before the first frame update
    void Start()
    {
        if (_light is null)
        {
            _light = GetComponent<Light>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void ChangeLightColor(string color)
    {
        switch(color)
        {
            case "Blue":
                _light.color = Color.blue;
                break;
            case "Red":
                _light.color = Color.red;
                break;
            default:
                _light.color = Color.white;
                break;
        }
    }
}
