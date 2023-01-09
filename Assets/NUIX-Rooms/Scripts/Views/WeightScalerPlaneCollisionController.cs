using System.Collections;
using UnityEngine;

public class WeightScalerPlaneCollisionController : MonoBehaviour
{

    private ArrayList _colliders = new ArrayList(); // A list of objects currently colliding with the scaler

    public float totalWeight;

    private void Update()
    {
        totalWeight = 0f;
        for (int i = 0; i < _colliders.Count; i++)
        {
            Rigidbody rigidbody = (_colliders[i] as GameObject).GetComponent<Rigidbody>();

            if (rigidbody)
                totalWeight += rigidbody.mass;
        }
    }

    void OnCollisionEnter(Collision col)
    {
        _colliders.Add(col.gameObject);
    }

    void OnCollisionExit(Collision col)
    {
        _colliders.Remove(col.gameObject);
    }
}

