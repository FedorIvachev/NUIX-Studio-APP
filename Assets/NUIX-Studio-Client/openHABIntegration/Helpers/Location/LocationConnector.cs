using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// A tiny class to implement fixed joint
/// </summary>
public class LocationConnector : MonoBehaviour
{
    public Transform Follower;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Follower != null) Follower.position = transform.position;
    }
}
