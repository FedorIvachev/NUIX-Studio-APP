using Proyecto26;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

class SemanticModelController : MonoBehaviour
{
    [SerializeField] 
    public GameObject _dimmerWidgetPrefab;
    public GameObject _switchWidgetPrefab;
    public GameObject _textWidgetPrefab;
    public GameObject _locationWidgetPrefab;
    public GameObject _equipmentWidgetPrefab;
    public GameObject _colorWidgetPrefab;

    [Header("Category prefabs")]
    public GameObject _lampPrefab;
    public GameObject _mobilePhonePrefab;

    [Header("Server setup")]
    private string _ServerURL = ClientConfig.getInstance()._ServerURL;
    public string _Username = ClientConfig.getInstance()._Username;
    public string _Password = ClientConfig.getInstance()._Password;

    public bool InitOnStartup = false;



    private void Start()
    {
        if (InitOnStartup)
        {
            StartSystem();
        }
    }
    public void StartSystem()
    {
        print("Starting System");
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
        // Otherwise call it after all the widgets are added to the scene
        // Currently the complexity is O(n)
        SetParentTransforms();
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
        if (_colorWidgetPrefab != null) ClientConfig.getInstance()._widgetPrefabs["Color"] = _colorWidgetPrefab;


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
        Vector3 _shift = new Vector3(0.0f, 0.0f, 0.0f);
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
                item.Value.AddWidget(createdItem);
                continue;
            }
            else if (ClientConfig.getInstance()._widgetPrefabs.ContainsKey(item.Value._itemModel.type))
            {
                if (item.Value._itemModel.type == "Group" && ClientConfig.getInstance()._categoryPrefabs.ContainsKey(item.Value._itemModel.category))
                {
                    string categoryitemname = item.Key;
                    GameObject createdCategoryItem;
                    createdCategoryItem = Instantiate(ClientConfig.getInstance()._categoryPrefabs[item.Value._itemModel.category], this.transform.position + _shift, Quaternion.identity) as GameObject;
                    createdCategoryItem.GetComponent<ItemWidget>()._Item = categoryitemname;
                    _shift.Set(_shift.x + delta, _shift.y, _shift.z);
                    createdCategoryItem.name = categoryitemname + " Widget";
                    item.Value.AddWidget(createdCategoryItem);
                }
                else
                {
                    string itemname = item.Key;
                    GameObject createdItem;
                    createdItem = Instantiate(ClientConfig.getInstance()._widgetPrefabs[item.Value._itemModel.type], this.transform.position + _shift, Quaternion.identity) as GameObject;
                    createdItem.GetComponent<ItemWidget>()._Item = itemname;
                    _shift.Set(_shift.x + delta, _shift.y, _shift.z);
                    createdItem.name = itemname + " Widget";
                    item.Value.AddWidget(createdItem);
                }
            }
        }
    }

    private void SetParentTransforms()
    {
        // setting the parent based on groupnames

        foreach (KeyValuePair<string, Item> item in SemanticModel.getInstance()._items)
        {
            if (item.Value._itemWidgets != null)
            {
                // The item should be a child of its groupname item. If we find at least one such groupname, set the transform parent to its widget then
                foreach (string groupName in item.Value._itemModel.groupNames)
                {
                    GameObject parent;
                    if ((parent = GameObject.Find(groupName + " Widget")) != null)
                    {
                        foreach (GameObject itemWidget in item.Value._itemWidgets)
                        {
                            itemWidget.transform.SetParent(parent.transform);
                        }
                    }
                }
            }
        }
    }


    public void SetServerIPAdress()
    {
        string adr = GameObject.Find("KeyboardOutputIPAdress").GetComponent<TMPro.TextMeshPro>().text;
        if (System.Net.IPAddress.TryParse(adr, out var _))
        {
            print("DEBUG IP ADRESS " + adr);
            ClientConfig.getInstance()._ServerURL = "http://" + adr;
        }
        else
        {
            print("Invalid IP Address");
        }
    }
}