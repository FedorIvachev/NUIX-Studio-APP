using UnityEngine;


/// <summary>
/// Attached to a ButtonDescriptionItem, adding extra actions
/// </summary>
public class ButtonItemViewController: ItemViewController
{ 
    ButtonItemViewController()
    {
        //senderMethods.Add(nameof(Press), null);
    }

    public void Start()
    {
        CreateNewOrUpdateExistingSenderMethod(new ActionData(itemID, nameof(Press)));
    }

    /// <summary>
    /// A sender method to be called when the button is pressed.
    /// Add it to OnClick component
    /// </summary>
    public void Press()
    {
        CallReceiverMethod(nameof(Press));
        Debug.Log(itemID + " " + nameof(Press));
    }
}
