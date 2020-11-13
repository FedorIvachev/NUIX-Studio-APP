using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


// TODO: refactor code to get; set 
// Change to OnStateChange, so the Trigger / Untrigger functions work only once
namespace Tsinghua.HCI.IoTVRP
{
    public class WeightScalerItem : BasicSensorItem
    {
        [SerializeField] float _requiredWeight = 0.0f;
        private ArrayList _colliders = new ArrayList(); // A list of object currently colliding with the scaler

        // Start is called before the first frame update
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {
            float totalWeight = 0f;

            // add up the weight of all objects on the button
            for (int i = 0; i < _colliders.Count; i++)
            {
                Rigidbody rigidbody = (_colliders[i] as GameObject).GetComponent<Rigidbody>();

                if (rigidbody)
                    totalWeight += rigidbody.mass;
            }

            // press the switch if total weight meets requirement
            if (totalWeight > _requiredWeight)
            {
                SensorTrigger();
            } else
            {
                SensorUntrigger();
            }
        }

        void OnCollisionEnter(Collision col)
        {
            // keep track of all objects colliding with button
            _colliders.Add(col.gameObject);
        }

        void OnCollisionExit(Collision col)
        {
            // remove objects when no longer colliding with button
            _colliders.Remove(col.gameObject);
        }
    }
}