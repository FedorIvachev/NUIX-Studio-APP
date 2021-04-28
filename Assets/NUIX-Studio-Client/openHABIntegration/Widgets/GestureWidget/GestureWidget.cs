using Microsoft.MixedReality.Toolkit.Utilities;
using UnityEngine;

#if OCULUSINTEGRATION_PRESENT
#endif
/// <summary>
/// One hand gesture basic class
/// </summary>
public abstract class GestureWidget : ItemWidget
{
    public Handedness _handedness;

    [SerializeField] public bool isTrigger;
    [SerializeField] public SensorWidget _trigger;

    public GestureWidget(Handedness handedness = Handedness.Right)
    {
        _handedness = handedness;
    }

    public abstract bool GestureCondition();

    public abstract bool TryGetGestureValue(out float value);

    public abstract void GestureEventTrigger();

    public override void Start()
    {
        base.Start();
    }
}
