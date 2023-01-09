using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;



/// <summary>
/// Responsible for receiving and sending actions
/// to the item GameObject it is attached to
/// </summary>
public class ItemViewController : MonoBehaviour
{
    /// <summary>
    /// The ItemPresenter in the scene. Can be assigned during the runtime
    /// </summary>
    public ItemPresenter itemPresenter;

    /// <summary>
    /// The ID of the Item GameObject it is assigned to 
    /// </summary>
    public string itemID;

    /// <summary>
    /// Stores ActionData objects based on the Sender Method names.
    /// By invoking a sender method, the specified in ActionData Receiver Method is called
    /// </summary>
    public Dictionary <string, ActionData> senderMethods;

    /// <summary>
    /// Stores the names of the methods, which change the parameters of the connected GameObject.
    /// Can be called by Sender Methods of this or other GameObjects
    /// </summary>
    public List<string> receiverMethods;
   
    /// <summary>
    /// Each Item GameObject in the Scene has a transform component,
    /// which we can modify by calling these receiver methods
    /// </summary>
    void Start()
    {
        receiverMethods.Add(nameof(SetPosition));
        receiverMethods.Add(nameof(SetRotation));
        receiverMethods.Add(nameof(SetLocalScale));
    }


    public ItemViewController()
    {
        if (senderMethods == null) senderMethods = new Dictionary<string, ActionData>();
        if (receiverMethods == null) receiverMethods = new List<string>();

    }

    public Transform GetItemTransform()
    {
        return gameObject.transform;
    }

    public void SetItemTransform(Transform itemTransform)
    {
        SetPosition(itemTransform.position);
        SetRotation(itemTransform.rotation);
        SetLocalScale(itemTransform.localScale);
    }

    public void SetPosition(Vector3 position)
    {
        this.transform.position = position;
    }

    public void SetRotation(Quaternion rotation)
    {
        this.transform.rotation = rotation;
    }

    public void SetLocalScale(Vector3 localScale)
    {
        this.transform.localScale = localScale;
    }

    /// <summary>
    /// A function used to call the specified method with parameters
    /// </summary>
    /// <param name="method"></param>
    /// <param name="parameters"></param>
    public void CallMethod(string method, object[] parameters)
    {
        try
        {
            MethodInfo methodInfo = this.GetType().GetMethod(method);
            methodInfo.Invoke(this, parameters);
        }
        catch (Exception ex)
        {
            Debug.Log("Error: " + ex.Message);
        }
    }

    /// <summary>
    /// Call the receiver method of this or other item.
    /// The senderMethods Dictionary has to include the corresponding ActionData entry.  
    /// </summary>
    /// <param name="senderMethod">The name of the method used for the call</param>
    public void CallReceiverMethod(string senderMethod)
    {
        try
        {
            ActionData actionData = senderMethods[senderMethod];
            itemPresenter.GetItemViewController(actionData.receiverID).
                CallMethod(actionData.receiverMethod, actionData.receiverArgs.Cast<object>().ToArray());
        }
        catch (Exception ex)
        {
            Debug.Log("Error: " + ex.Message);
        }
    }

    /// <summary>
    /// Add a sender method entry (or update it with a new ActionData if exists)
    /// </summary>
    /// <param name="actionData">Can be empty or include the receiver method and id</param>
    public void CreateNewOrUpdateExistingSenderMethod(ActionData actionData)
    {
        // IMPORTANT: Explain why this check is needed/not needed
        // Because in the beginning an empty actionData may override 
        // the one loaded from serialization data
        if (actionData.actionID != null) 
            senderMethods[actionData.senderMethod] = actionData;
        else
        {
            if (!CheckIfSenderMethodExists(actionData))
                senderMethods[actionData.senderMethod] = actionData;
        }
    }

    private bool CheckIfSenderMethodExists(ActionData actionData)
    {
        return senderMethods.ContainsKey(actionData.senderMethod);
    }
}
