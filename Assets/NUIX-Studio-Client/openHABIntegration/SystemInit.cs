using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Proyecto26;

/*
public class EventController : MonoBehaviour
{
	private readonly string apiPath = "http://localhost:8080/rest";
	List<ThingModel> Things = new List<ThingModel>();
	List<EquipmentItemModel> Equipment = new List<EquipmentItemModel>();
	public void SystemInit()
    {
		TryGetEquipmentFromServer();
    }

    private void TryGetEquipmentFromServer()
    {
		print("TryGetEquipmentFromServer");
		RestClient.Get(apiPath + "/items?tags=Equipment").Then(res => {
			if (res.StatusCode >= 200 && res.StatusCode < 300)
			{
				print(res.Text);
				Equipment = JsonUtility.FromJson<EquipmentItemModelList>("{\"result\":" + res.Text + "}").result;

				foreach (EquipmentItemModel equipmentItem in Equipment)
				{
					Things.Add(new ThingModel(name = equipmentItem.name));
				}


			}
			else
			{
				Debug.Log("Rest GET status for receiving Equipment was not in OK span. (" + res.StatusCode + ")\n" + res.Error);
			}
		});
	}
}
*/