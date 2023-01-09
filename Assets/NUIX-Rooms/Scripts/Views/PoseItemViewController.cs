using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Oculus.Interaction;
using Oculus.Interaction.Input;
using UnityEngine.Assertions;

/// <summary>
/// Attached to a PoseItemDescription, adding extra actions
/// </summary>
public class PoseItemViewController : ItemViewController
{
    /// <summary>
    /// Set poses from Oculus Integration package to be recognized
    /// </summary>
    [SerializeField] private ActiveStateSelector[] poses;

    /// <summary>
    /// Let each of the recognized poses to call a connected sender method
    /// </summary>
    void Start()
    {
        transform.Find("HandRefLeft").gameObject.GetComponent<HandRef>().InjectHand(GameObject.Find("NUIXHandRefLeft").GetComponent<HandRef>().Hand);
        transform.Find("HandRefRight").gameObject.GetComponent<HandRef>().InjectHand(GameObject.Find("NUIXHandRefRight").GetComponent<HandRef>().Hand);
        for (int i = 0; i < poses.Length; i++)
        {
            int poseNumber = i;
            //_poses[i].WhenSelected += () => ShowVisuals(poseNumber);
            //_poses[i].WhenUnselected += () => HideVisuals(poseNumber);

            poses[i].WhenSelected += () => CallReceiverMethod(poses[poseNumber].gameObject.name);
            CreateNewOrUpdateExistingSenderMethod(new ActionData(itemID, poses[poseNumber].gameObject.name));
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
