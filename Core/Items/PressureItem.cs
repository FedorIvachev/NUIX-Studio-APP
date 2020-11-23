using System.Collections;
using UnityEngine;

namespace Tsinghua.HCI.IoThingsLab
{
    /// <summary>
    /// Get the total mass of the colliding objects, trigger if it exceeds the required value
    /// </summary>
    public class PressureItem : SensorItem
    {
        GenericItem _weight;

        [SerializeField] float _requiredWeight = 0.0f;
        private ArrayList _colliders = new ArrayList(); // A list of object currently colliding with the scaler
        float _totalWeight;
        
        void Update()
        {
            _totalWeight = 0f;

            // add up the weight of all objects
            for (int i = 0; i < _colliders.Count; i++)
            {
                Rigidbody rigidbody = (_colliders[i] as GameObject).GetComponent<Rigidbody>();

                if (rigidbody)
                    _totalWeight += rigidbody.mass;
            }

            // press the switch if total weight meets requirement
            if (_totalWeight > _requiredWeight)
            {
                SensorTrigger();
            }
            else
            {
                SensorUntrigger();
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

        public void SerializeValue(string name = "")
        {
            base.SerializeValue(name + "_requiredWeightTrigger");
            _weight.name = name + "_totalWeight";
            _weight.state = _totalWeight.ToString();
            _weight.type = "Float";
        }
    }
}
