using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Attached to a LightDescriptionItem, adding extra actions
/// </summary>
public class LightItemViewController : ItemViewController
{

    private List<Color> colors = new List<Color> { Color.white, Color.green, Color.red, Color.cyan };
    public int colorIndex = 0;

    public void Start()
    {
        receiverMethods.Add(nameof(Toggle));
        receiverMethods.Add(nameof(ChangeNextColor));
        receiverMethods.Add(nameof(ChangePreviousColor));
    }

    /// <summary>
    /// A Light component to interact with
    /// </summary>
    public Light itemLight;


    /// <summary>
    /// Receiver action to enable/disable light
    /// </summary>
    public void Toggle()
    {
        itemLight.enabled = !itemLight.enabled;
    }

    public void SetColor(int colorIndex)
    {
        itemLight.color = colors[colorIndex];
    }

    public void ChangeNextColor()
    {
        colorIndex++;
        if (colorIndex >= colors.Count) colorIndex = 0;
        SetColor(colorIndex);
    }
    public void ChangePreviousColor()
    {
        colorIndex--;
        if (colorIndex < 0) colorIndex = colors.Count - 1;
        SetColor(colorIndex);
    }
}
