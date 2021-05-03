using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirtualPositionWidget : ItemWidget
{
    [Header("Virtual Position parameters")]
    [Tooltip("The Widget to synchronize the Virtual Position of")]
    public GameObject TargetWidget;

    private readonly Queue<Vector3> sentValues = new Queue<Vector3>();

    public override void Start()
    {
        base.Start();
        InitWidget();
    }

    private void InitWidget()
    {
        if (TargetWidget == null) TargetWidget = GameObject.Find(item.Substring(0, item.Length - "_VirtualPosition".Length));
        //if (TargetWidget == null) TargetWidget = gameObject;
        itemController.updateItem?.Invoke();
    }

    public override void OnUpdate()
    {
        Vector3 value = itemController.GetItemStateAsVector();
        if (TargetWidget != null) if (!sentValues.Contains(value)) TargetWidget.transform.position = value;
    }

    public void OnSetItem()
    {
        Vector3 value = TargetWidget.transform.position;
        itemController.SetItemStateAsVector(value);
        sentValues.Enqueue(value);
        if (sentValues.Count > 40) sentValues.Dequeue();
    }

    public void Update()
    {
        OnSetItem();
    }
}
