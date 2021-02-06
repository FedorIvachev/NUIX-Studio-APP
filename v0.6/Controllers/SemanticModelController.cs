using Proyecto26;
using System.Collections.Generic;
using UnityEngine;

class SemanticModelController : MonoBehaviour
{
    [SerializeField]
    private bool _isConnected = false;
    public string _serverUri = "http://localhost:8080";
    public List<ThingModel> _things;

    void Start()
    {
        GetModel();
    }

    private void GetModel()
    {
        GetThingNames();
    }

    public void AddThingToModel(ThingModel thing)
    {
        if (_things == null) _things = new List<ThingModel>();
        _things.Add(thing);
    }

	public void GetThingNames()
	{
		RestClient.Get(_serverUri + "/rest/items?tags=Equipment").Then(res => {
			if (res.StatusCode >= 200 && res.StatusCode < 300)
			{
				List<EquipmentItemModel> equipmentItems = JsonUtility.FromJson<EquipmentItemModelList>("{\"result\":" + res.Text + "}").result;
                print("Got " + equipmentItems.Count + " equipment items from the server");
				foreach(EquipmentItemModel equipmentItemModel in equipmentItems)
                {
                    string equipmentName = equipmentItemModel.name;
                    print(equipmentName);
                    //print(equipmentItemModel.members.Count);
                }
			}
			else
			{
				Debug.Log("Rest GET status for Item: " + " was not in OK span. (" + res.StatusCode + ")\n" + res.Error);
			}
		});
	}

}