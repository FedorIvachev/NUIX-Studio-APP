using System;
using System.Collections.Generic;
using UnityEngine;

public class WeightScalerItemViewController : ItemViewController
{

    public TMPro.TMP_Text currentWeightLabel;
    public TMPro.TMP_Text requiredWeightLabel;

    public float requiredWeight = 1.0f;
    public WeightScalerPlaneCollisionController weightScalerPlaneCollisionController;

    private bool isTriggered = false;
    // Start is called before the first frame update
    void Start()
    {
        CreateNewOrUpdateExistingSenderMethod(new ActionData(itemID, nameof(WeightTrigger)));
        CreateNewOrUpdateExistingSenderMethod(new ActionData(itemID, nameof(WeightUnTrigger)));
    }
    
    /// <summary>
    /// A method to be called when the total weight on the weighscaler exceeds the value set
    /// </summary>
    public void WeightTrigger()
    {
        Debug.Log($"Total weight {weightScalerPlaneCollisionController.totalWeight} exceeded the weight set {requiredWeight}");
        CallReceiverMethod(nameof(WeightTrigger));
    }

    public void WeightUnTrigger()
    {
        Debug.Log($"Total weight {weightScalerPlaneCollisionController.totalWeight} is now less than the weight set {requiredWeight}");
        CallReceiverMethod(nameof(WeightUnTrigger));
    }

    public void IncreaseRequiredWeight()
    {
        requiredWeight += 1;
    }

    public void DecreaseRequiredWeight()
    {
        requiredWeight -= 1;
    }

    private void UpdateTextLabels()
    {
        currentWeightLabel.text = "Current Weight:" + Environment.NewLine + weightScalerPlaneCollisionController.totalWeight.ToString();
        requiredWeightLabel.text = "Trigger Weight:" + Environment.NewLine + requiredWeight.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        float currentWeight = weightScalerPlaneCollisionController.totalWeight;

        if (currentWeight > requiredWeight)
        {
            if (!isTriggered) WeightTrigger();
            isTriggered = true;
        }
        else
        {
            if (isTriggered) WeightUnTrigger();
            isTriggered = false;
        }
        UpdateTextLabels();
    }
    public void SetRequiredWeight(float requiredWeight)
    {
        this.requiredWeight = requiredWeight;
    }
    public float GetRequredWeight()
    {
        return requiredWeight;
    }
}