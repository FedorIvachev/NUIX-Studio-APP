using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionsUIController : MonoBehaviour
{
    //public ItemsStorage itemsStorage;
    //public GameObject actionView;
    //public GameObject actionsLabel;
    //// Start is called before the first frame update
    //void Start()
    //{
        
    //}

    //// Update is called once per frame
    //void Update()
    //{
        
    //}


    //public void TrackActionsAvailability(ActionData actionData, bool isOn)
    //{
    //    actionData.isActionEnabled = isOn;
    //    if (isOn)
    //    {
    //          itemsStorage.GetItemDataByID(actionData.fromItemID).gameObject.GetComponent<ItemPresenter>().method =
    //          itemsStorage.GetItemDataByID(actionData.toItemID).gameObject.GetComponent<ItemPresenter>().actions;
    //        ItemPresenter itempresenterFrom = itemsStorage.GetItemDataByID(actionData.fromItemID).gameObject.GetComponent<ItemPresenter>();
    //    }
    //    else
    //    {
    //        itemsStorage.GetItemDataByID(actionData.fromItemID).gameObject.GetComponent<ItemPresenter>().method = null;
    //    }
    //}


    //public GameObject AddActionView(ActionData actionData)
    //{
    //    GameObject action = Instantiate(actionView, Vector3.zero, Quaternion.identity);
    //    action.name = actionData.ToString();
    //    action.transform.SetParent(actionsLabel.transform, false);
    //    action.GetComponentInChildren<TMPro.TMP_Text>().text = "Sample text";
    //    action.GetComponentInChildren<Toggle>().isOn = actionData.isActionEnabled;
    //    action.GetComponentInChildren<Toggle>().onValueChanged.AddListener(
    //        delegate
    //        {
    //            TrackActionsAvailability(actionData, action.GetComponentInChildren<Toggle>().isOn);
    //        });
    //    return action;
    //}
}
