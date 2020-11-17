using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Tsinghua.HCI.IoTVRP
{
    public class IndicatorItem : MonoBehaviour
    {
        [SerializeField] bool _isIndicated = false;
        [SerializeField] Material baseMaterial;
        [SerializeField] Material indicateMaterial;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (_isIndicated) IndicateByChangingMaterial(indicateMaterial);
            else IndicateByChangingMaterial(baseMaterial);
            //DeIndicate();
        }

        public void IndicateByChangingMaterial(Material newMaterial)
        {
            GetComponent<MeshRenderer>().material = newMaterial;
            //print("Indicated");
        }

        public void Indicate()
        {
            _isIndicated = true;
        }

        public void DeIndicate()
        {
            _isIndicated = false;
        }
    }
}
