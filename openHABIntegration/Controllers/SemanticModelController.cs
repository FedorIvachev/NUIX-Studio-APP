using Proyecto26;
using System.Collections.Generic;
using UnityEngine;

class SemanticModelController : MonoBehaviour
{
    [SerializeField] 
    public GameObject _dimmerWidgetPrefab;
    public GameObject _switchWidgetPrefab;
    public GameObject _textWidgetPrefab;
    public GameObject _locationWidgetPrefab;

    public GameObject _thingWidgetPrefab;


    [Header("Category prefabs")]
    public GameObject _lampPrefab;
    public GameObject _mobilePhonePrefab;


    void Start()
    {
        InitializeWidgetPrefabDictionary();
        GetModel();
    }

    private void GetModel()
    {
        GetThingNames();
    }


    // TODO: get all items names, put in a dictionary of itemcontrollers
    // Next build a tree of items based on the groupNames field
    // Then it must be easy to assign item values
	public void GetThingNames()
	{
		RestClient.Get(ClientConfig.getInstance()._ServerURL + "/rest/items?tags=Equipment").Then(res => {
			if (res.StatusCode >= 200 && res.StatusCode < 300)
			{
				List<EnrichedGroupItemDTO> equipmentItems = JsonUtility.FromJson<EquipmentItemModelList>("{\"result\":" + res.Text + "}").result;

                Vector3 _shift = Vector3.zero;
                float delta = 0.5f;

                foreach (EnrichedGroupItemDTO equipmentItemModel in equipmentItems)
                {
                    string equipmentName = equipmentItemModel.name;
                    GameObject createdItem;
                    createdItem = Instantiate(_thingWidgetPrefab, this.transform.position + _shift, Quaternion.identity) as GameObject;
                    createdItem.GetComponent<ItemWidget>()._Item = equipmentName;
                    _shift.Set(_shift.x + delta, _shift.y, _shift.z);
                    createdItem.name = equipmentName + "Widget";
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
        if (_dimmerWidgetPrefab != null) ClientConfig.getInstance()._widgetPrefabs["Dimmer"] = _dimmerWidgetPrefab;
        if (_switchWidgetPrefab != null) ClientConfig.getInstance()._widgetPrefabs["Switch"] = _switchWidgetPrefab;
        if (_textWidgetPrefab != null) ClientConfig.getInstance()._widgetPrefabs["String"] = _textWidgetPrefab;
        if (_locationWidgetPrefab != null) ClientConfig.getInstance()._widgetPrefabs["Virtual Location"] = _locationWidgetPrefab;

        // There are plenty of Number:<dimension> units https://www.openhab.org/docs/concepts/units-of-measurement.html
        // Not going to add a dict entry for each of them for now
        if (_textWidgetPrefab != null) ClientConfig.getInstance()._widgetPrefabs["Number:Time"] = _textWidgetPrefab;
        if (_textWidgetPrefab != null) ClientConfig.getInstance()._widgetPrefabs["Number"] = _textWidgetPrefab;
        if (_textWidgetPrefab != null) ClientConfig.getInstance()._widgetPrefabs["DateTime"] = _textWidgetPrefab;

        // Category prefabs
        if (_mobilePhonePrefab != null) ClientConfig.getInstance()._categoryPrefabs["MobilePhone"] = _mobilePhonePrefab;
        if (_lampPrefab != null) ClientConfig.getInstance()._categoryPrefabs["Lamp"] = _lampPrefab;
    }
}