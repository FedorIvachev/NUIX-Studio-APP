using UnityEngine;
using EvtSource;
using System;
using System.Threading.Tasks;

class ThingController : MonoBehaviour
{
    [SerializeField]
    private bool _isConnected = false;
    public string _serverUri = "http://localhost:8080";

    void Start()
    {
    }

}
