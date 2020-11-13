using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.UI;
using Microsoft.MixedReality.Toolkit.Utilities;

namespace Tsinghua.HCI.IoTVRP
{
    public class ToolTipItem : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            gameObject.transform.localScale = new Vector3(0, 0, 0);
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void EnableToolTipVisibility()
        {
            gameObject.transform.localScale = new Vector3(1, 1, 1);
        }

        public void DisableToolTipVisibility()
        {
            gameObject.transform.localScale = new Vector3(0,0,0);
        }

    }
}
