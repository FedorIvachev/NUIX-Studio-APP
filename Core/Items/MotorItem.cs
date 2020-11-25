using UnityEngine;

namespace Tsinghua.HCI.IoThingsLab
{
    // Need to change into sensor item
    public class MotorItem : MonoBehaviour
    {
        [SerializeField] bool _moveToDestination = false;
        [SerializeField] Vector3 _destinationPosition;

        // Update is called once per frame
        void Update()
        {
            if (_moveToDestination) MoveToDestination();
        }

        private void MoveToDestination()
        {
            this.gameObject.transform.position += (_destinationPosition - this.gameObject.transform.position).normalized / 100;
            if (Vector3.Distance(_destinationPosition, this.gameObject.transform.position) < 0.1f)
            {
                _moveToDestination = false;
            }
        }

        public void Toggle()
        {
            _moveToDestination = !_moveToDestination;
        }
    }
}
