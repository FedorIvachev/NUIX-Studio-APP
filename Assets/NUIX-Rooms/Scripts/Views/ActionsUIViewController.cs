using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ActionsUIViewController : MonoBehaviour
{
    public ItemPresenter itemPresenter;

    private ItemViewController currentSenderViewController;
    private int senderViewControllerIndex = 0;
    private ItemViewController currentReceiverViewController;
    private int receiverViewControllerIndex = 0;

    private string currentSenderMethod;
    private int currentSenderMethodIndex = 0;
    private string currentReceiverMethod;
    private int currentReceiverMethodIndex = 0;



    public TMPro.TMP_Text senderIDLabel;
    public TMPro.TMP_Text senderItemTypeLabel;
    public TMPro.TMP_Text senderMethodNameLabel;
    
    public TMPro.TMP_Text receiverIDLabel;
    public TMPro.TMP_Text receiverItemTypeLabel;
    public TMPro.TMP_Text receiverMethodNameLabel;


    public void NextSenderMethod()
    {
        try
        {
            int senderMethodsCount = currentSenderViewController.senderMethods.Count;
            if (senderMethodsCount == 0) return;
            currentSenderMethodIndex++;
            if (currentSenderMethodIndex >= senderMethodsCount)
            {
                currentSenderMethodIndex = 0;
            }
            currentSenderMethod = currentSenderViewController.senderMethods.ElementAt(currentSenderMethodIndex).Key;
            UpdateView();
        }
        catch (Exception ex)
        {
            Debug.Log("Error: " + ex.Message);
        }
    }

    public void PreviousSenderMethod()
    {
        try
        {
            int senderMethodsCount = currentSenderViewController.senderMethods.Count;
            if (senderMethodsCount == 0) return;
            currentSenderMethodIndex--;
            if (currentSenderMethodIndex <= -1)
            {
                currentSenderMethodIndex = senderMethodsCount - 1;
            }
            currentSenderMethod = currentSenderViewController.senderMethods.ElementAt(currentSenderMethodIndex).Key;
            UpdateView();
        }
        catch (Exception ex)
        {
            Debug.Log("Error: " + ex.Message);
        }
    }

    public void NextReceiverMethod()
    {
        try
        {
            int receiverMethodsCount = currentReceiverViewController.receiverMethods.Count;
            if (receiverMethodsCount == 0) return;
            currentReceiverMethodIndex++;
            if (currentReceiverMethodIndex >= receiverMethodsCount)
            {
                currentReceiverMethodIndex = 0;
            }
            currentReceiverMethod = currentReceiverViewController.receiverMethods.ElementAt(currentReceiverMethodIndex);
            UpdateView();
        }
        catch (Exception ex)
        {
            Debug.Log("Error: " + ex.Message);
        }
    }
    public void PreviousReceiverMethod()
    {
        try
        {
            int receiverMethodsCount = currentReceiverViewController.receiverMethods.Count;
            if (receiverMethodsCount == 0) return;
            currentReceiverMethodIndex--;
            if (currentReceiverMethodIndex <= -1)
            {
                currentReceiverMethodIndex = receiverMethodsCount -1;
            }
            currentReceiverMethod = currentReceiverViewController.receiverMethods.ElementAt(currentReceiverMethodIndex);
            UpdateView();
        }
        catch (Exception ex)
        {
            Debug.Log("Error: " + ex.Message);
        }
    }

    public void NextSenderViewController()
    {
        try
        {
            int viewControllersCount = itemPresenter.itemViewControllers.Count;
            if (viewControllersCount == 0) return;
            senderViewControllerIndex++;
            if (senderViewControllerIndex >= viewControllersCount)
            {
                senderViewControllerIndex = 0;
            }
            currentSenderViewController = itemPresenter.itemViewControllers.ElementAt(senderViewControllerIndex).Value;
            UpdateView();
        }
        catch (Exception ex)
        {
            Debug.Log("Error: " + ex.Message);
        }
    }

    public void PreviousSenderViewController()
    {
        try
        {
            int viewControllersCount = itemPresenter.itemViewControllers.Count;
            if (viewControllersCount == 0) return;
            senderViewControllerIndex--;
            if (senderViewControllerIndex <= -1)
            {
                senderViewControllerIndex = viewControllersCount - 1;
            }
            currentSenderViewController = itemPresenter.itemViewControllers.ElementAt(senderViewControllerIndex).Value;
            UpdateView();
        }
        catch (Exception ex)
        {
            Debug.Log("Error: " + ex.Message);
        }
    }

    public void NextReceiverViewController()
    {
        try
        {
            int viewControllersCount = itemPresenter.itemViewControllers.Count;
            if (viewControllersCount == 0) return;
            receiverViewControllerIndex++;
            if (receiverViewControllerIndex >= viewControllersCount)
            {
                receiverViewControllerIndex = 0;
            }
            currentReceiverViewController = itemPresenter.itemViewControllers.ElementAt(receiverViewControllerIndex).Value;
            UpdateView();
        }
        catch (Exception ex)
        {
            Debug.Log("Error: " + ex.Message);
        }
    }
    public void PreviousReceiverViewController()
    {
        try
        {
            int viewControllersCount = itemPresenter.itemViewControllers.Count;
            if (viewControllersCount == 0) return;
            receiverViewControllerIndex--;
            if (receiverViewControllerIndex <= -1)
            {
                receiverViewControllerIndex = viewControllersCount;
            }
            currentReceiverViewController = itemPresenter.itemViewControllers.ElementAt(receiverViewControllerIndex).Value;
            UpdateView();
        }
        catch (Exception ex)
        {
            Debug.Log("Error: " + ex.Message);
        }
    }

    private void UpdateView()
    {
        if (currentSenderViewController != null)
        {
            senderIDLabel.text = currentSenderViewController.itemID;
            senderItemTypeLabel.text = itemPresenter.GetItemDatabyID(currentSenderViewController.itemID).itemType.ToString();
        }
        if (currentReceiverViewController != null)
        {
            receiverIDLabel.text = currentReceiverViewController.itemID;
            receiverItemTypeLabel.text = itemPresenter.GetItemDatabyID(currentReceiverViewController.itemID).itemType.ToString();
        }

        // Update methods based on viewcontrollers
        if (currentSenderMethod != "")
        {
            senderMethodNameLabel.text = currentSenderMethod;
        }
        if (currentReceiverMethod != "")
        {
            receiverMethodNameLabel.text = currentReceiverMethod;
        }
    }

    public void AddAction()
    {
        try
        {
            ActionData actionData = new ActionData(Guid.NewGuid().ToString(),
                currentSenderViewController.itemID, currentSenderMethod, new List<string>(),
                currentReceiverViewController.itemID, currentReceiverMethod, new List<string>());
            itemPresenter.AddActionData(actionData);
        }
        catch (Exception ex)
        {
            Debug.Log("Error: " + ex.Message);
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
