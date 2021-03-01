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
    public GameObject _equipmentWidgetPrefab;

    [Header("Category prefabs")]
    public GameObject _lampPrefab;
    public GameObject _mobilePhonePrefab;

    [Header("Server setup")]
    public string _ServerURL = ClientConfig.getInstance()._ServerURL;
    public string _Username = ClientConfig.getInstance()._Username;
    public string _Password = ClientConfig.getInstance()._Password;



    void Start()
    {
        InitializeWidgetPrefabDictionary();
        GetModel();
    }

    private void GetModel()
    {
        //GetThingNames();
        GetAllItems();
    }

    private void Update()
    {
        SetParentTransforms();
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



                foreach (EnrichedGroupItemDTO equipmentItemModel in equipmentItems)
                {
                    Item item = new Item();
                    item._itemModel = equipmentItemModel;
                    //item._itemWidget = createdItem;
                    SemanticModel.getInstance()._items[equipmentItemModel.name] = item;
                }
                CreateWidgetsForItems();
            }
            else
			{
				Debug.Log("Rest GET status for Item: " + " was not in OK span. (" + res.StatusCode + ")\n" + res.Error);
			}
		});
	}


    public void GetAllItems()
    {
        RestClient.Get(ClientConfig.getInstance()._ServerURL + "/rest/items").Then(res => {
            if (res.StatusCode >= 200 && res.StatusCode < 300)
            {
                List<EnrichedGroupItemDTO> equipmentItems = JsonUtility.FromJson<EquipmentItemModelList>("{\"result\":" + res.Text + "}").result;


                foreach (EnrichedGroupItemDTO equipmentItemModel in equipmentItems)
                {
                    Item item = new Item();
                    item._itemModel = equipmentItemModel;
                    //item._itemWidget = createdItem;
                    SemanticModel.getInstance()._items[equipmentItemModel.name] = item;
                }
                CreateWidgetsForItems();
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
        if (_equipmentWidgetPrefab != null) ClientConfig.getInstance()._widgetPrefabs["Group"] = _equipmentWidgetPrefab;


        // There are plenty of Number:<dimension> units https://www.openhab.org/docs/concepts/units-of-measurement.html
        // Not going to add a dict entry for each of them for now
        if (_textWidgetPrefab != null) ClientConfig.getInstance()._widgetPrefabs["Number:Time"] = _textWidgetPrefab;
        if (_textWidgetPrefab != null) ClientConfig.getInstance()._widgetPrefabs["Number"] = _textWidgetPrefab;
        if (_textWidgetPrefab != null) ClientConfig.getInstance()._widgetPrefabs["DateTime"] = _textWidgetPrefab;

        // Category prefabs
        if (_mobilePhonePrefab != null) ClientConfig.getInstance()._categoryPrefabs["MobilePhone"] = _mobilePhonePrefab;
        if (_lampPrefab != null) ClientConfig.getInstance()._categoryPrefabs["Lamp"] = _lampPrefab;
    }


    private void CreateWidgetsForItems()
    {
        Vector3 _shift = Vector3.zero;
        float delta = 0.5f;

        foreach (KeyValuePair<string, Item> item in SemanticModel.getInstance()._items)
        {
            if (item.Value._itemModel.category == "Virtual Location")
            {
                string itemname = item.Key;
                GameObject createdItem;
                createdItem = Instantiate(ClientConfig.getInstance()._widgetPrefabs[item.Value._itemModel.category], Vector3.zero, Quaternion.identity) as GameObject;
                createdItem.GetComponent<ItemWidget>()._Item = itemname;
                createdItem.name = itemname + " Widget";
                item.Value._itemWidget = createdItem;
                continue;
            }
            if (ClientConfig.getInstance()._widgetPrefabs.ContainsKey(item.Value._itemModel.type))
            {
                string itemname = item.Key;
                GameObject createdItem;
                createdItem = Instantiate(ClientConfig.getInstance()._widgetPrefabs[item.Value._itemModel.type], this.transform.position + _shift, Quaternion.identity) as GameObject;
                createdItem.GetComponent<ItemWidget>()._Item = itemname;
                _shift.Set(_shift.x + delta, _shift.y, _shift.z);
                createdItem.name = itemname + " Widget";
                item.Value._itemWidget = createdItem;
            }
        }
    }

    private void SetParentTransforms()
    {
        // setting the parent based on groupnames

        foreach (KeyValuePair<string, Item> item in SemanticModel.getInstance()._items)
        {
            if (item.Value._itemWidget != null)
            {
                // The item should be a child of its groupname item. If we find at least one such groupname, set the transform parent to its widget then
                foreach (string groupName in item.Value._itemModel.groupNames)
                {
                    GameObject parent;
                    if ((parent = GameObject.Find(groupName + " Widget")) != null)
                    {
                        item.Value._itemWidget.gameObject.transform.SetParent(parent.transform);
                    }
                }
            }
        }
    }
}