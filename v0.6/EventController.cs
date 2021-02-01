using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Proyecto26;

public class EventController : MonoBehaviour
{
	private readonly string apiPath = "http://localhost:8080/rest";
	List<ThingModel> Things = new List<ThingModel>();
	public void SystemInit()
    {
        if (TryGetEquipmentFromServer(out List<EquipmentItemModel> equipmentItems))
        {
			print("Successfully received the list of equipment items from the server");
			foreach (EquipmentItemModel equipmentItem in equipmentItems)
            {
				Things.Add(new ThingModel(name = equipmentItem.name));
            }
			print(Things.Count);
        }
    }

    private bool TryGetEquipmentFromServer(out List<EquipmentItemModel> equipmentItems)
    {
		print("TryGetEquipmentFromServer");
		equipmentItems = new List<EquipmentItemModel>();
		List<EquipmentItemModel> equipmentItemsTemp = new List<EquipmentItemModel>();
		RestClient.Get(apiPath + "/items?tags=Equipment").Then(res => {
			if (res.StatusCode == 200)
			{
				equipmentItemsTemp = JsonUtility.FromJson<EquipmentItemModelList>("{\"result\":" + res.Text + "}").result;
			}
			else
			{
				Debug.Log("Rest GET status for receiving Equipment was not in OK span. (" + res.StatusCode + ")\n" + res.Error);
			}
		});

		if (equipmentItemsTemp.Capacity > 0)
        {
			equipmentItems = equipmentItemsTemp;
			return true;
        }
		else
        {
			return false;
        }
	}
}