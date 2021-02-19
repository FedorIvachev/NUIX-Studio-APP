using Proyecto26;
using System.Collections.Generic;
using UnityEngine;

class SemanticModelController : MonoBehaviour
{
    [SerializeField]
    private bool _isConnected = false;
    public string _serverUri = "http://localhost:8080";
    public List<ThingWidget> _things;

    [SerializeField] 
    public GameObject _dimmerWidgetPrefab;
    public GameObject _switchWidgetPrefab;
    public GameObject _textWidgetPrefab;

    public GameObject _thingWidgetPrefab;

    public Dictionary<string, GameObject> _widgetPrefabs = new Dictionary<string, GameObject>();

    void Start()
    {
        InitializeWidgetPrefabDictionary();
        GetModel();
    }

    private void GetModel()
    {
        GetThingNames();
    }

    public void AddThingToModel(ThingWidget thing)
    {
        if (_things == null) _things = new List<ThingWidget>();
        _things.Add(thing);
    }

	public void GetThingNames()
	{
		RestClient.Get(_serverUri + "/rest/items?tags=Equipment").Then(res => {
			if (res.StatusCode >= 200 && res.StatusCode < 300)
			{
				List<EnrichedGroupItemDTO> equipmentItems = JsonUtility.FromJson<EquipmentItemModelList>("{\"result\":" + res.Text + "}").result;
                print("Got " + equipmentItems.Count + " equipment items from the server");


                Vector3 _shift = Vector3.zero;
                float delta = 0.5f;

                foreach (EnrichedGroupItemDTO equipmentItemModel in equipmentItems)
                {
                    string equipmentName = equipmentItemModel.name;
                    print(equipmentName);

                    GameObject createdItem;
                    createdItem = Instantiate(_thingWidgetPrefab, this.transform.position + _shift, Quaternion.identity) as GameObject;

                    _shift.Set(_shift.x + delta, _shift.y, _shift.z);
                    createdItem.name = equipmentName + "Widget";
                    createdItem.GetComponent<ThingWidget>().Initialize(equipmentName, _widgetPrefabs);
                    
                    //print(equipmentItemModel.members.Count);
                }
			}
			else
			{
				Debug.Log("Rest GET status for Item: " + " was not in OK span. (" + res.StatusCode + ")\n" + res.Error);
			}
		});
	}

    private void InitializeWidgetPrefabDictionary()
    {
        if (_dimmerWidgetPrefab != null) _widgetPrefabs["Dimmer"] = _dimmerWidgetPrefab;
        if (_switchWidgetPrefab != null) _widgetPrefabs["Switch"] = _switchWidgetPrefab;
        if (_textWidgetPrefab != null) _widgetPrefabs["String"] = _textWidgetPrefab;
        // There are plenty of Number:<dimension> units https://www.openhab.org/docs/concepts/units-of-measurement.html
        // Not going to add a dict entry for each of them for now
        if (_textWidgetPrefab != null) _widgetPrefabs["Number:Time"] = _textWidgetPrefab;
        if (_textWidgetPrefab != null) _widgetPrefabs["Number"] = _textWidgetPrefab;
        if (_textWidgetPrefab != null) _widgetPrefabs["DateTime"] = _textWidgetPrefab;
    }
}