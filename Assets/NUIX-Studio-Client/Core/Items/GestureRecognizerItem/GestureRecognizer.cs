using UnityEngine;

#if OCULUSINTEGRATION_PRESENT
#endif

namespace Tsinghua.HCI.IoThingsLab
{
    /// <summary>
    /// Performs checking of each connected gesture condition to trigger the associated sensors.
    /// </summary>
    public class GestureRecognizer : MonoBehaviour
    {
        [SerializeField] public Gesture[] SelectedGestures;


        public void Update()
        {
            foreach (Gesture gesture in SelectedGestures)
            {
                if (gesture.isTrigger) gesture.GestureEventTrigger();
            }
        }

    }
}