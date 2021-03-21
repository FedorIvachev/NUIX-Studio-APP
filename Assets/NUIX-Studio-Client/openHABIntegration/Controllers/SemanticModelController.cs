using Proyecto26;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;

class SemanticModelController : MonoBehaviour
{
    [SerializeField] 
    public GameObject _dimmerWidgetPrefab;
    public GameObject _switchWidgetPrefab;
    public GameObject _textWidgetPrefab;
    public GameObject _equipmentWidgetPrefab;
    public GameObject _colorWidgetPrefab;

    [Header("Tag-based interactables")]
    public GameObject _dimmerGestureControlPrefab;
    public GameObject _dimmerBrightnessWidgetPrefab;

    public GameObject _stringVirtualLocationDataPrefab;

    [Header("Client config")]
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
        GetComponent<EventController>().StartListen();
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
                List<EnrichedGroupItemDTO> items = JsonUtility.FromJson<EquipmentItemModelList>("{\"result\":" + res.Text + "}").result;

                foreach (EnrichedGroupItemDTO equipmentItemModel in items)
                {
                    Debug.Log("Adding item " + equipmentItemModel.name + " to the model");
                    List<GameObject> Widgets = new List<GameObject>();
                    Widgets.AddRange(CreateWidgetsByPrefab(equipmentItemModel));
                    SemanticModel.getInstance().AddItem(equipmentItemModel, Widgets);
                }
            }
            else
            {
                Debug.Log("Rest GET status for Item: " + " was not in OK span. (" + res.StatusCode + ")\n" + res.Error);
            }
        });
    }

    public void GetItem(string itemId)
    {
        RestClient.Get(ClientConfig.getInstance()._ServerURL + "/rest/items/" + itemId).Then(res => {
            if (res.StatusCode >= 200 && res.StatusCode < 300)
            {
                EnrichedGroupItemDTO item = JsonUtility.FromJson<EnrichedGroupItemDTO>(res.Text);
                List<GameObject> Widgets = new List<GameObject>();
                Widgets.AddRange(CreateWidgetsByPrefab(item));
                SemanticModel.getInstance().AddItem(item, Widgets);
            }
            else
            {
                Debug.Log("Rest GET status for Item: " + " was not in OK span. (" + res.StatusCode + ")\n" + res.Error);
            }
        });
    }

    public void RemoveItem(string itemId)
    {
        SemanticModel.getInstance().RemoveItem(itemId);
    }

    private void InitializeWidgetPrefabDictionary()
    {
        if (_dimmerWidgetPrefab != null) ClientConfig.getInstance()._widgetPrefabs["Dimmer"] = _dimmerWidgetPrefab;
        if (_switchWidgetPrefab != null) ClientConfig.getInstance()._widgetPrefabs["Switch"] = _switchWidgetPrefab;
        if (_textWidgetPrefab != null) ClientConfig.getInstance()._widgetPrefabs["String"] = _textWidgetPrefab;
        if (_equipmentWidgetPrefab != null) ClientConfig.getInstance()._widgetPrefabs["Group"] = _equipmentWidgetPrefab;
        if (_colorWidgetPrefab != null) ClientConfig.getInstance()._widgetPrefabs["Color"] = _colorWidgetPrefab;


        // There are plenty of Number:<dimension> units https://www.openhab.org/docs/concepts/units-of-measurement.html
        // Not going to add a dict entry for each of them for now
        if (_textWidgetPrefab != null) ClientConfig.getInstance()._widgetPrefabs["Number:Time"] = _textWidgetPrefab;
        if (_textWidgetPrefab != null) ClientConfig.getInstance()._widgetPrefabs["Number"] = _textWidgetPrefab;
        if (_textWidgetPrefab != null) ClientConfig.getInstance()._widgetPrefabs["DateTime"] = _textWidgetPrefab;


        // Tag-Based interactables
        if (_dimmerGestureControlPrefab != null) ClientConfig.getInstance()._widgetPrefabs["Dimmer_NUIXGestureControl"] = _dimmerGestureControlPrefab;
        if (_dimmerBrightnessWidgetPrefab != null) ClientConfig.getInstance()._widgetPrefabs["Dimmer_NUIXBrightness"] = _dimmerBrightnessWidgetPrefab;

        if (_stringVirtualLocationDataPrefab != null) ClientConfig.getInstance()._widgetPrefabs["String_VirtualLocationData"] = _stringVirtualLocationDataPrefab;
        
    }


    public List<GameObject> CreateWidgetsByPrefab(EnrichedGroupItemDTO item)
    {
        List<GameObject> itemWidgets = new List<GameObject>();

        // Need to delete it and only create if there is no item based on tag created
        if (ClientConfig.getInstance()._widgetPrefabs.ContainsKey(item.type))
        {
            GameObject itemWidget;
            itemWidget = Instantiate(ClientConfig.getInstance()._widgetPrefabs[item.type], this.transform.position, Quaternion.identity) as GameObject;
            itemWidget.GetComponent<ItemWidget>().item = item.name;
            itemWidget.name = item.name + " Widget";
            itemWidgets.Add(itemWidget);

            if (item.label != "VirtualLocationData") CreateVirtualLocationItemOnServer(item.name);
        }
        foreach (string itemTag in item.tags)
        {
            if (ClientConfig.getInstance()._widgetPrefabs.ContainsKey(item.type + "_" + itemTag))
            {
                GameObject itemWidget;
                itemWidget = Instantiate(ClientConfig.getInstance()._widgetPrefabs[item.type + "_" + itemTag], this.transform.position, Quaternion.identity) as GameObject;
                itemWidget.GetComponent<ItemWidget>().item = item.name;
                itemWidget.name = item.name + "_" + itemTag + " Widget";
                itemWidgets.Add(itemWidget);

                if (itemTag != "VirtualLocationData")
                {
                    CreateVirtualLocationItemOnServer(item.name + "_" + itemTag);
                }
            }
        }
        return itemWidgets;
    }



    private void CreateVirtualLocationItemOnServer(string itemName)
    {
        // Create Location Data Item
        List<string> groups = new List<string>
        {

        };
        List<string> locationTag = new List<string>
                        {
                            "VirtualLocationData"
                        };
        // Need to Create a locationItem on server
        GroupItemDTO locationDataItem = new GroupItemDTO
        {
            type = "String",
            name = itemName + "_" + "VirtualLocationData",
            label = "VirtualLocationData",
            tags = locationTag,
            groupNames = groups
        };
        CreateItemOnServer(locationDataItem);
    }

    // TODO: Create a widget for the item on Success calback
    /// <summary>
    /// Creates an item on server with PUT method (GroupItemDTO item)
    /// </summary>
    /// <param name="item"></param>
    public void CreateItemOnServer(GroupItemDTO item)
    {
        RestClient.DefaultRequestHeaders["Authorization"] = ClientConfig.getInstance().Authenticate();
        RestClient.DefaultRequestHeaders["content-type"] = "application/json";
        RequestHelper currentRequest;
        currentRequest = new RequestHelper
        {
            Uri = UriBuilder.GetItemUri(ClientConfig.getInstance()._ServerURL, item.name),
            Body = item,
            Retries = 5,
            RetrySecondsDelay = 1,
            RetryCallback = (err, retries) => {
                Debug.Log(string.Format("Retry #{0} Status {1}\nError: {2}", retries, err.StatusCode, err));
            }
        };

        RestClient.Put<string>(currentRequest, (err, res, body) => {
            if (err != null)
            {
                print("Error creating item on server");
                print(err.Message);
            }
            else
            {
                print("Success " + res.StatusCode + JsonUtility.ToJson(body, true));
            }
            RestClient.ClearDefaultHeaders();
        });

    }

    private void SetParentTransforms()
    {
        // setting the parent based on groupnames

        foreach (KeyValuePair<string, Item> item in SemanticModel.getInstance().items)
        {
            if (item.Value._itemWidgets != null)
            {
                // The item should be a child of its groupname item. If we find at least one such groupname, set the transform parent to its widget then
                foreach (string groupName in item.Value.ItemModel.groupNames)
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
        string adr = GameObject.Find("NativeKeyboardOutputIPAdress").GetComponent<TMPro.TextMeshPro>().text;
        //string adr = GameObject.Find("KeyboardOutputIPAdress").GetComponent<TMP_InputField>().text;
        print(adr);
        if (System.Net.IPAddress.TryParse(adr, out var _))
        {
            print("DEBUG IP ADRESS " + adr);
            ClientConfig.getInstance()._ServerURL = "http://" + adr;
        }
        else
        {
            print("Invalid IP Address");
            ClientConfig.getInstance()._ServerURL = "http://" + adr;
        }
    }

    //Move it away from this class; make loading of prefabs in semanticmodelcontroller using this method by default
    private GameObject LoadPrefabFromFile(string filename)
    {
        Debug.Log("Trying to load LevelPrefab from file (" + filename + ")...");
        var loadedObject = Resources.Load(filename);
        if (loadedObject == null)
        {
            throw new FileNotFoundException("...no file found - please check the configuration");
        }
        return loadedObject as GameObject;
    }
}