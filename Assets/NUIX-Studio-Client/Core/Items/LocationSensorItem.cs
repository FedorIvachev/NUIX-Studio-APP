using UnityEngine;

namespace Tsinghua.HCI.IoThingsLab
{
    /// <summary>
    /// Provides a sensor trigger functionality 
    /// based on the distance to the connected Gameobject
    /// </summary>
    public class LocationSensorItem : SensorItem
    {
        private Transform _transform;
        [SerializeField]
        [Tooltip("Connect a Gameobject transform to calculate distance to it")] Transform _connectedItemTransform;
        [SerializeField]
        [Tooltip("The available distance to the connected item. If the distance is higher than defined, the sensor triggers.")]
        private float _availableDistanceToConnectedItem = 0.0f;
        private float _calculatedDistanceToConnectedItem;

        GenericItem _distanceToConnectedItem;


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

        public void GetConnectedItemDistance()
        {
            _calculatedDistanceToConnectedItem = Vector3.Distance(_transform.position, _connectedItemTransform.position);
        }

        /// <summary>
        /// Calculate the distance to the connected item. If the distance is higher than defined, the sensor triggers.
        /// </summary>
        public void CalculateDictanceToConnectedItem()
        {
            GetConnectedItemDistance();
            if (_calculatedDistanceToConnectedItem >= _availableDistanceToConnectedItem)
            {
                SensorTrigger(); // Sensor triggers if the distance is too big
            }
            else
            {
                SensorUntrigger(); // Otherwise untriggers
            }
        }

        public void SerializeValue(string name = "")
        {
            base.SerializeValue(name + "_availableDistanceToConnectedItemTrigger");
            _distanceToConnectedItem.name = name + "_distanceToConnectedItem";
            _distanceToConnectedItem.state = _calculatedDistanceToConnectedItem.ToString();
            _distanceToConnectedItem.type = "Float";
        }
    }
}
