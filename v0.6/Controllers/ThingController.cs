using UnityEngine;
using EvtSource;
using System;
using System.Threading.Tasks;
using Proyecto26;
using System.Collections.Generic;
using Microsoft.MixedReality.Toolkit.UI;
using System.IO;

class ThingController : MonoBehaviour
{
    [SerializeField]
    public string _serverUri = "http://localhost:8080";
    public string _name = "";
    public List<GameObject> _items;

    private Dictionary<string, GameObject> _widgetPrefabs;

    public void GetGroupItems()
    {
        RestClient.Get(_serverUri + "/rest/items/" + _name).Then(res => {
            if (res.StatusCode >= 200 && res.StatusCode < 300)
            {
                EnrichedGroupItemDTO equipmentItems = JsonUtility.FromJson<EnrichedGroupItemDTO>(res.Text);

                Vector3 _shift = Vector3.zero;
                float delta = 0.5f;

                foreach (EnrichedItemDTO item in equipmentItems.members)
                {
                    if (_widgetPrefabs.ContainsKey(item.type))
                    {
                        GameObject createdItem;
                        createdItem = Instantiate(_widgetPrefabs[item.type], this.transform.position + _shift, Quaternion.identity) as GameObject;

                        createdItem.GetComponent<ItemWidget>()._Item = item.name;
                        //if (_name == "YeelightColorLEDBulb") DontDestroyOnLoad(createdItem);
                        createdItem.transform.SetParent(this.gameObject.transform);
                        createdItem.name = item.name + "Widget";

                        GameObject itemToolTip;

                        itemToolTip = Instantiate(LoadPrefabFromFile("ToolTip"), this.transform.position + _shift, Quaternion.identity) as GameObject;
                        itemToolTip.GetComponent<ToolTip>().ToolTipText = item.name;
                        itemToolTip.GetComponent<ToolTipConnector>().Target = createdItem;
                        itemToolTip.transform.SetParent(createdItem.transform);

                        _shift.Set(_shift.x, _shift.y, _shift.z + delta);
                    }
                    else
                    {
                        print(equipmentItems.name + " item " + item.name + " of type " + item.type + " has no predefined prefab for this type.");
                    }
                }

            }
            else
            {
                Debug.Log("Rest GET status for Item: " + " was not in OK span. (" + res.StatusCode + ")\n" + res.Error);
            }
        });
    }

    public void Initialize(string name, Dictionary<string, GameObject> widgetPrefabs)
    {
        _name = name;
        _widgetPrefabs = widgetPrefabs;
        GetGroupItems();
    }
    public void AddItemToThing(GameObject item)
    {
        if (_items == null) _items = new List<GameObject>();
        _items.Add(item);
    }



    //Move it away from this class; make loading of prefabs in semanticmodelcontroller using this method by default
    private UnityEngine.Object LoadPrefabFromFile(string filename)
    {
        Debug.Log("Trying to load LevelPrefab from file (" + filename + ")...");
        var loadedObject = Resources.Load("Prefabs/" + filename);
        if (loadedObject == null)
        {
            throw new FileNotFoundException("...no file found - please check the configuration");
        }
        return loadedObject;
    }

    private bool CreateVirtualLocationItem()
    {

        return false;
    }

    public void CreateLocationItemOnServer()
    {
        GroupItemDTO locationItem = new GroupItemDTO();
        locationItem.type = "Switch";
        locationItem.name = "example_location";
        locationItem.label = "example_location";
        ItemController _locationitemcontroller = new ItemController();

        _locationitemcontroller.CreateItemOnServer(locationItem);
    }
}
