using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ItemInit : MonoBehaviour
{
    [SerializeField] public ItemDescription itemDescription;
    [SerializeField] public Vector3 SpawnLocation;
    [SerializeField] public Vector3 SpawnRotation;
}

public class Room1Init : MonoBehaviour
{

    [SerializeField] public int[] itemsInitializedAtStart;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
