
using Microsoft.MixedReality.Toolkit.Utilities;
using UnityEngine;

#if OCULUSINTEGRATION_PRESENT
#endif

namespace Tsinghua.HCI.IoThingsLab
{
    /// <summary>
    /// One hand gesture basic class
    /// </summary>
    public abstract class Gesture : MonoBehaviour
    {
        public Handedness _handedness;

        
        [SerializeField] public bool isTrigger;
        [SerializeField] public SensorItem _trigger;
                
        public Gesture(Handedness handedness = Handedness.Right)
        {
            _handedness = handedness;
        }

        public abstract bool GestureCondition();

        public abstract bool TryGetGestureValue(out float value);

        public abstract void GestureEventTrigger();
    }
}