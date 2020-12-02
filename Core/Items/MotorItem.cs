using UnityEngine;

namespace Tsinghua.HCI.IoThingsLab
{
    /// <summary>
    /// Provides functions to move the GameObject to the defined destination
    /// </summary>
    public class MotorItem : MonoBehaviour
    {

        [SerializeField]
        [Tooltip("Whether to move to destination")] bool _moveToDestination = false;
        [SerializeField]
        [Tooltip("Destination Position in world coordinates")] Vector3 _destinationPosition;

        // Update is called once per frame
        void Update()
        {
            if (_moveToDestination) MoveToDestination();
        }

        /// <summary>
        /// Move to the destination position, then stop
        /// </summary>
        private void MoveToDestination()
        {
            this.gameObject.transform.position += (_destinationPosition - this.gameObject.transform.position).normalized / 100;
            if (Vector3.Distance(_destinationPosition, this.gameObject.transform.position) < 0.1f)
            {
                _moveToDestination = false;
            }
        }

        /// <summary>
        /// Change the state Moving/Not moving
        /// </summary>
        public void Toggle()
        {
            _moveToDestination = !_moveToDestination;
        }
    }
}
