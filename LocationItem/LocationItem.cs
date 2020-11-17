using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


// TODO: refactor code to get; set 
// TODO: add UnityEvent that triggers at specific distance
namespace Tsinghua.HCI.IoTVRP
{
    public class LocationItem : BasicSensorItem
    {
        private Transform _transform;
        [SerializeField] Transform _connectedItemTransform;
        [SerializeField]
        [Tooltip("The available distance to the connected item. If the distance is higher than defined, the sensor triggers.")]
        private float _availableDistanceToConnectedItem = 0.0f;
        

        // Start is called before the first frame update
        void Start()
        {
            _transform = GetComponent<Transform>();
        }

        // Update is called once per frame
        void Update()
        {
            _transform.parent = _transform; // ???? maybe wrong
            CalculateDictanceToConnectedItem();
        }
        
        public Transform GetTransform()
        {
            return _transform;
        }

        public Vector3 GetLocation()
        {
            return _transform.position;
        }

        public void SetConnectedItemTransform(Transform newConnectedItemTransform)
        {
            _connectedItemTransform = newConnectedItemTransform;
        }

        public float ConnectedItemDistance()
        {
            return Vector3.Distance(_transform.position, _connectedItemTransform.position);
        }

        public void CalculateDictanceToConnectedItem()
        {
            if (ConnectedItemDistance() >= _availableDistanceToConnectedItem)
            {
                SensorTrigger(); // Sensor triggers if the distance is too big
            }
            else
            {
                SensorUntrigger(); // Otherwise untriggers
            }
        }
        
    }
}
