using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Rewrite the code here
namespace Tsinghua.HCI.IoTVRP
{
    public class MotorItem : MonoBehaviour
    {
        [SerializeField] bool _moveToDestionation;
        [SerializeField] Vector3 _destinationPosition;
        Vector3 _startPosition;

        // Start is called before the first frame update
        void Start()
        {
            _startPosition = this.gameObject.transform.position;
        }

        // Update is called once per frame
        void Update()
        {
            this.gameObject.transform.position += (_destinationPosition - this.gameObject.transform.position).normalized / 100;
            if (Vector3.Distance(_destinationPosition, this.gameObject.transform.position) < 0.1f)
            {
                _destinationPosition = _startPosition;
            }
        }


    }
}
